using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public UnityAction OnGameStart;
    public UnityAction<bool> OnGameOver;
    private bool isGameSucceed;
    public UnityAction OnRevive;
    public bool IsRevived { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    public void StartGame()
    {
        OnGameStart?.Invoke();
    }

    public void GameOver(bool isGameSucceed)
    {
        this.isGameSucceed = isGameSucceed;
        OnGameOver?.Invoke(isGameSucceed);
        Debug.Log("over");
    }

    public void Revive()
    {
        IsRevived = true;
        OnRevive?.Invoke();
    }

    public void LoadScene()
    {
        if (isGameSucceed)
            SaveData.GameLevel++;

        SceneManager.LoadScene("GameScene");
    }
}