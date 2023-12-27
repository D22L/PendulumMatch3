using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : IState
{
    private UIManager _uiManager;
    private MenuUIWindow _menuWindow;
    private StateMachine _stateMachine;
    private IStateHolder _stateHolder;
    private CircleAnimPool _circleAnimPool;
    public MenuState(UIManager uIManager, StateMachine stateMachine, IStateHolder stateHolder)
    {
        _uiManager = uIManager;
        _stateMachine = stateMachine;
        _stateHolder = stateHolder;
    }

    public void Enter()
    {
        _menuWindow = (MenuUIWindow)_uiManager.ShowWindow(eUIWindowType.Menu);
        _menuWindow.PlayButton.onClick.AddListener(Play);

        _circleAnimPool = MonoBehaviour.FindObjectOfType<CircleAnimPool>();
        if (_circleAnimPool != null) _circleAnimPool.Pool();
    }

    private void Play()
    {
        var state = _stateHolder.TryGetState<GameState>();
        if(state!=null) _stateMachine.ChangeState(state);

    }

    public void Exit()
    {
        _menuWindow.PlayButton.onClick.RemoveListener(Play);
        if (_circleAnimPool != null) _circleAnimPool.DestroyAllCircle();
    }

    public void Update()
    {
       
    }
}
