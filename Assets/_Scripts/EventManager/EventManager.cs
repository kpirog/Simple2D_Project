using System;

public static class EventManager 
{
    #region Round Events
    public static event Action<RoundData> OnStartRoundScreenLoad;
    public static event Action<bool> OnCompleteRoundScreenLoad;
    public static event Action OnRoundStart;
    public static event Action<bool> OnRoundComplete;
    public static event Action OnGoblinKill;
    public static event Action OnMushroomKill;
    public static event Action OnCoinCollect;
    public static event Action<int> OnHeartUpdate;
    
    public static void OnStartRoundScreenLoaded(RoundData roundData)
    {
        OnStartRoundScreenLoad?.Invoke(roundData);
    }
    public static void OnCompleteRoundScreenLoaded(bool roundComplete)
    {
        OnCompleteRoundScreenLoad?.Invoke(roundComplete);
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
    public static void OnHeartUpdated(int currentHealth)
    {
        OnHeartUpdate?.Invoke(currentHealth);
    }
    #endregion

    #region Gameplay Events
    public static event Action OnPlayerHitEvent;
    public static event Action OnGetShurikenEvent;
    public static event Action OnThrowShurikenEvent;
    public static void OnPlayerHit()
    {
        OnPlayerHitEvent?.Invoke();
    }
    public static void OnGetShuriken()
    {
        OnGetShurikenEvent?.Invoke();
    }
    public static void OnThrowShuriken()
    {
        OnThrowShurikenEvent?.Invoke();
    }
    #endregion
}
