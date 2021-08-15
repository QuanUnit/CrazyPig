using System;
using System.Collections.Generic;
using UnityEngine;

class PlayerBehaviour : EntityBehaviour
{
    [SerializeField] private GameObject _bombPrefab;

    private Controller _controller;

    public override void Initialize(Entity owner)
    {
        base.Initialize(owner);
        _controller = FindObjectOfType<Controller>();
        _controller.OnMovementButtonDrag += TryMakeStep;
        _controller.OnBombButtonClick += PlantBomb;
    }

    protected override BaseState DefineNewState(out object[] p)
    {
        p = default;
        return _states[1];
    }

    protected override List<BaseState> DefineStates()
    {
        List<BaseState> states = new List<BaseState>();
        states.Add(new MakeStepState());
        states.Add(new IdleState());

        return states;
    }

    private void TryMakeStep(Vector2 dir)
    {
        if(_activeState != _states[0])
        {
            Vector2Int vector = new Vector2Int((int)dir.x, (int)dir.y);
            object[] content = { vector };
            ForceToLaunchState(_states[0], content);
        }
    }

    private void PlantBomb()
    {
        Instantiate(_bombPrefab, transform.position, Quaternion.identity);
    }
}
