using UnityEngine;
using TMPro;

public class CoinView : MonoBehaviour
{
    [SerializeField] private TMP_Text coinCountText;
    
    private CoinController coinController;
    private int coinCount;

    private void Awake()
    {
        coinController = FindObjectOfType<CoinController>();
    }
    private void OnEnable()
    {
        coinController.onCoinCollected.AddListener(SetViewData);
    }
    private void OnDisable()
    {
        coinController.onCoinCollected.RemoveListener(SetViewData);
    }
    private void SetViewData()
    {
        coinCount++;
        coinCountText.text = coinCount.ToString();
    }
}
