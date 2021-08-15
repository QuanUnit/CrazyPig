using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeStepState : BaseState
{
    public override void Launch(Entity owner, params object[] p)
    {
        _owner = owner;
        Vector2Int dir = (Vector2Int)p[0];

        Vector2Int currentCellPos = LevelGrid.Instance.Grid.FindValue(owner.Warden.OccupiedCell);

        if (LevelGrid.Instance.Grid.TryGetValue(currentCellPos + new Vector2Int(dir.y, dir.x), out Cell target) == true && target.CellType == Cell.Type.passable)
        {
            owner.StartCoroutine(owner.Move(target, _owner.MovementSpeed, Finish));
        }
        else
        {
            Finish();
        }
    }
}
