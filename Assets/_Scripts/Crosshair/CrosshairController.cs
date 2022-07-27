using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    [SerializeField] private Image crosshair;

    private PlayerControls playerControls;
    private bool isActive = false;

    private void Awake()
    {
        playerControls = new PlayerControls();
        crosshair.enabled = true;
    }

    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.Gameplay.MovePointer.performed += ctx => MovePointer(ctx.ReadValue<Vector2>());

        EventManager.OnGetShurikenEvent += EventManager_OnGetShurikenEvent;
        EventManager.OnThrowShurikenEvent += EventManager_OnThrowShurikenEvent;
    }
    private void OnDisable()
    {
        playerControls.Disable();
        playerControls.Gameplay.MovePointer.performed -= ctx => MovePointer(ctx.ReadValue<Vector2>());

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
        //if (!isActive) { return; }

        //crosshair.transform.position = Input.mousePosition;
    }
    private void MovePointer(Vector2 position)
    {
        Debug.Log("Rusza celownikiem");
        transform.position = position;
    }
}
