using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultState : IState
{
    private UIManager _uiManager;
    private ResultUIWindow _resultWindow;
    private StateMachine _stateMachine;
    private IStateHolder _stateHolder;
    private CircleCollector _circleCollector;
    private Pendulum _pendulum;
    public ResultState(UIManager uIManager, StateMachine stateMachine, CircleCollector circleCollector, Pendulum pendulum, IStateHolder stateHolder)
    {
        _uiManager = uIManager;
        _stateMachine = stateMachine;
        _stateHolder = stateHolder;
        _circleCollector = circleCollector;
        _pendulum = pendulum;
    }

    public void Enter()
    {
        _resultWindow = (ResultUIWindow)_uiManager.ShowWindow(eUIWindowType.Result);
        _resultWindow.ResultText.text = _circleCollector.ResultScore.ToString();
        _resultWindow.RestartButton.onClick.AddListener(Restart);
        _resultWindow.MenuButton.onClick.AddListener(GoToMenu);
    }
    
    public void Exit()
    {
        _resultWindow.RestartButton.onClick.RemoveListener(Restart);
        _resultWindow.MenuButton.onClick.RemoveListener(GoToMenu);
    }

    public void Update()
    {
        
    }

    private void Restart()
    {
        var newState = _stateHolder.TryGetState<GameState>();
        if (newState != null)
        {
            _pendulum.DestroyCircles();
            _stateMachine.ChangeState(newState);
        }
    }   

    private void GoToMenu()
    {
        var newState = _stateHolder.TryGetState<MenuState>();
        if (newState != null)
        {
            _pendulum.DestroyCircles();
            _stateMachine.ChangeState(newState);
        }
    }


}
