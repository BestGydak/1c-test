using UnityEngine;

public class FightState : State
{
    [SerializeField] private GameManager _gameManager;

    public override void OnEnter()
    {
        _gameManager.Started.Invoke();
        _gameManager.VictoryChecker.StartGame();
        _gameManager.DefeatChecker.StartGame();
        _gameManager.Spawner.StartSpawning();    
    }

    public override void OnExit()
    {
        _gameManager.Spawner.StopSpawning();
        _gameManager.Spawner.RemoveAllEnemies();
    }
}
