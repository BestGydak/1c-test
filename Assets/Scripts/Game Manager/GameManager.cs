using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [field: SerializeField] public DefeatChecker DefeatChecker { get; private set; }
    [field: SerializeField] public VictoryChecker VictoryChecker { get; private set; }  
    [field: SerializeField] public Spawner Spawner { get; private set; }
    [SerializeField] private bool _immediateStart;

    [Header("Game States")]
    [SerializeField] private StateMachine _stateMachine;
    [SerializeField] private State _winState;
    [SerializeField] private State _lostState;
    [SerializeField] private State _fightState;

    public UnityEvent Won;
    public UnityEvent Lost;
    public UnityEvent Started;

    private void Start()
    {
        if(_immediateStart)
            StartGame();
    }

    public void OnEnable()
    {
        VictoryChecker.Won.AddListener(OnWon);
        DefeatChecker.Defeated.AddListener(OnDefeated);
    }

    public void OnDisable()
    {
        VictoryChecker.Won.RemoveListener(OnWon);
        DefeatChecker.Defeated.RemoveListener(OnDefeated);
    }

    public void StartGame()
    {
        _stateMachine.IsRunning = true;
        _stateMachine.SetState(_fightState);
    }

    private void OnWon()
    {
        if(_stateMachine.CurrentState != _lostState)
            _stateMachine.SetState(_winState);
    }

    private void OnDefeated()
    {
        if(_stateMachine.CurrentState != _winState)
            _stateMachine.SetState(_lostState);
    }
}
