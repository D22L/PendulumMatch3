using System;
using UnityEngine;

public class InputService : MonoBehaviour
{
    public event Action OnMouseDown;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) OnMouseDown?.Invoke();
    }
}
