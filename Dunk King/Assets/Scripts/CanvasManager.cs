using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    private GameObject _canvas;
    private Button _restartButton;

    private void Start()
    {
        _canvas = GameObject.FindGameObjectWithTag("Canvas");
        _restartButton = _canvas.transform.Find("RestartButton").GetComponent<Button>();

        GameEvents.Instance.OnDead += ShowRestartButton;
        GameEvents.Instance.OnRestart += HideRestartButton;
    }

    private void ShowRestartButton()
    {
        _restartButton.gameObject.SetActive(true); 
    }

    private void HideRestartButton()
    {
        _restartButton.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnDead -= ShowRestartButton;
        GameEvents.Instance.OnRestart -= HideRestartButton;
    }
}
