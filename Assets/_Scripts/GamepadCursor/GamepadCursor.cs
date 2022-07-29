using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

public class GamepadCursor : MonoBehaviour
{
    [SerializeField] private RectTransform cursorRectTransform;
    [SerializeField] private RectTransform canvasRectTransform;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float cursorSpeed = 1000f;
    [SerializeField] private float padding = 35f;

    private Mouse virtualMouse;
    private Mouse currentMouse;
    private Camera mainCamera;

    private string previousControlScheme = "";

    private const string gamepadControlScheme = "Gamepad";
    private const string mouseControlScheme = "Keyboard&Mouse";

    public Vector2 CurrentCursorPosition
    {
        get
        {
            if (playerInput.currentControlScheme == gamepadControlScheme)
            {
                return mainCamera.ScreenToWorldPoint(virtualMouse.position.ReadValue());
            }
            else
            {
                return mainCamera.ScreenToWorldPoint(currentMouse.position.ReadValue());
            }
        }
    }

    private void OnEnable()
    {
        currentMouse = Mouse.current;
        mainCamera = Camera.main;

        if (virtualMouse == null)
        {
            virtualMouse = InputSystem.AddDevice<Mouse>("VirtualMouse");
        }
        else if (!virtualMouse.added)
        {
            InputSystem.AddDevice(virtualMouse);
        }

        InputUser.PerformPairingWithDevice(virtualMouse, playerInput.user);

        Vector2 cursorPosition = cursorRectTransform.anchoredPosition;
        InputState.Change(virtualMouse.position, cursorPosition);

        InputSystem.onAfterUpdate += UpdateMotion;
        playerInput.onControlsChanged += OnControlsChanged;
    }
    private void OnDisable()
    {
        if (virtualMouse != null && virtualMouse.added) { InputSystem.RemoveDevice(virtualMouse); }
        InputSystem.onAfterUpdate -= UpdateMotion;
        playerInput.onControlsChanged -= OnControlsChanged;
    }
    private void OnControlsChanged(PlayerInput input)
    {
        if (input.currentControlScheme == gamepadControlScheme && previousControlScheme != gamepadControlScheme)
        {
            InputState.Change(virtualMouse.position, currentMouse.position.ReadValue());
            previousControlScheme = gamepadControlScheme;
            SetCursorPosition(currentMouse.position.ReadValue());
        }
        else if (input.currentControlScheme == mouseControlScheme && previousControlScheme != mouseControlScheme)
        {
            currentMouse.WarpCursorPosition(virtualMouse.position.ReadValue());
            previousControlScheme = mouseControlScheme;
            SetCursorPosition(currentMouse.position.ReadValue());
        }
    }
    private void UpdateMotion()
    {
        if (Gamepad.current == null || virtualMouse == null)
        {
            return;
        }

        Vector2 motionPosition;

        if (playerInput.currentControlScheme == gamepadControlScheme)
        {
            Vector2 stickDelta = Gamepad.current.rightStick.ReadValue();
            stickDelta *= cursorSpeed * Time.deltaTime;

            Vector2 currentPosition = virtualMouse.position.ReadValue();
            motionPosition = currentPosition + stickDelta;

            ClampVector(ref motionPosition);

            InputState.Change(virtualMouse.position, motionPosition);
            InputState.Change(virtualMouse.delta, stickDelta);
        }
        else
        {
            motionPosition = currentMouse.position.ReadValue();

            ClampVector(ref motionPosition);
        }

        SetCursorPosition(motionPosition);
    }
    private void SetCursorPosition(Vector2 newPosition)
    {
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, newPosition, null, out anchoredPosition);

        cursorRectTransform.anchoredPosition = anchoredPosition;
    }
    private void ClampVector(ref Vector2 vector)
    {
        vector.x = Mathf.Clamp(vector.x, padding, Screen.width - padding);
        vector.y = Mathf.Clamp(vector.y, padding, Screen.height - padding);
    }
}
