using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaxScoreHandler : MonoBehaviour
{
    public static MaxScoreHandler Instance { get; private set; }

    public TextMeshProUGUI _MaxScoreText;

    private int _maxScore;

    private const string _saveKey = "MaxScoreSave";

    private void Awake()
    {
        Instance = this;
        Load();
    }

    private void Start()
    {
        SetMaxScoreOnUI();
        GameEvents.Instance.OnRightHit += CheckAndSetForMaxScore;

        GameEvents.Instance.OnAppQuitOrPause += Save;
    }

    

    private void OnEnable()
    {
        SetMaxScoreOnUI();
    }

    private void Load()
    {
        var data = SaveManager.Load<SaveData.MaxScore>(_saveKey);
        _maxScore = data.MaxScoreValue;
    }

    public void Save()
    {
        SaveManager.Save(_saveKey, GetSaveSnapshot());
    }

    private SaveData.MaxScore GetSaveSnapshot()
    {
        var data = new SaveData.MaxScore()
        {
            MaxScoreValue = _maxScore
        };

        return data;
    }

    private void SetMaxScoreOnUI()
    {
        if (_MaxScoreText)
        {
            _MaxScoreText.text = "MAX SCORE: " +  _maxScore.ToString();
        }
    }

    private void CheckAndSetForMaxScore(OnRightHitEventArgs args)
    {
        CanvasManager.Instance.MainMenu.SetActive(true); // set zero scale to make it invisible and set active cause the coroutine
        StartCoroutine(WaitForNextFrameThenDo());
    }

    private IEnumerator WaitForNextFrameThenDo()
    {
        yield return null;

        if (GameManager.Instance.Score > _maxScore)
        {
            _maxScore = GameManager.Instance.Score;
        }

        CanvasManager.Instance.MainMenu.SetActive(false);
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnRightHit -= CheckAndSetForMaxScore;

        GameEvents.Instance.OnAppQuitOrPause -= Save;
    }
}
