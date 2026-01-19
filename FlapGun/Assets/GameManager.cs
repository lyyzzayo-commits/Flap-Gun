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

    private void Awake()
    {
        StartGame();
    }

    private void OnEnable()
    {
        GameSignals.FruitHit += OnFruitDestroyed;
    }

    private void OnDisable()
    {
        GameSignals.FruitHit -= OnFruitDestroyed;
    }

    private void OnFruitDestroyed(FruitTarget fruit)
    {
        if (state != GameState.Playing) return;

        scoreManager.AddScore(collisionScore);
        roundManager.RequestNextRound();
    }

    public void StartGame()
    {
        if (state == GameState.Playing)
            return;

        if (roundManager == null || scoreManager == null)
        {
#if UNITY_EDITOR
            Debug.LogError("[GameManager] Missing references (roundManager / scoreManager).");
#endif
            return;
        }

        state = GameState.Playing;
        roundManager.StartFirstRound();
    }

    public void EndGame()
    {
        if (roundManager.remainingTime != 0f)
            return;

        if (roundManager.remainingTime == 0f)
        {
            roundManager.TriggerGameOver();
        }
        
        if (state == GameState.GameOver)
            return;

        

        state = GameState.GameOver;

        Time.timeScale = 0f;
        GameSignals.RaiseGameOver();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
