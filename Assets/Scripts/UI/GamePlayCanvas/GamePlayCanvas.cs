using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Extentions;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GamePlayCanvas : MonoBehaviour
{
    private Camera mainCam;
    private Canvas canvas;
    [SerializeField] private RectTransform levelUpText;
    [SerializeField] private GameObject helpUsBalloon;

    private void Start()
    {
        mainCam = Camera.main;
        canvas = GetComponent<Canvas>();
        ShowHelpUsBalloon();
    }

    public void ShowLevelUpText(Vector3 canonPosition)
    {
        StartCoroutine(Routine());
        IEnumerator Routine()
        {
            levelUpText.gameObject.SetActive(true);
            levelUpText.anchoredPosition = canonPosition.WorldToCanvasPosition(mainCam, canvas);
            float anchoredPositionY = levelUpText.anchoredPosition.y;
            float time = .5f;
            levelUpText.DOAnchorPosY(anchoredPositionY + 400, time);
            levelUpText.transform.localScale = Vector3.zero;
            levelUpText.transform.DOScale(Vector3.one, time);
            yield return new WaitForSeconds(0.75f);
            levelUpText.DOAnchorPosY(anchoredPositionY, time);
            levelUpText.transform.DOScale(Vector3.zero, time);
        }
    }

    private void ShowHelpUsBalloon()
    {
        StartCoroutine(Routine());
        IEnumerator Routine()
        {
            float delayTime = 10f;
            yield return new WaitForSeconds(delayTime);
            helpUsBalloon.SetActive(true);
            helpUsBalloon.transform.DOScale(1, 0.5f).From(0);
            float stayTime = 4f;
            yield return new WaitForSeconds(stayTime);
            helpUsBalloon.SetActive(false);
        }
    }
}
