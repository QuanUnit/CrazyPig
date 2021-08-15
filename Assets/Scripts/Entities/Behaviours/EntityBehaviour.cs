using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public abstract class EntityBehaviour : MonoBehaviour
{
    protected List<BaseState> _states;
    protected Entity _owner;
    protected BaseState _activeState;


    public virtual void Initialize(Entity owner)
    {
        _owner = owner;

        _states = DefineStates();

        foreach (var state in _states)
        {
            state.OnCompleted += SwitchState;
        }
        SwitchState();
    }

    protected void SwitchState()
    {
        _activeState = DefineNewState(out object[] p);
        _activeState.Launch(_owner, p);
    }

    protected void ForceToLaunchState(BaseState state, object[] p)
    {
        _activeState.Stop();
        _activeState = state;
        state.Launch(_owner, p);
    }

    protected abstract List<BaseState> DefineStates();
    protected abstract BaseState DefineNewState(out object[] p);
}
