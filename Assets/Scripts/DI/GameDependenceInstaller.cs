using UnityEngine;
using Zenject;

public class GameDependenceInstaller : MonoInstaller
{
    [SerializeField] private PendulumView _pendulumView;
    [SerializeField] private CircleCollectorView _collectorView;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private InputService _inputService;
    
    [Inject] private RewardConfig _rewardConfig;
    public override void InstallBindings()
    {
        Pendulum pendulum = new Pendulum(_pendulumView);
        Container.Bind<Pendulum>().FromInstance(pendulum).AsSingle().NonLazy();

        CircleCollector circleCollector = new CircleCollector(_collectorView, _rewardConfig);
        Container.Bind<CircleCollector>().FromInstance(circleCollector).AsSingle().NonLazy();

        Container.Bind<UIManager>().FromInstance(_uiManager).AsSingle().NonLazy();

        Container.Bind<InputService>().FromInstance(_inputService).AsSingle().NonLazy();


    }
}