using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private EnemyFactory enemyFactory;
    [SerializeField] private List<Transform> _spawnPoints;

    [Header("Spawn Settings")]
    [SerializeField] private float _minCooldownInSeconds;
    [SerializeField] private float _maxCooldownInSeconds;
    [SerializeField] private int _minEnemiesToSpawn;
    [SerializeField] private int _maxEnemiesToSpawn;
    [SerializeField] private bool _startSpawnOnStart;

    private Coroutine _spawnCoroutine;
    private HashSet<BaseEnemy> _aliveEnemies = new();
    private int _enemiesRemainToSpawn;

    public UnityEvent<Spawner, BaseEnemy> Spawned;
    public UnityEvent<Spawner> AllSpawned;
    public UnityEvent<Spawner, int, int> EnemiesToSpawnChanged;

    public IReadOnlyCollection<BaseEnemy> AliveEnemies => _aliveEnemies;
    public int EnemiesRemainToSpawn 
    { 
        get => _enemiesRemainToSpawn;
        private set
        {
            if(value < 0)
            {
                Debug.LogError("EnemiesRemainToSpawn can't be negative");
            }
            var prevValue = _enemiesRemainToSpawn;
            _enemiesRemainToSpawn = value;
            EnemiesToSpawnChanged.Invoke(this, prevValue, value);
        }
    }

    private void Start()
    {
        if(_startSpawnOnStart)
            StartSpawning();
    }

    public void StartSpawning()
    {
        if(_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);
        EnemiesRemainToSpawn = Random.Range(_minEnemiesToSpawn, _maxEnemiesToSpawn);
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
            enemy.Kill();
        _aliveEnemies.Clear();
    }

    private IEnumerator SpawnCoroutine()
    {
        while (EnemiesRemainToSpawn > 0)
        {
            SpawnEnemy();
            var cooldown = Random.Range(_minCooldownInSeconds, _maxCooldownInSeconds);
            yield return new WaitForSeconds(cooldown);
            EnemiesRemainToSpawn--;
        }
        AllSpawned.Invoke(this);
    }

    private void SpawnEnemy()
    {
        var newEnemy = enemyFactory.Create();
        var spawnPoint = _spawnPoints.GetRandom();
        newEnemy.transform.position = spawnPoint.position;
        _aliveEnemies.Add(newEnemy);
        newEnemy.Died.AddListener(OnDied);
        Spawned.Invoke(this, newEnemy);
    }

    private void OnDied(BaseEnemy newEnemy)
    {
        newEnemy.Died.RemoveListener(OnDied);
        _aliveEnemies.Remove(newEnemy);
    }
}
