using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public event Action OnRightHit;
    public void RightHitEventTrigger()
    {
        OnRightHit?.Invoke();
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
}
