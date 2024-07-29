using UnityEngine;
using UnityEngine.Events;

public class VictoryChecker : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private int _minKillGoal;
    [SerializeField] private int _maxKillGoal;
    [SerializeField] private DamageType _playerDamage;
    [SerializeField] private bool _immediateStart;

    private int _currentKillGoal;

    public UnityEvent Won;
    public UnityEvent<int, int> CurrentKillGoalChanged;

    public int CurrentKillGoal
    {
        get => _currentKillGoal;
        private set
        {
            var prevValue = _currentKillGoal;
            _currentKillGoal = value;
            CurrentKillGoalChanged.Invoke(prevValue, _currentKillGoal);
            TryWin();
        }
    }

    private void Start()
    {
        if(_immediateStart)
            StartGame();
    }

    private void OnEnable()
    {
        _spawner.Spawned.AddListener(OnSpawned);
    }

    private void OnDisable()
    {
        _spawner.Spawned.RemoveListener(OnSpawned);
    }

    public void StartGame()
    {
        CurrentKillGoal = Random.Range(_minKillGoal, _maxKillGoal);
    }

    private void OnSpawned(BaseEnemy enemy)
    {
        enemy.Died.AddListener(OnEnemyDied);
    }

    private void OnEnemyDied(BaseEnemy enemy, DamageType damageType)
    {
        enemy.Died.RemoveListener(OnEnemyDied);
        if(damageType == _playerDamage)
            CurrentKillGoal -= 1;
    }

    private bool TryWin()
    {
        if (CurrentKillGoal != 0)
            return false;

        Won.Invoke();
        return true;
    }
}
