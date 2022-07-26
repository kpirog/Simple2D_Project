using System;

public static class EventManager 
{
    #region Round Events
    public static event Action<GameRoundData> OnRoundScreenLoad;
    public static event Action OnRoundStart;
    public static event Action<bool> OnRoundComplete;
    public static event Action OnGoblinKill;
    public static event Action OnMushroomKill;
    public static event Action OnCoinCollect;
    
    public static void OnRoundScreenLoaded(GameRoundData roundData)
    {
        OnRoundScreenLoad?.Invoke(roundData);
    }
    public static void OnRoundStarted()
    {
        OnRoundStart?.Invoke();
    }
    public static void OnRoundCompleted(bool complete)
    {
        OnRoundComplete?.Invoke(complete);
    }
    public static void OnGoblinKilled()
    {
        OnGoblinKill?.Invoke();
    }
    public static void OnMushroomKilled()
    {
        OnMushroomKill?.Invoke();
    }
    public static void OnCoinCollected()
    {
        OnCoinCollect?.Invoke();
    }
    #endregion
}
