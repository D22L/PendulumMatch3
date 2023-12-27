using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardConfig", menuName = "Configs/RewardConfig")]
public class RewardConfig : ScriptableObject
{
    [SerializeField] private List<RewardData> _rewardDatas;

    public IReadOnlyList<RewardData> RewardData => _rewardDatas;
}

[System.Serializable]
public class RewardData
{
    [field: SerializeField] public Color ColorValue { get; private set; }
    [field: SerializeField] public uint RewardSize { get; private set; }

}
