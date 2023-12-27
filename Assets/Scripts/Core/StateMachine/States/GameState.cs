using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : IState
{
    private Pendulum _pendulum;
    private CircleCollector _circleCollector;
    private InputService _inputService;
    private StateMachine _machine;
    private IStateHolder _stateHolder;
    private UIManager _uIManager;
    public GameState(UIManager uiManager, Pendulum pendulum, CircleCollector circleCollector, InputService inputService, StateMachine machine, IStateHolder stateHolder)
    {
        _pendulum = pendulum;
        _circleCollector = circleCollector;
        _inputService = inputService;
        _machine = machine;
        _stateHolder = stateHolder;
        _uIManager = uiManager;
    }

    public void Enter()
    {
        _circleCollector.ResetScore();
        _circleCollector.InitMatrix();     

        _uIManager.ShowWindow(eUIWindowType.Game);
        _inputService.OnMouseDown += InputService_OnMouseDown;
        _circleCollector.onFull += CircleCollector_onFull;
    }

    private void CircleCollector_onFull()
    {
        var newState = _stateHolder.TryGetState<ResultState>();
        if(newState != null) _machine.ChangeState(newState);
    }

    private void InputService_OnMouseDown()
    {
        _pendulum.ThrowCircle();
    }

    public void Exit()
    {
        _inputService.OnMouseDown -= InputService_OnMouseDown;
        _circleCollector.onFull -= CircleCollector_onFull;
    }

    public void Update()
    {
        
    }
}
