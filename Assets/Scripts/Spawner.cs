using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private EnemyFactory enemyFactory;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private float _minCooldownInSeconds;
    [SerializeField] private float _maxCooldownInSeconds;
    [SerializeField] private bool _startSpawnOnStart;
    [SerializeField] private DamageType _gameDamage;

    public UnityEvent<BaseEnemy> Spawned;

    private Coroutine _spawnCoroutine;
    private HashSet<BaseEnemy> _aliveEnemies = new();

    private void Start()
    {
        if(_startSpawnOnStart)
            StartSpawning();
    }

    public void StartSpawning()
    {
        if(_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);
        _spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }
    
    public void StopSpawning()
    {
        StopCoroutine(_spawnCoroutine);
    }

    public void KillAll()
    {
        var enemies = new List<BaseEnemy>(_aliveEnemies);
        foreach (var enemy in enemies)
            enemy.Kill(_gameDamage);
        _aliveEnemies.Clear();
    }

    private IEnumerator SpawnCoroutine()
    {
        while(true)
        {
            SpawnEnemy();
            var cooldown = Random.Range(_minCooldownInSeconds, _maxCooldownInSeconds);
            yield return new WaitForSeconds(cooldown);
        }
    }

    private void SpawnEnemy()
    {
        var newEnemy = enemyFactory.Create();
        var spawnPoint = _spawnPoints.GetRandom();
        newEnemy.transform.position = spawnPoint.position;
        _aliveEnemies.Add(newEnemy);
        newEnemy.Died.AddListener(OnDied);
        Spawned.Invoke(newEnemy);
    }

    private void OnDied(BaseEnemy newEnemy, DamageType damageType)
    {
        newEnemy.Died.RemoveListener(OnDied);
        _aliveEnemies.Remove(newEnemy);
    }
}
