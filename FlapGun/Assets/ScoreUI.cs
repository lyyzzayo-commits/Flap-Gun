using System;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text scoreText;

    [Header("Config")]
    [SerializeField] private int scorePerHit = 1;

    private int score;

    private void Awake()
    {
        ResetScore();
        RefreshUI();
    }

    private void OnEnable()
    {
        GameSignals.ScoreUp += OnScoreUp;
    }

    private void OnDisable()
    {
        GameSignals.ScoreUp -= OnScoreUp;
    }

    private void OnScoreUp()
    {
        score += scorePerHit;
        Debug.Log("OnScoreUp");
        RefreshUI();
    }

    private void ResetScore()
    {
        score = 0;
    }

    private void RefreshUI()
    {
        if (scoreText != null)
            scoreText.text = score.ToString();
    }
}
