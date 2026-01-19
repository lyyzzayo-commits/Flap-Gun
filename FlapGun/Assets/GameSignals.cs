using System;
using UnityEngine;

public static class GameSignals
{
    public static event Action ShootRequested;
    public static void RaiseShootRequested() => ShootRequested?.Invoke();
    public static event Action BulletFired;
    public static void RaiseBulletFired() => BulletFired?.Invoke();
    public static event Action<FruitTarget> FruitHit;
    public static void RaiseFruitHit(FruitTarget fruit) => FruitHit?.Invoke(fruit);
    public static event Action<FruitTarget> FruitDestroyed;
    public static void RaiseFruitDestroyed(FruitTarget fruit) => FruitDestroyed?.Invoke(fruit);
    public static event Action ScoreUp;
    public static void RaiseScoreUP() => ScoreUp?.Invoke();
    public static event Action GameOver;
    public static void RaiseGameOver() => GameOver?.Invoke();

    public static event Action TimeUp;
    public static void RaiseTimeUp() => TimeUp?.Invoke();

    public static event Action<float, float> TimeUpdated;
    public static void RaiseTimeUpdated(float remaining, float max)
        => TimeUpdated?.Invoke(remaining, max);
}
