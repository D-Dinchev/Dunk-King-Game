using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public event Action<OnRightHitEventArgs> OnRightHit;
    public void RightHitEventTrigger(OnRightHitEventArgs args)
    {
        OnRightHit?.Invoke(args);
    }

    public event Action OnCalmBallDown;
    public void CalmBallDownTrigger()
    {
        OnCalmBallDown?.Invoke();
    }

    public event Action OnDead;
    public void DieTrigger()
    {
        OnDead?.Invoke();
    }

    public event Action OnRestart;

    public void RestartTrigger()
    {
        OnRestart?.Invoke();
    }

    public event Action OnAppQuitOrPause;

    public void AppQuitOrPauseTrigger()
    {
        OnAppQuitOrPause?.Invoke();
    }

    public event Action OnNewHighScore;

    public void NewHighScoreTrigger()
    {
        OnNewHighScore?.Invoke();
    }
}

public class OnRightHitEventArgs : EventArgs
{
    public string AudioTitle { get; set; }
}
