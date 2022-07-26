using TMPro;
using UnityEngine;

public class GameRoundUI : MonoBehaviour
{
    [SerializeField] private GameObject gameRoundPanel;

    [SerializeField] private RoundObjectiveSlot[] roundObjectiveSlots;
    [SerializeField] private HealthHeart[] hearts;

    [SerializeField] private TMP_Text roundTitle;
    [SerializeField] private TMP_Text coinCountText;
    [SerializeField] private TMP_Text mushroomCountText;
    [SerializeField] private TMP_Text goblinCountText;

    private GameRoundController gameRoundController;

    private void Awake()
    {
        gameRoundController = GameRoundController.Instance;   
    }
    private void OnEnable()
    {
        EventManager.OnRoundScreenLoad += SetRoundData;
        EventManager.OnCoinCollect += EventManager_OnCoinCollect;
        EventManager.OnHeartUpdate += EventManager_OnHeartUpdate;
        EventManager.OnMushroomKill += EventManager_OnMushroomKill;
        EventManager.OnGoblinKill += EventManager_OnGoblinKill;
    }
    private void OnDisable()
    {
        EventManager.OnRoundScreenLoad -= SetRoundData;
        EventManager.OnCoinCollect -= EventManager_OnCoinCollect;
        EventManager.OnHeartUpdate -= EventManager_OnHeartUpdate;
        EventManager.OnMushroomKill -= EventManager_OnMushroomKill;
        EventManager.OnGoblinKill -= EventManager_OnGoblinKill;
    }
    private void EventManager_OnGoblinKill()
    {
        goblinCountText.text = gameRoundController.KilledGoblins.ToString();
    }
    private void EventManager_OnMushroomKill()
    {
        mushroomCountText.text = gameRoundController.KilledMushrooms.ToString();
    }
    private void EventManager_OnCoinCollect()
    {
        coinCountText.text = gameRoundController.CollectedGold.ToString();
    }
    private void EventManager_OnHeartUpdate(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if ((currentHealth - 1) >= i)
            {
                hearts[i].SetState(true);
            }
            else
            {
                hearts[i].SetState(false);
            }
        }
    }
    private void SetRoundData(GameRoundData roundData)
    {
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
