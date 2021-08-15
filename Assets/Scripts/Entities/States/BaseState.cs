using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BaseState
{
    public event Action OnCompleted;

    protected Entity _owner;

    public abstract void Launch(Entity owner, params object[] p);
    public virtual void Stop() { }

    protected virtual void Finish()
    {
        OnCompleted?.Invoke();
    }
}
