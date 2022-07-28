using System.Collections;
using UnityEngine;

public class HoopGeneratorHandler : MonoBehaviour
{
    public static HoopGeneratorHandler Instance { get; private set; }

    public GameObject RedHoopPrefab;
    public GameObject GreyHoopPrefab;

    private float _minYDifference = 1f, _maxYDifference = 3.5f;
    private float _maxRotationAnge = 34f;

    private Transform _bounds;

    private Vector3 _boundsCenter;
    private Vector3 _startPosition;
    private Vector3 _rightCornerOfBounds;
    private Vector3 _leftCornerOfBounds;

    private float _ySpawnPoint;

    private float _halfOfHoopWidth;
    private float _minDistanceBetweenHoops;

    private BallController _ballController;

    private void Awake()
    {
        Instance = this;
        _bounds = GameObject.FindGameObjectWithTag("Bounds").transform;
        _startPosition = _bounds.transform.Find("StartPosition").position;
    }

    private void Start()
    {
        _boundsCenter = _bounds.transform.Find("Center").position;
        _ySpawnPoint = _bounds.transform.Find("YSpawnPoint").position.y;
        _rightCornerOfBounds = _bounds.Find("Right").GetComponent<SpriteRenderer>().bounds.min;
        _leftCornerOfBounds = _bounds.Find("Left").GetComponent<SpriteRenderer>().bounds.max;
        GameObject topOfBounds = _bounds.transform.Find("Top").gameObject;

        _ballController = GameObject.FindGameObjectWithTag("Player").GetComponent<BallController>();

        var sr = RedHoopPrefab.transform.Find("front").GetComponent<SpriteRenderer>();
        _halfOfHoopWidth = sr.bounds.extents.x;
        _minDistanceBetweenHoops = _halfOfHoopWidth * 2;

        GameEvents.Instance.OnRightHit += GenerateRedHoop;
    }

    public void StartGeneration()
    {
        GameObject startGreyHoop = Instantiate(GreyHoopPrefab, _startPosition, Quaternion.identity);
        Instantiate(RedHoopPrefab, _boundsCenter + Vector3.up + Vector3.left / 2, Quaternion.identity);
        
        GameObject ball = GameObject.FindGameObjectWithTag("Player");
        ball.transform.position = startGreyHoop.transform.Find("front").position;
    }
    private void GenerateRedHoop() // TODO
    {
        Transform currentRedHoop = GameObject.FindGameObjectWithTag("Red").transform;

        float minXPosition = currentRedHoop.position.x + _minDistanceBetweenHoops;
        float maxXPosition = _rightCornerOfBounds.x - _halfOfHoopWidth;

        float positionX = Random.Range(minXPosition, maxXPosition);
        float positionY = Random.Range(_startPosition.y + _minYDifference, _startPosition.y + _maxYDifference);
        float rotationZ = Random.Range(0f, _maxRotationAnge); // left side

        if (currentRedHoop.position.x > _boundsCenter.x) // to the left of bounds center
        {
            minXPosition = _leftCornerOfBounds.x + _halfOfHoopWidth;
            maxXPosition = currentRedHoop.position.x - _minDistanceBetweenHoops;

            positionX = Random.Range(maxXPosition, minXPosition);
            rotationZ *= -1f; // right side
        }

        if (RedHoopPrefab && GreyHoopPrefab)
        {
            GameObject newRedHoop = Instantiate(RedHoopPrefab, new Vector3(positionX, _ySpawnPoint, 0), Quaternion.Euler(0f, 0f, rotationZ));
            GameObject newGreyHoop = Instantiate(GreyHoopPrefab, currentRedHoop.position, Quaternion.identity);
            Destroy(currentRedHoop.gameObject);

            StartCoroutine(LerpYPoint(newGreyHoop.transform, _startPosition.y, 1f));
            StartCoroutine(LerpYPoint(newRedHoop.transform, positionY, 1f));
        }
    }

    private IEnumerator LerpYPoint(Transform objPosition, float endPosition, float duration)
    {
        float time = 0f;
        Vector2 startPosition = objPosition.position;

        _ballController.StickToHoop(objPosition.gameObject);

        while (time < duration)
        {
            float newY = Mathf.Lerp(startPosition.y, endPosition, time / duration);
            objPosition.position = new Vector3(objPosition.position.x, newY, transform.position.z);
            time += Time.deltaTime;
            yield return null;
        }

        objPosition.position = new Vector3(objPosition.position.x, endPosition, transform.position.z);
        _ballController.UnstickToHoop();
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnRightHit -= GenerateRedHoop;
    }
}
