using UnityEngine;
using UnityEngine.Events;

public class VictoryChecker : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    public UnityEvent<VictoryChecker> Won;

    private void OnEnable()
    {
        _spawner.Spawned.AddListener(OnSpawned);
    }

    private void OnDisable()
    {
        _spawner.Spawned.RemoveListener(OnSpawned);
    }

    private void OnSpawned(Spawner _, BaseEnemy enemy)
    {
        enemy.Died.AddListener(OnEnemyDied);
    }

    private void OnEnemyDied(BaseEnemy enemy)
    {
        enemy.Died.RemoveListener(OnEnemyDied);
        if (_spawner.EnemiesRemainToSpawn == 0 &&
            _spawner.AliveEnemies.Count == 0)
            Won.Invoke(this);
    }
}
