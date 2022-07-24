using UnityEngine;

public class HoopGeneratorHandler : MonoBehaviour
{
    public GameObject RedHoopPrefab;

    private float _minYDifference = 1f, _maxYDifference = 3f;
    //private float _maxRotationAnge = 34f;

    private Transform _bounds;
    private Vector3 _boundsCenter;
    private Vector3 _rightCornerOfBounds;
    private Vector3 _leftCornerOfBounds;
    private float _ySpawnPosition;

    private float _halfOfHoopWidth;
    private float _minDistanceBetweenHoops;
    

    private void Start()
    {
        _bounds = GameObject.FindGameObjectWithTag("Bounds").transform;
        _boundsCenter = _bounds.transform.Find("Center").position;
        _rightCornerOfBounds = _bounds.Find("Right").GetComponent<SpriteRenderer>().bounds.min;
        _leftCornerOfBounds = _bounds.Find("Left").GetComponent<SpriteRenderer>().bounds.max;
        GameObject topOfBounds = _bounds.transform.Find("Top").gameObject;

        var sr = RedHoopPrefab.transform.Find("front").GetComponent<SpriteRenderer>();
        _halfOfHoopWidth = sr.bounds.extents.x;
        _minDistanceBetweenHoops = _halfOfHoopWidth * 2;

        GameEvents.Instance.OnRightHit += GenerateRedHoop;
    } 
    private void GenerateRedHoop() // TODO
    {
        Transform currentRedHoop = GameObject.FindGameObjectWithTag("Red").transform;

        float minXPosition = currentRedHoop.position.x + _minDistanceBetweenHoops;
        float maxXPosition = _rightCornerOfBounds.x - _halfOfHoopWidth;

        float positionX = Random.Range(minXPosition, maxXPosition);
        float positionY = Random.Range(currentRedHoop.position.y + _minYDifference, currentRedHoop.position.y + _maxYDifference);

        if (currentRedHoop.position.x > _boundsCenter.x) // to the left of bounds center
        {
            minXPosition = _leftCornerOfBounds.x + _halfOfHoopWidth;
            maxXPosition = currentRedHoop.position.x - _minDistanceBetweenHoops;

            positionX = Random.Range(maxXPosition, minXPosition);
        }

        if (RedHoopPrefab)
        {
            
            Instantiate(RedHoopPrefab, new Vector3(positionX, positionY, 0), Quaternion.identity);
        }
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnRightHit -= GenerateRedHoop;
    }
}
