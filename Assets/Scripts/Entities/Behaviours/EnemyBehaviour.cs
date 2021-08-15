using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : EntityBehaviour
{
    protected override BaseState DefineNewState(out object[] p)
    {
        p = new object[1];

        return _states[Random.Range(0, _states.Count)];
    }

    protected override List<BaseState> DefineStates()
    {
        List<BaseState> states = new List<BaseState>();

        states.Add(new PatrolState());
        states.Add(new RabiesState());

        return states;
    }
}

