
using System;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;



[Serializable]
public sealed class RoundConfig
{
    [Min(0.1f)] public float baseTimeLimit = 15.0f;
    [Min(0f)] public float timeDecreasePerRound = 0.25f;
    [Min(0.1f)] public float minTimeLimt =1.5f;
    [Min(0)] public int maxRounds = 0; // 0 = unlimited
}

public sealed class RoundManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private RoundConfig config;

    [Header("Runtime (Read Only)")]
    [SerializeField] private int currentRound = 0;
    [SerializeField] private float remainingTime = 0f;
    [SerializeField] private bool isRunning = false;
    [SerializeField] private bool isGameOver = false;

    [SerializeField] private TargetManager targetManager;
    
    

    public int CurrentRound => currentRound;
    public float RemainingTime => remainingTime;
    public bool IsRunning => isRunning;
    public bool IsGameOver => isGameOver;

    
    private float _tickAccumulator = 0f;

    // Tick rate for UI updates (set 0 to tick every frame)
    [SerializeField] private float tickInterval = 0.1f;

    private bool timeUpRaised;

    private void Update()
    {
        if(timeUpRaised) return;

        remainingTime -= Time.deltaTime;
        if(remainingTime <= 0f)
        {
            remainingTime = 0f;
            timeUpRaised = true;
            GameSignals.RaiseTimeUp();
        }
    }

    

    public void StartFirstRound()
    {
        timeUpRaised = false;

        if (IsGameOver)
            return;
        if(currentRound > 0)
           return;
        currentRound = 1;

        float timeLimit = ComputeTimeLimitForRound(currentRound);
       
        remainingTime = timeLimit;

        targetManager.RespawnForRound(currentRound);


        StartTimer();
    }

    public void RequestNextRound()
    {
        timeUpRaised = false;
        
        if (isGameOver)
            return;
        if (currentRound <= 0)
        {
            StartFirstRound();
            return;
        }

        currentRound++;

        targetManager.RespawnForRound(currentRound);

        float timeLimit = ComputeTimeLimitForRound(currentRound);
        remainingTime = timeLimit;

        StartTimer();
    }

    public void StartTimer()
    {
        if (isGameOver)
            return;
        if (remainingTime <= 0f)
        {
            TriggerGameOver();
            return;
        }

        isRunning = true;
    }

    public void TriggerGameOver()
    {
        if(IsGameOver)
           return;
        
        isGameOver = true;
        isRunning = false;

        remainingTime = 0f;

        
    }  

    private float ComputeTimeLimitForRound(int roundIndex)
    {
        if (roundIndex < 1) roundIndex = 1;

        if(config == null)
           return 15.0f;

           float time = config.baseTimeLimit - config.timeDecreasePerRound * (roundIndex -1);
        if (time < config.minTimeLimt)
            time = config.minTimeLimt;   
        
        return time;
    }
}

