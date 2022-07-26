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
        EventManager.OnCoinCollect += SetViewData;
    }
    private void OnDisable()
    {
        EventManager.OnCoinCollect -= SetViewData; 
    }
    private void SetViewData()
    {
        coinCount++;
        coinCountText.text = coinCount.ToString();
    }
}
