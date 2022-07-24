using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedHoop : MonoBehaviour
{
    public GameObject GreyHoopPrefab;

    private void Start()
    {
        GameEvents.Instance.OnRightHit += ChangeHoopAndItsPosition;
        //StartCoroutine(LerpPosition(transform.position + Vector3.down * 2.5f, 1.5f));
    }

    
    /*IEnumerator LerpPosition(Vector2 endPosition, float duration)
    {
        GameObject go = Instantiate(GreyHoopPrefab, transform.position + Vector3.up * 6f + Vector3.right * -2f, Quaternion.identity);
        float time = 0f;
        Vector2 startPosition = transform.position;
        Vector2 goSP = go.transform.position;
        while (time < duration)
        {
            transform.position = Vector2.Lerp(startPosition, endPosition, time / duration);
            go.transform.position = Vector2.Lerp(goSP, endPosition + Vector2.up * 2f + Vector2.right * -2f, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;
    }*/
    private void ChangeHoopAndItsPosition()
    {
        Vector2 oldPosition = transform.position;

        if (GreyHoopPrefab)
        {
            Instantiate(GreyHoopPrefab, oldPosition, Quaternion.identity);
            Destroy(gameObject);
            
        }
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnRightHit -= ChangeHoopAndItsPosition;
    }
}
