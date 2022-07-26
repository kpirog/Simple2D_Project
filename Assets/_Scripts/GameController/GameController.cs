using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] private List<GameRoundData> roundDataList;

    private int currentRoundIndex = 0;

    public int CurrentRoundIndex => currentRoundIndex;
    public GameRoundData CurrentRoundData => roundDataList[currentRoundIndex];

    public Action<GameRoundData> OnRoundScreenLoaded;
    public Action OnRoundStarted;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    private void Start()
    {
        LoadRoundScreen();
    }
    private void OnEnable()
    {
        OnRoundStarted += StartRound;
    }
    private void OnDisable()
    {
        OnRoundStarted -= StartRound;
    }
    private void LoadRoundScreen()
    {
        OnRoundScreenLoaded?.Invoke(CurrentRoundData);
    }
    private void StartRound()
    {
        Debug.Log("Round started");
    }
}
