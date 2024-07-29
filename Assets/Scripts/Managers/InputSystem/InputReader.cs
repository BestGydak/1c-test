using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Input Reader", fileName = "Input Reader")]
public class InputReader : ScriptableObject, InputActions.IPlayerActions
{
    private InputActions _inputActions;

    public event Action<Vector2> Moved;

    private void OnEnable()
    {
        if (_inputActions != null)
            return;

        _inputActions = new InputActions();
        _inputActions.Player.SetCallbacks(this);
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

    private void OnDestroy()
    {
        _inputActions.Dispose();
    }

    public void DisablePlayerControls()
    {
        _inputActions.Player.Disable();
    }

    public void EnablePlayerControls()
    {
        _inputActions.Player.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Moved?.Invoke(context.ReadValue<Vector2>());
    }
}
