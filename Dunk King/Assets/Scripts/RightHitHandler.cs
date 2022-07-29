using UnityEngine;

public class RightHitHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        OnRightHitEventArgs args = new OnRightHitEventArgs();
        args.AudioTitle = "Right Hit";
        GameEvents.Instance.RightHitEventTrigger(args);
    }

}
