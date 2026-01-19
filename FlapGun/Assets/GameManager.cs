using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    None,
    Playing,
    GameOver
}



public sealed class GameManager : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private RoundManager roundManager;
    [SerializeField] private ScoreManager scoreManager;

    [Header("Scoring Rules")]
    [SerializeField] private int collisionScore = 1;

    [Header("Runtime (Read Only)")]
    [SerializeField] private GameState state = GameState.None;

    

   private void OnEnable()
{
    GameSignals.FruitDestroyed += OnFruitDestroyed;
}

private void OnDisable()
{
    GameSignals.FruitDestroyed -= OnFruitDestroyed;
}

private void OnFruitDestroyed(int collisionScore, bool roundClear)
{
    if (state != GameState.Playing) return;

    scoreManager.AddScore(collisionScore);

   
    if (roundClear)
        roundManager.RequestNextRound();

    
}

    public void StartGame()
    {
        if (state == GameState.Playing)
            return;

        if(roundManager == null || scoreManager == null)
        {
#if UNITY_EDITOR
        Debug.LogError("[GameManager] Missing references (roundManager / scoreManager).");
#endif
        return;
        }

        state = GameState.Playing;

        roundManager.StartFirstRound();
    }





    public void EndGame(GameOverReason reason)
    {
        if (state == GameState.GameOver)
        return;
        
        state = GameState.GameOver;
        
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
}