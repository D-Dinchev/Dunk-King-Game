using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreAnimation : MonoBehaviour
{
    private bool _showAnim;
    public bool ShowAnim
    {
        get
        {
            return _showAnim;
        }

        private set
        {
            _showAnim = value;
        }
    }

    private void Start()
    {
        _showAnim = true;
        GameEvents.Instance.OnRestart += SetShowAnimToTrue;

        GameEvents.Instance.OnNewHighScore += PlayHighScoreAnimation;

        gameObject.SetActive(false);
    }

    private void PlayHighScoreAnimation()
    {
        if (_showAnim)
        {
            gameObject.SetActive(true);
            transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
            transform.localScale *= 2f;
            transform.LeanScale(Vector3.one, 1f).setEase(LeanTweenType.easeInOutQuart);
            transform.LeanMoveX(transform.position.x - 0.25f, 0.4f).setEase(LeanTweenType.easeOutQuad).setDelay(1f);
            transform.LeanMoveX(transform.position.x + 4f, 0.7f).setEase(LeanTweenType.easeOutCirc).setDelay(1.4f).setOnComplete(() => gameObject.SetActive(false));

            _showAnim = false;
        }
    }

    private void SetShowAnimToTrue()
    {
        _showAnim = true;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnRestart -= SetShowAnimToTrue;

        GameEvents.Instance.OnNewHighScore -= PlayHighScoreAnimation;
    }
}
