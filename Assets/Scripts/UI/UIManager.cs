using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public StartCanvas StartCanvas;
    public GamePlayCanvas GamePlayCanvas;
    public GameOverSuccessCanvas GameOverSuccessCanvas;
    public GameOverFailCanvas GameOverFailCanvas;
    public CurrencyCanvas CurrencyCanvas;

    protected override void Awake()
    {
        base.Awake();
        StartCanvas.gameObject.SetActive(true);
        GamePlayCanvas.gameObject.SetActive(false);
        GameOverSuccessCanvas.gameObject.SetActive(false);
        GameOverFailCanvas.gameObject.SetActive(false);
    }

    private void Start()
    {
        ServiceSystem.GetService<GameService>().OnGameStart += OnGameStart;
        ServiceSystem.GetService<GameService>().OnGameOver += OnGameFinish;
    }

    private void OnGameStart()
    {
        StartCanvas.gameObject.SetActive(false);
        CurrencyCanvas.gameObject.SetActive(false);
        GamePlayCanvas.gameObject.SetActive(true);
    }

    private void OnGameFinish(bool isGameSucceed)
    {
        StartCoroutine(Routine());
        IEnumerator Routine()
        {
            GamePlayCanvas.gameObject.SetActive(false);
            float waitTime = isGameSucceed ? 3:1;
            yield return new WaitForSeconds(waitTime);
            GameOverSuccessCanvas.gameObject.SetActive(isGameSucceed);
            GameOverFailCanvas.gameObject.SetActive(!isGameSucceed);
            CurrencyCanvas.gameObject.SetActive(isGameSucceed);
        }
        
    }
}
