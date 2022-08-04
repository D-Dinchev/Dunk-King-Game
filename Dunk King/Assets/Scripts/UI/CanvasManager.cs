using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance { get; private set; }

    private GameObject _canvas;
    private GameObject _mainMenu;

    public GameObject MainMenu
    {
        get
        {
            return _mainMenu;
        }

        private set
        {
            _mainMenu = value;
        }
    }

    private Button _restartButton;

    private CanvasGroup _canvasGroup;

    private bool _inMainMenu = true;

    private void Awake()
    {
        Instance = this;

        _canvas = GameObject.FindGameObjectWithTag("Canvas");
        _mainMenu = _canvas.transform.Find("MainMenu").gameObject;
        _restartButton = _canvas.transform.Find("RestartButton").GetComponent<Button>();
        _canvasGroup = _canvas.transform.Find("Background").GetComponent<CanvasGroup>();
    }

    private void Start()
    { 
        GameEvents.Instance.OnRestart += FadeFromWhite;
    }

    private void Update()
    {
        if (!_inMainMenu && CheckEscapeClick())
        {
            EnableMainMenu();
            HideRestartButton();
        }
    }

    public void ShowRestartButton()
    {
        _restartButton.gameObject.SetActive(true); 
    }

    public void HideRestartButton()
    {
        _restartButton.gameObject.SetActive(false);
    }

    private void FadeFromWhite()
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.LeanAlpha(0f, .5f).setEase(LeanTweenType.easeInOutCubic);
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnRestart -= FadeFromWhite;
    }

    public void DisableMainMenu()
    {
        _mainMenu.LeanScale(Vector3.zero, 0.3f).setEase(LeanTweenType.easeInBack).setOnComplete((() => _mainMenu.SetActive(false)));
        _inMainMenu = false;
    }

    public void EnableMainMenu()
    {
        FadeFromWhite();
        GameEvents.Instance.RestartTrigger();
        _mainMenu.SetActive(true);
        _mainMenu.LeanScale(Vector3.one, 0.5f).setEase(LeanTweenType.easeInOutQuart);
        _inMainMenu = true;
    }


    private bool CheckEscapeClick()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }
}
