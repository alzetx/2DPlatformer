using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField]
    private Button _startButton;

    [SerializeField]
    private Button _exitButton;

    public event UnityAction _onClickStart
    {
        add { _startButton.onClick.AddListener(value); }
        remove { _startButton.onClick.RemoveListener(value); }
    }
    public event UnityAction _onClickExit
    {
        add { _exitButton.onClick.AddListener(value); }
        remove { _exitButton.onClick.RemoveListener(value); }
    }
}