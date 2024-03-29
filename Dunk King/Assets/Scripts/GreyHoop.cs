using System.Collections;
using UnityEngine;

public class GreyHoop : MonoBehaviour
{
    private void Start()
    {
        GameEvents.Instance.OnRightHit += Destroy;
    }

    private void Destroy(OnRightHitEventArgs args)
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnRightHit -= Destroy;
    }
}
