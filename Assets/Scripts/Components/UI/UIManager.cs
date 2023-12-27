using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<UIWindow> _uiWindows;

    private UIWindow _currentWindow;

    public UIWindow ShowWindow(eUIWindowType windowType)
    {
        _currentWindow?.Hide();
        _currentWindow = _uiWindows.Find(x=>x.WindowType == windowType);
        _currentWindow?.Show();

        return _currentWindow;
    }
}
