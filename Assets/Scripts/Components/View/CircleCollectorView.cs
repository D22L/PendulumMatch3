using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCollectorView : MonoBehaviour
{
    [SerializeField] private List<CollectZoneTrigger> _collectZoneTriggers;

    public void Init(Action<int, int, int> OnCircleEnterCallback)
    {
        for (int i = 0; i < _collectZoneTriggers.Count; i++)
        {
            _collectZoneTriggers[i].Init(i, OnCircleEnterCallback);
        }
    }

    public void DestroyCircle(List<Vector2> circlePosInMatrix)
    {
        for (int i = 0; i < circlePosInMatrix.Count; i++)
        {
            var colum = (int)circlePosInMatrix[i].y;
            _collectZoneTriggers[colum].DestroyCircle(circlePosInMatrix[i]);
        }
    }

    public void DestroyAllCircle()
    {
        _collectZoneTriggers.ForEach(x => x.Clear());
    }

    public void SetNewPositionForCirlce(Vector2 circleOldPos, Vector2 circleNewPos)
    {
        var colum = (int)circleOldPos.y;
        _collectZoneTriggers[colum].TrySetNewPositionForCircle(circleOldPos, circleNewPos);
    }
}
