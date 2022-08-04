using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private GameObject _canvas;
    private TextMeshProUGUI _scoreText;

    private int _score = 0;

    public int Score
    {
        get
        {
            return _score;
        }

        private set
        {
            _score = value;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        HoopGeneratorHandler.Instance.StartGeneration();
        _canvas = GameObject.FindGameObjectWithTag("Canvas");
        _scoreText = _canvas.transform.Find("Score").gameObject.GetComponent<TextMeshProUGUI>();

        GameEvents.Instance.OnRightHit += IncreaseScore;
        GameEvents.Instance.OnRestart += Restart;
    }
    private void IncreaseScore(OnRightHitEventArgs args)
    {
        _score++;
        _scoreText.text = _score.ToString();
    }

    public void Restart()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<BallController>().UnstickToHoop();
        Destroy(GameObject.FindGameObjectWithTag("Grey"));
        Destroy(GameObject.FindGameObjectWithTag("Red"));

        _score = 0;
        _scoreText.text = _score.ToString();
        HoopGeneratorHandler.Instance.StartGeneration();
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnRightHit -= IncreaseScore;
        GameEvents.Instance.OnRestart -= Restart;
    }

    private static void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Instance.SaveAllData();
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            Instance.SaveAllData();
        }
    }

    private void OnApplicationQuit()
    {
        Instance.SaveAllData();
    }

    private void SaveAllData()
    {
        GameEvents.Instance.AppQuitOrPauseTrigger();
    }

}
