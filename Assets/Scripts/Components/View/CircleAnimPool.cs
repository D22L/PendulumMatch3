using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CircleAnimPool : MonoBehaviour
{
    [SerializeField] private CircleView _circleViewPfb;
    [SerializeField] private int _count = 10;

    [Inject] private RewardConfig _rewardConfig;

    private List<CircleView> _createdCircles = new List<CircleView>();
    public void Pool()
    {
        StartCoroutine(SpawnWithDelay());
    }

    private IEnumerator SpawnWithDelay()
    {
        while (_createdCircles.Count < _count)
        {
            var r = Random.Range(0, _rewardConfig.RewardData.Count);
            var rewardData = _rewardConfig.RewardData[r];
            var circle = Instantiate(_circleViewPfb, transform);
            circle.transform.localPosition = new Vector3(Random.Range(-1,1f),0);
            circle.Undocking();           
            circle.Init(rewardData.ColorValue, r);
            _createdCircles.Add(circle);
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void DestroyAllCircle()
    {
        StopCoroutine(SpawnWithDelay());
        _createdCircles.ForEach(x => { if (x != null) Destroy(x.gameObject); });
        _createdCircles.Clear();
    }
}
