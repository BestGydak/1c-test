using UnityEngine;
using UnityEngine.Events;

public class DefeatChecker : MonoBehaviour
{
    [SerializeField] private EnemyFinishLine _enemyFinishLine;
    [SerializeField] private int _maxHp;
    [SerializeField] private bool _immediateStart;

    private int _currentHp;
    private bool _hasDefeated;
 
    public UnityEvent<int, int> HpChanged;
    public UnityEvent Defeated;

    public int CurrentHp
    {
        get => _currentHp;
        private set
        {
            var prevHp = _currentHp;
            _currentHp = value;
            HpChanged.Invoke(prevHp, _currentHp);
            TryDefeat();
        }
    }

    public int MaxHp => _maxHp;
    public bool HasDefeated => _hasDefeated;

    private void Start()
    {
        if (_immediateStart)
            StartGame();
    }

    private void OnEnable()
    {
        _enemyFinishLine.EnemyCrossed.AddListener(OnEnemyCrossed);
    }

    private void OnDisable()
    {
        _enemyFinishLine.EnemyCrossed.RemoveListener(OnEnemyCrossed);
    }

    public void StartGame()
    {
        _hasDefeated = false;
        CurrentHp = _maxHp;
    }

    private bool TryDefeat()
    {
        if (_currentHp > 0)
            return false;

        _hasDefeated = true;
        Defeated.Invoke();
        return true;
    }

    private void OnEnemyCrossed(BaseEnemy enemy)
    {
        CurrentHp -= 1;
    }
}
