using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput
{
    public static PlayerInput Instance
    {
        get
        {
            if (_instance == null)
                _instance = new PlayerInput();
            return _instance;
        }
    }
    private static PlayerInput _instance;

    public Vector2 TouchPosition { get; private set; }
    public bool Tap { get; private set; }

    private PlayerInputActions _playerInputActions;

    public PlayerInput()
    {

        SetupControl();
    }

    public void ResetInputs()
    {
        Tap = false;
    }

    private void SetupControl()
    {
        _playerInputActions = new PlayerInputActions();

        _playerInputActions.Player.Swipe.performed += context => OnPosition(context);
        _playerInputActions.Player.Tap.performed += context => OnTap(context);

        _playerInputActions.Player.Enable();
    }

    private void OnTap(InputAction.CallbackContext context)
    {
        Tap = true;
    }

    private void OnPosition(InputAction.CallbackContext context)
    {
        TouchPosition = context.ReadValue<Vector2>();
    }
}
