using UnityEngine;
using UnityEngine.Events;

public class HealthView : MonoBehaviour
{
    [SerializeField] private HealthHeart[] hearts;

    [HideInInspector] public UnityAction<int> onHealthUpdated;

    private void OnEnable()
    {
        onHealthUpdated += UpdateHealthUI;
    }
    private void OnDisable()
    {
        onHealthUpdated -= UpdateHealthUI;
    }
    private void UpdateHealthUI(int currentHealth)
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
}
