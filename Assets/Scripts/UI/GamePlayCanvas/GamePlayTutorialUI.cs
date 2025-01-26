using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GamePlayTutorialUI : MonoBehaviour
{
    [SerializeField]
    private GameObject swipeText;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     if (isTutorialShown)
    //     {
    //         gameObject.SetActive(false);
    //         return;
    //     }
    //
    //     swipeText.transform.DOScale(1.25f, 1f).SetLoops(-1, LoopType.Yoyo);
    //     TouchManager.Instance.onTouchMoved += OnTouchMove;
    // }
    //
    // private void OnTouchMove(TouchInput touch)
    // {
    //     swipeText.transform.DOKill();
    //     isTutorialShown = true;
    //     TouchManager.Instance.onTouchMoved -= OnTouchMove;
    //     gameObject.SetActive(false);
    // }
    //
    // private const string tutorialKey = "swipeTutorialKey";
    // private bool isTutorialShown
    // {
    //     get => PlayerPrefs.GetInt(tutorialKey, 0) == 1;
    //     set => PlayerPrefs.SetInt(tutorialKey, value ? 1 : 0);
    // }
}
