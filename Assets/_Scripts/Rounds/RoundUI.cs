using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoundUI : MonoBehaviour
{
    [SerializeField] private GameObject startRoundPanel;
    [SerializeField] private GameObject completeRoundPanel;

    [SerializeField] private RoundObjectiveSlot[] roundObjectiveSlots;
    [SerializeField] private HealthHeart[] hearts;

    [SerializeField] private TMP_Text startRoundTitle;
    [SerializeField] private TMP_Text completeRoundTitle;
    [SerializeField] private TMP_Text completeRoundContent;
    [SerializeField] private TMP_Text coinCountText;
    [SerializeField] private TMP_Text mushroomCountText;
    [SerializeField] private TMP_Text goblinCountText;
    [SerializeField] private TMP_Text nextRoundButtonText;

    [SerializeField] private Button startRoundButton;
    [SerializeField] private Button nextRoundButton;

    private RoundController roundController;

    private void Awake()
    {
        roundController = RoundController.Instance;
    }
    private void Start()
    {
        ResetView();
    }
    private void OnEnable()
    {
        EventManager.OnStartRoundScreenLoad += SetRoundData;
        EventManager.OnCompleteRoundScreenLoad += EventManager_OnCompleteRoundScreenLoad;
        EventManager.OnCoinCollect += EventManager_OnCoinCollect;
        EventManager.OnHeartUpdate += EventManager_OnHeartUpdate;
        EventManager.OnMushroomKill += EventManager_OnMushroomKill;
        EventManager.OnGoblinKill += EventManager_OnGoblinKill;
    }
    private void OnDisable()
    {
        EventManager.OnStartRoundScreenLoad -= SetRoundData;
        EventManager.OnCompleteRoundScreenLoad -= EventManager_OnCompleteRoundScreenLoad;
        EventManager.OnCoinCollect -= EventManager_OnCoinCollect;
        EventManager.OnHeartUpdate -= EventManager_OnHeartUpdate;
        EventManager.OnMushroomKill -= EventManager_OnMushroomKill;
        EventManager.OnGoblinKill -= EventManager_OnGoblinKill;
    }
    private void EventManager_OnGoblinKill()
    {
        goblinCountText.text = roundController.KilledGoblins.ToString();
    }
    private void EventManager_OnMushroomKill()
    {
        mushroomCountText.text = roundController.KilledMushrooms.ToString();
    }
    private void EventManager_OnCoinCollect()
    {
        coinCountText.text = roundController.CollectedGold.ToString();
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
    private void SetRoundData(RoundData roundData)
    {
        startRoundTitle.text = $"Round {roundController.CurrentRoundIndex + 1}";

        roundObjectiveSlots[0].SetObjectiveText(roundData.MushroomsToKill);
        roundObjectiveSlots[1].SetObjectiveText(roundData.GoblinsToKill);
        roundObjectiveSlots[2].SetObjectiveText(roundData.GoldToCollect);

        startRoundPanel.SetActive(true);
        startRoundButton.Select();
    }
    private void EventManager_OnCompleteRoundScreenLoad(bool roundComplete)
    {
        string roundTitle = string.Empty;
        string roundContent = string.Empty;
        string buttonText = string.Empty;
        
        if (roundComplete)
        {
            roundTitle = $"Round {roundController.CurrentRoundIndex + 1} Completed!";
            roundContent = "Congratulations!";
            buttonText = "Start next round!";
        }
        else
        {
            roundTitle = "You are dead!";
            roundContent = "Don't worry! There will be better next time!";
            buttonText = "Play again!";
        }

        completeRoundTitle.SetText(roundTitle);
        completeRoundContent.SetText(roundContent);

        nextRoundButton.Select();
        nextRoundButtonText.SetText(buttonText);

        completeRoundPanel.SetActive(true);
    }
    private void ResetView()
    {
        goblinCountText.text = "0";
        mushroomCountText.text = "0";
        coinCountText.text = "0";
    }
    public void StartRoundButton()
    {
        EventManager.OnRoundStarted();
        startRoundPanel.SetActive(false);
    }
    public void NextRoundButton()
    {
        ResetView();
        completeRoundPanel.SetActive(false);
        EventManager.OnStartRoundScreenLoaded(roundController.CurrentRoundData);
    }
}
