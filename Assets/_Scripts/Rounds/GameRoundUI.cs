using TMPro;
using UnityEngine;

public class GameRoundUI : MonoBehaviour
{
    [SerializeField] private GameObject gameRoundPanel;
    [SerializeField] private RoundObjectiveSlot[] roundObjectiveSlots;
    [SerializeField] private TMP_Text roundTitle;
    
    private GameController gameController;

    private void Awake()
    {
        gameController = GameController.Instance;
    }
    private void OnEnable()
    {
        gameController.OnRoundScreenLoaded += SetViewData;
    }
    private void OnDisable()
    {
        gameController.OnRoundScreenLoaded -= SetViewData;
    }
    private void SetViewData(GameRoundData roundData)
    {
        roundTitle.text = $"Round {gameController.CurrentRoundIndex + 1}";
        
        roundObjectiveSlots[0].SetObjectiveText(roundData.MushroomsToKill);
        roundObjectiveSlots[1].SetObjectiveText(roundData.GoblinsToKill);
        roundObjectiveSlots[2].SetObjectiveText(roundData.GoldToCollect);

        gameRoundPanel.SetActive(true);
    }
    public void StartRoundButton()
    {
        gameRoundPanel.SetActive(false);
        gameController.OnRoundStarted?.Invoke();
    }
}
