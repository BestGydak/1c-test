using UnityEngine;

public class WinState : State
{
    [SerializeField] public GameManager _gameManager;

    public override void OnEnter()
    {
        _gameManager.Won.Invoke();
    }
}