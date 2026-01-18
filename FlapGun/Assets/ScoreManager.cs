using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public void AddScore(int sc)
    {
        score += sc;
    }
}
