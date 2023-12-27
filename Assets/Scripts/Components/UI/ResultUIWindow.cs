using UnityEngine;
using UnityEngine.UI;

public class ResultUIWindow : UIWindow
{
    [field: SerializeField] public Button RestartButton { get; private set; }
    [field: SerializeField] public Button MenuButton { get; private set; }
    [field: SerializeField] public Text ResultText { get; private set; }
}
