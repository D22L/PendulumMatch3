using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider2D))]
public class CollectZoneTrigger : MonoBehaviour
{
    private BoxCollider2D _trigger;
    private Action<int, int, int> onEnterCallback;
    
    private int _maxCount = 3;
    private int _currentCount = 0;
    private int _columNumber;
    private List<CircleView> _circleViews;

    public void Init(int columNumber, Action<int, int, int> OnCircleEnterCallback)
    {
        _columNumber = columNumber;
        _trigger = GetComponent<BoxCollider2D>();
        onEnterCallback = OnCircleEnterCallback;
        _circleViews = new List<CircleView>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_currentCount >= _maxCount) return;

        if (collision.TryGetComponent(out CircleView circleView))
        {
            _currentCount++;           
            circleView.SetMatrixPos(new Vector2(_currentCount - 1, _columNumber));
            _circleViews.Add(circleView);

            StartCoroutine(SendCallbackAfterDelay(_columNumber, _currentCount - 1, circleView.ColorIndex));
        }
    }

    private IEnumerator SendCallbackAfterDelay(int column, int row, int colorIndex)
    {
        yield return new WaitForSeconds(0.5f);
        onEnterCallback?.Invoke(column,row, colorIndex);
    }
    
    public void DestroyCircle(Vector2 pos)
    {
        for (int i = 0; i < _circleViews.Count; i++)
        {
            if (pos == _circleViews[i].PosInMatrix)
            {
                _circleViews[i].Destroy();
                _circleViews.Remove(_circleViews[i]);
                _currentCount--;
            }
        }
    }

    public void TrySetNewPositionForCircle(Vector2 circleOldPos, Vector2 circleNewPos)
    {
        var circle = _circleViews.Find(x=> x.PosInMatrix == circleOldPos);
        if (circle != null)
        {
            circle.SetMatrixPos(circleNewPos);
        }
    }

    public void Clear()
    {
        _circleViews.Clear();
        _currentCount = 0;
    }
}
