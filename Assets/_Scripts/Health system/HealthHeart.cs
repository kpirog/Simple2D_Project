using UnityEngine;
using UnityEngine.UI;

public class HealthHeart : MonoBehaviour
{
    [SerializeField] private Image heartImage;
    [SerializeField] private Sprite fullHeartSprite;
    [SerializeField] private Sprite emptyHeartSprite;

    public void SetState(bool isFull)
    {
        heartImage.sprite = isFull ? fullHeartSprite : emptyHeartSprite;
    }
}
