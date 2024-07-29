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
    [SerializeField] private State _preGameState;

    public UnityEvent Won;
    public UnityEvent Lost;
    public UnityEvent Started;

    private void Start()
    {
        _stateMachine.SetState(_preGameState);
        if(_immediateStart)
            StartGame();
    }

    private void OnEnable()
    {
        VictoryChecker.Won.AddListener(OnWon);
        DefeatChecker.Defeated.AddListener(OnDefeated);
    }

    private void OnDisable()
    {
        VictoryChecker.Won.RemoveListener(OnWon);
        DefeatChecker.Defeated.RemoveListener(OnDefeated);
    }

    public void StartGame()
    {
        _stateMachine.SetState(_fightState);
    }

    private void OnWon(VictoryChecker _)
    {
        if(_stateMachine.CurrentState != _lostState)
            _stateMachine.SetState(_winState);
    }

    private void OnDefeated(DefeatChecker _)
    {
        if(_stateMachine.CurrentState != _winState)
            _stateMachine.SetState(_lostState);
    }
}
