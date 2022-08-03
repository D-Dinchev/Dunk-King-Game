using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButtonHandler : MonoBehaviour
{

    private void Start()
    {
        GameEvents.Instance.OnDead += Appear;
        GameEvents.Instance.OnRestart += Close;
        transform.localScale = Vector3.zero;
        CanvasManager.Instance.HideRestartButton();
    }

    private void Appear()
    {
        CanvasManager.Instance.ShowRestartButton();
        transform.LeanScale(Vector2.one, 0.4f).setEase(LeanTweenType.easeInOutBack);
    }

    private void Close()
    {
        transform.LeanScale(Vector3.zero, 0.3f).setEase(LeanTweenType.easeInBack).setOnComplete(CanvasManager.Instance.HideRestartButton);
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnDead -= Appear;
        GameEvents.Instance.OnRestart -= Close;

    }
}
