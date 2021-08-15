using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabiesState : BaseState
{
    public override void Launch(Entity owner, params object[] p)
    {
        _owner = owner;
        Cell[] path = LevelGrid.Instance.RandomPath(Random.Range(3, 7), _owner.Warden.OccupiedCell);
        _owner.Animator.SetBool("IsRunning", true);
        _owner.StartCoroutine(_owner.Move(path, _owner.MovementSpeed * _owner.RunMultiplie, Finish));
    }
}
