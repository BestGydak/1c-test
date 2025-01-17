﻿using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [field: SerializeField] public bool IsRunning { get; set; }
    public State CurrentState { get; private set; }

    private void Update()
    {
        if (CurrentState != null && IsRunning)
            CurrentState.OnUpdate();
    }

    private void FixedUpdate()
    {
        if(CurrentState != null && IsRunning)
            CurrentState.OnFixedUpdate();
    }

    public void SetState(State state)
    {
        var prevState = CurrentState;
        CurrentState = state;
        if (prevState != null)
            prevState.OnExit();
        state.OnEnter();
    }
}
