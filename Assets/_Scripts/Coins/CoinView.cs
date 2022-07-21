using UnityEngine;
using TMPro;

public class CoinView : MonoBehaviour
{
    [SerializeField] private TMP_Text coinCountText;
    
    private CoinController coinController;

    private void Awake()
    {
        coinController = FindObjectOfType<CoinController>();
    }
    private void OnEnable()
    {
        coinController.onCoinCollected += SetViewData;
    }
    private void OnDisable()
    {
        coinController.onCoinCollected -= SetViewData;
    }
    private void SetViewData(int coinCount)
    {
        coinCountText.text = coinCount.ToString();
    }
}
