using UnityEngine;

public class PatrolState : BaseState
{
    public override void Launch(Entity owner, params object[] p)
    {
        _owner = owner;
        Cell[] path = LevelGrid.Instance.RandomPath(7, _owner.Warden.OccupiedCell);
        _owner.Animator.SetBool("IsRunning", false);
        _owner.StartCoroutine(_owner.Move(path, _owner.MovementSpeed, Finish));
    }
}