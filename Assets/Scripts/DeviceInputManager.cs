using UnityEngine;
using UnityEngine.InputSystem;

public class DeviceInputManager : MonoBehaviour
{
    public InputManager inputManager;
    
    private Vector2 deviceInput; // Stores the raw vector2 input

    void Start()
    {
        if (inputManager == null)
        {
            Debug.LogError("DeviceInputManager is not found in the scene!");
        }
    }

    public void OnMovementInput(InputAction.CallbackContext context)
    {
        deviceInput = context.ReadValue<Vector2>();

        if (inputManager != null)
        {
            inputManager.HandleInput(deviceInput);
        }
    }
}
