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
}
