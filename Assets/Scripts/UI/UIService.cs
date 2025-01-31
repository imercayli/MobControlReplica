using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIService : BaseService<UIService>
{
    [SerializeField] private GamePlayCanvas gamePlayCanvas;
    [SerializeField] private GameOverCanvas gameOverCanvas;

    public GamePlayCanvas GamePlayCanvas => gamePlayCanvas;
    public GameOverCanvas GameOverCanvas => gameOverCanvas;

    public override void Start()
    {
        base.Start();
        gameOverCanvas.gameObject.SetActive(false);
        ServiceSystem.GetService<GameService>().OnGameOver += OnGameFinish;
    }

    private void OnGameFinish(bool isGameSucceed)
    {
        StartCoroutine(Routine());
        IEnumerator Routine()
        {
            float waitTime = isGameSucceed ? 1 : 0.5f;
            yield return new WaitForSeconds(waitTime);
            gameOverCanvas.gameObject.SetActive(true);
            gameOverCanvas.Initialize(isGameSucceed);
        }
        
    }
}
