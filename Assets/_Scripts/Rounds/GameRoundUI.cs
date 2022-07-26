using TMPro;
using UnityEngine;

public class GameRoundUI : MonoBehaviour
{
    [SerializeField] private GameObject gameRoundPanel;
    [SerializeField] private RoundObjectiveSlot[] roundObjectiveSlots;
    [SerializeField] private TMP_Text roundTitle;
    
    private void OnEnable()
    {
        EventManager.OnRoundScreenLoad += SetViewData;
    }
    private void OnDisable()
    {
        EventManager.OnRoundScreenLoad -= SetViewData;
    }
    private void SetViewData(GameRoundData roundData)
    {
        Debug.Log("To");
        
        roundTitle.text = $"Round {GameRoundController.Instance.CurrentRoundIndex + 1}";
        
        roundObjectiveSlots[0].SetObjectiveText(roundData.MushroomsToKill);
        roundObjectiveSlots[1].SetObjectiveText(roundData.GoblinsToKill);
        roundObjectiveSlots[2].SetObjectiveText(roundData.GoldToCollect);

        gameRoundPanel.SetActive(true);
    }
    public void StartRoundButton()
    {
        EventManager.OnRoundStarted();
        gameRoundPanel.SetActive(false);
    }
}
