using UnityEngine;

public class DefeatState : State
{
    [SerializeField] public GameManager _gameManager;

    public override void OnEnter()
    {
        _gameManager.Lost.Invoke();
    }
}
