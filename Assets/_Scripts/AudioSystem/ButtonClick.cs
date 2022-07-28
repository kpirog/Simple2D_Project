using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClick : MonoBehaviour
{
    private Button myButton;
    
    private void Awake()
    {
        myButton = GetComponent<Button>();

        myButton.onClick.AddListener(PlayClick);
    }

    private void PlayClick()
    {
        AudioSystem.PlayButtonSFX_Global();
    }
}
