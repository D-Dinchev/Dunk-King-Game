using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    private void OnTriggerExit2D()
    {
        GameEvents.Instance.DieTrigger();
    }
}
