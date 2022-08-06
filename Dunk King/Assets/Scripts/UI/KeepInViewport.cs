using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepInViewport : MonoBehaviour
{
    private Vector2 _screenBounds;

    private GameObject _leftWall;
    private GameObject _rightWall;
    private GameObject _bottomWall;

    private float _wallWidth;

    private void Awake()
    {
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)); // half's of screen size
        _leftWall = transform.Find("Left").gameObject;
        _rightWall = transform.Find("Right").gameObject;
        _bottomWall = transform.Find("Down").gameObject;

        _wallWidth = _leftWall.GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    private void Start()
    {
        _leftWall.transform.position = new Vector3(-1f * _screenBounds.x - _wallWidth, _leftWall.transform.position.y, _leftWall.transform.position.z);
        _rightWall.transform.position = new Vector3(_screenBounds.x + _wallWidth, _rightWall.transform.position.y, _rightWall.transform.position.z);

        _bottomWall.transform.parent = null;
        _bottomWall.transform.localScale = new Vector3(_bottomWall.transform.localScale.x, _screenBounds.x * 2f, _bottomWall.transform.localScale.z);
        _bottomWall.transform.parent = transform;

        float middleXPoint = ((_leftWall.transform.position.x + _wallWidth) + (_rightWall.transform.position.x - _wallWidth)) / 2f;
        _bottomWall.transform.position = new Vector3(middleXPoint, _bottomWall.transform.position.y, _bottomWall.transform.position.z);
    }

    private void LateUpdate()
    {

    }
}
