using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    private Coroutine _waitingCoroutine;

    public override void Launch(Entity owner, params object[] p)
    {
        _owner = owner;
        _waitingCoroutine = owner.StartCoroutine(Tool.Wait(Random.Range(1, 3), Finish));
    }

    public override void Stop()
    {
        _owner.StopCoroutine(_waitingCoroutine);
    }
}
