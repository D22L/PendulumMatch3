using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EntryPoint : MonoBehaviour, IStateHolder
{    
    private MenuState _menuState;
    private GameState _gameState;
    private ResultState _resultState;

    private StateMachine _stateMachine;
    private Pendulum _pendulum;
    private CircleCollector _circleCollector;
    private UIManager _uIManager;
    private InputService _inputService;

    private List<IState> _stateList;
    [Inject]
    private void Construct(Pendulum pendulum, CircleCollector circleCollector, UIManager uIManager, InputService inputService)
    {
        _pendulum = pendulum;
        _circleCollector = circleCollector;
        _uIManager = uIManager;
        _inputService = inputService;
    }

    private void Awake()
    {
        _stateMachine = new StateMachine();

        _stateList = new List<IState>();
        _menuState = new MenuState(_uIManager, _stateMachine, this);        
        _gameState = new GameState(_uIManager, _pendulum, _circleCollector, _inputService, _stateMachine, this);
        _resultState = new ResultState(_uIManager, _stateMachine, _circleCollector, _pendulum, this);

        _stateMachine.ChangeState(_menuState);

        _stateList.Add(_menuState);
        _stateList.Add(_gameState);
        _stateList.Add(_resultState);
    }

    public IState TryGetState<T>() where T : IState
    {
        return _stateList.Find(x=> x is T );
    }
}
