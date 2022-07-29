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

    private Mouse virtualMouse;

    private void OnEnable()
    {
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
    }
    private void OnDisable()
    {
        InputSystem.onAfterUpdate -= UpdateMotion;
    }
    private void UpdateMotion()
    {
        if (Gamepad.current == null || virtualMouse == null)
        {
            return;
        }

        Vector2 stickDelta = Gamepad.current.rightStick.ReadValue();
        stickDelta *= cursorSpeed * Time.deltaTime;

        Vector2 currentPosition = virtualMouse.position.ReadValue();
        Vector2 newPosition = currentPosition + stickDelta;

        newPosition.x = Mathf.Clamp(newPosition.x, 0f, Screen.width);
        newPosition.y = Mathf.Clamp(newPosition.y, 0f, Screen.height);

        InputState.Change(virtualMouse.position, newPosition);
        InputState.Change(virtualMouse.delta, stickDelta);

        SetCursorPosition(newPosition);
    }
    private void SetCursorPosition(Vector2 newPosition)
    {
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, newPosition, null, out anchoredPosition);

        cursorRectTransform.anchoredPosition = anchoredPosition;
    }
}
