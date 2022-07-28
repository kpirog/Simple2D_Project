using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    [SerializeField] private Image crosshair;

    private bool isActive = false;

    private void Awake()
    {
        crosshair.enabled = true;
    }

    private void OnEnable()
    {
        EventManager.OnGetShurikenEvent += EventManager_OnGetShurikenEvent;
        EventManager.OnThrowShurikenEvent += EventManager_OnThrowShurikenEvent;
    }
    private void OnDisable()
    {
        EventManager.OnGetShurikenEvent -= EventManager_OnGetShurikenEvent;
        EventManager.OnThrowShurikenEvent -= EventManager_OnThrowShurikenEvent;
    }
    private void EventManager_OnGetShurikenEvent()
    {
        isActive = true;
        crosshair.enabled = true;
    }
    private void EventManager_OnThrowShurikenEvent()
    {
        isActive = false;
        crosshair.enabled = false;
    }
    private void Update()
    {
        if (!isActive) { return; }

        crosshair.transform.position = Input.mousePosition;
    }
}
