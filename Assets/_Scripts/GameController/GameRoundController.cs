using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameRoundController : MonoBehaviour
{
    [SerializeField] private List<GameRoundData> roundDataList;
    [SerializeField] private SpawnerManager spawnerManager;

    #region Round Variables
    private GameRoundGoal currentRoundGoal;
    private int currentRoundIndex = 0;
    private int killedMushrooms = 0;
    private int killedGoblins = 0;
    private int collectedGold = 0;

    public int CurrentRoundIndex => currentRoundIndex;
    public int KilledMushrooms => killedMushrooms;
    public int KilledGoblins => killedGoblins;
    public int CollectedGold => collectedGold;
    public GameRoundData CurrentRoundData => roundDataList[currentRoundIndex];
    #endregion

    #region Singleton
    public static GameRoundController Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    #endregion

    private void Start()
    {
        EventManager.OnRoundScreenLoaded(CurrentRoundData);
    }
    private void OnEnable()
    {
        EventManager.OnRoundStart += EventManager_OnRoundStart;
        EventManager.OnRoundComplete += EventManager_OnRoundComplete;
        EventManager.OnMushroomKill += EventManager_OnMushroomKill;
        EventManager.OnGoblinKill += EventManager_OnGoblinKill;
        EventManager.OnCoinCollect += EventManager_OnCoinCollect;
    }
    private void OnDisable()
    {
        EventManager.OnMushroomKill -= EventManager_OnMushroomKill;
        EventManager.OnGoblinKill -= EventManager_OnGoblinKill;
        EventManager.OnCoinCollect -= EventManager_OnCoinCollect;
    }
    private void EventManager_OnRoundStart()
    {
        Debug.Log("Round started!");
        currentRoundGoal = new GameRoundGoal(CurrentRoundData.MushroomsToKill, CurrentRoundData.GoblinsToKill, CurrentRoundData.GoldToCollect);
        spawnerManager.SetRoundData(CurrentRoundData);
    }
    private void EventManager_OnRoundComplete(bool complete)
    {
        if (complete)
        {
            Debug.Log("Round complete!");
        }
        else
        {
            Debug.Log("Round not complete!");
        }
    }
    private void EventManager_OnMushroomKill()
    {
        killedMushrooms++;
        CheckIfRoundComplete();
    }
    private void EventManager_OnGoblinKill()
    {
        killedGoblins++;
        CheckIfRoundComplete();
    }
    private void EventManager_OnCoinCollect()
    {
        collectedGold++;
        CheckIfRoundComplete();
    }
    private void CheckIfRoundComplete()
    {
        bool complete = false;
        
        if (killedMushrooms >= currentRoundGoal.MushroomsToKill && killedGoblins >= currentRoundGoal.GoblinsToKill
            && collectedGold >= currentRoundGoal.GoldToCollect)
        {
            complete = true;
        }

        EventManager.OnRoundCompleted(complete);
    }
}
