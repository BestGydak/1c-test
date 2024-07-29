using UnityEngine;

public class PreGameState : State
{
    [SerializeField] private InputReader _inputReader;

    public override void OnEnter()
    {
        _inputReader.DisablePlayerControls();
    }
}
