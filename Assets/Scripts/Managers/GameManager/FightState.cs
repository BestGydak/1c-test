using UnityEngine;

public class FightState : State
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private InputReader _inputReader;

    public override void OnEnter()
    {
        _inputReader.EnablePlayerControls();
        _gameManager.Started.Invoke();
        _gameManager.VictoryChecker.StartGame();
        _gameManager.DefeatChecker.StartGame();
        _gameManager.Spawner.StartSpawning();    
    }

    public override void OnExit()
    {
        _inputReader.DisablePlayerControls();
        _gameManager.Spawner.StopSpawning();
        _gameManager.Spawner.KillAll();
    }
}
