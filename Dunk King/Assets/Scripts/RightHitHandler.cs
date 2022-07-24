using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHitHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameEvents.Instance.RightHitEventTrigger();
    }


}
