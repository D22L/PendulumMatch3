using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PendulumView : MonoBehaviour
{
    [SerializeField] private Transform _circlePoint;            
    [SerializeField] private CircleView _circleViewPfb;
    [SerializeField] private float _delayForCreate = 1f;

    [Inject] private RewardConfig _rewardConfig;

    private CircleView _currentCircle;
    private List<CircleView> _createdCircles = new List<CircleView>();

    private void Awake()
    {
        CreateNewCircle();
    }

    public void ThrowCircle()
    {
        if (_currentCircle != null)
        {
            _currentCircle.Undocking();
            _currentCircle = null;
            StartCoroutine(CreateAfterDelay());
        }
    }

    public void DestroyCreated()
    {
        _createdCircles.ForEach(x => { if (x != null && x != _currentCircle) Destroy(x.gameObject); });
        _createdCircles.Clear();
        _createdCircles.Add(_currentCircle);
    }

    private IEnumerator CreateAfterDelay()
    {
        yield return new WaitForSeconds(_delayForCreate);
        CreateNewCircle();
    }

    public void CreateNewCircle()
    {
        var r = Random.Range(0, _rewardConfig.RewardData.Count);
        var rewardData = _rewardConfig.RewardData[r];
        _currentCircle = Instantiate(_circleViewPfb, _circlePoint);
        _currentCircle.transform.localPosition = Vector3.zero;
        _currentCircle.Init(rewardData.ColorValue, r);
        _createdCircles.Add(_currentCircle);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
