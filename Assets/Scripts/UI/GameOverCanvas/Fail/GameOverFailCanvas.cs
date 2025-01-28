using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameOverFailCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject ReviveGroup, FailGroup;
    [SerializeField]
    private float countdownTime;
    [SerializeField]
    private TextMeshProUGUI countdownText,reviveText;
    [SerializeField]
    private Image countdownFillImage;
    [SerializeField]
    private Button noThanksButton, reviveButton,continueButton;
    private float startTime;
    private bool isCountDown;

    // Start is called before the first frame update
    void OnEnable()
    {
        ReviveGroup.SetActive(true);
        FailGroup.SetActive(false);
        reviveText.transform.DOScale(1.25f, 1f).SetLoops(-1, LoopType.Yoyo);
        noThanksButton.onClick.AddListener(OnThanksButtonTap);
        noThanksButton.gameObject.SetActive(false);
        startTime = Time.time;
        isCountDown = true;
    }

    private void Update()
    {
        Countdown();
    }

    private void Countdown()
    {
        if (!isCountDown) return;

        float currentTime = (startTime + countdownTime) - Time.time;

        if (currentTime < 0)
        {
            OnThanksButtonTap();
            return;
        }

        countdownFillImage.fillAmount = Mathf.InverseLerp(1, 0, currentTime / countdownTime);

        TimeSpan t = TimeSpan.FromSeconds(currentTime);
        countdownText.text = t.Seconds.ToString("0");

        if (currentTime < countdownTime - 1)
        {
            noThanksButton.gameObject.SetActive(true);
        }
    }
    private void OnThanksButtonTap()
    {
        // isCountDown = false;
        // reviveText.transform.DOKill();
        // GameManager.Instance.LoadScene();
    }
    
    private void OnContinueButtonTap()
    {
       // GameManager.Instance.LoadScene();
    }
}
