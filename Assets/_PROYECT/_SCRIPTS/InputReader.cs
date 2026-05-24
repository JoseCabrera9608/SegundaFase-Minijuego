using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    public static InputReader Instance;
    public Vector2 move;
    public Vector2 look;

    private InputSystem_Actions inputSystem_Actions;

    public static event Action onInteract;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        inputSystem_Actions = new InputSystem_Actions();
        inputSystem_Actions.Player.SetCallbacks(this);
        inputSystem_Actions.Enable();
    }

    private void OnDisable()
    {
        inputSystem_Actions.Disable();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        onInteract?.Invoke();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

}
