using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameService : BaseService<GameService>
{
    public bool IsGameOver { get; private set; }
    
    public UnityAction OnGameStart;
    public UnityAction<bool> OnGameOver;
    
    public void StartGame()
    {
        OnGameStart?.Invoke();
    }

    public void GameOver(bool isGameSucceed)
    {
        IsGameOver = true;
        OnGameOver?.Invoke(isGameSucceed);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    
}
