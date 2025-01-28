using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameService : BaseService<GameService>
{
    private bool isGameSucceed;
    
    public UnityAction OnGameStart;
    public UnityAction<bool> OnGameOver;
    
    public void StartGame()
    {
        OnGameStart?.Invoke();
    }

    public void GameOver(bool isGameSucceed)
    {
        this.isGameSucceed = isGameSucceed;
        OnGameOver?.Invoke(isGameSucceed);
    }
    
}
