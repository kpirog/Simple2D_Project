using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    [SerializeField] private Image crosshair;

    private bool isActive = false;

    private void OnEnable()
    {
        EventManager.OnGetShurikenEvent += EventManager_OnGetShurikenEvent;
        EventManager.OnThrowShurikenEvent += EventManager_OnThrowShurikenEvent;
        EventManager.OnRoundStart += EventManager_OnRoundStart;
        EventManager.OnRoundComplete += EventManager_OnRoundComplete;
    }
    private void OnDisable()
    {
        EventManager.OnGetShurikenEvent -= EventManager_OnGetShurikenEvent;
        EventManager.OnThrowShurikenEvent -= EventManager_OnThrowShurikenEvent;
        EventManager.OnRoundStart -= EventManager_OnRoundStart;
    }
    private void EventManager_OnGetShurikenEvent()
    {
        ToggleCrosshair(true);
    }
    private void EventManager_OnThrowShurikenEvent()
    {
        ToggleCrosshair(false);
    }
    private void EventManager_OnRoundStart()
    {
        ToggleCrosshair(false);
    }
    private void EventManager_OnRoundComplete(bool obj)
    {
        ToggleCrosshair(false);
    }
    private void ToggleCrosshair(bool enable)
    {
        isActive = enable;
        crosshair.enabled = enable;
    }
}
