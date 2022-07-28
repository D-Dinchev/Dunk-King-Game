using UnityEngine;

public class BallCalmHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameEvents.Instance.CalmBallDownTrigger();
    }
}
