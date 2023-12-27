using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameRewardInstaller", menuName = "Installers/GameRewardInstaller")]
public class GameRewardInstaller : ScriptableObjectInstaller<GameRewardInstaller>
{
    [SerializeField] private RewardConfig _rewardConfig;
    public override void InstallBindings()
    {
        Container.Bind<RewardConfig>().FromInstance(_rewardConfig).AsSingle().NonLazy();
    }
}