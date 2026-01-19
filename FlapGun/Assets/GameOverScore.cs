using System;
using TMPro;
using UnityEngine;

public class GameOverScore : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private ScoreManager scoremanager;
    private int finalsc;

    private void Awake()
    {
        finalsc=scoremanager.score;
        RefreshUI();
    }
    private void RefreshUI()
    {
        if (scoreText != null)
            scoreText.text = finalsc.ToString();
    }
}
