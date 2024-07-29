using UnityEngine;
using UnityEngine.Events;

public class VictoryChecker : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private int _minKillGoal;
    [SerializeField] private int _maxKillGoal;
    [SerializeField] private bool _immediateStart;

    public UnityEvent Won;
    public UnityEvent<int, int> CurrentKillGoalChanged;

    private int _currentKillGoal;

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

    private void OnEnemyDied(BaseEnemy enemy)
    {
        enemy.Died.RemoveListener(OnEnemyDied);
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