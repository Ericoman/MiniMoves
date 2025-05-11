using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeviceInputManager : MonoBehaviour
{
    private Vector3 deviceInput; // Stores the raw vector2 input
    public TextMeshProUGUI debugText;
    private InputAction accelerometerAction;

    void Start()
    {
        if (InputManager.Instance == null)
        {
            Debug.LogError("DeviceInputManager is not found in the scene!");
        }
    }
    void OnEnable()
    {
        if (Accelerometer.current != null)
        {
            if(debugText) debugText.text = "Accelerometer found!";
            if (!Accelerometer.current.enabled)
                InputSystem.EnableDevice(Accelerometer.current);
        }
        else
        {
            if(debugText) debugText.text ="Accelerometer not found.";
        }
        if (LinearAccelerationSensor.current != null)
        {
            if(debugText) debugText.text = "LinearAccelerationSensor found!";
            if (!LinearAccelerationSensor.current.enabled)
                InputSystem.EnableDevice(LinearAccelerationSensor.current);
        }
        else
        {
            if(debugText) debugText.text ="LinearAccelerationSensor not found.";
        }
        // Initialize the InputAction
        // accelerometerAction = new InputAction("Directions", binding: "<Accelerometer>/acceleration");
        accelerometerAction = new InputAction("Directions", binding: "<LinearAccelerationSensor>/acceleration");
        
        // Subscribe to the 'performed' event (fired when accelerometer data is available)
        accelerometerAction.performed += OnMovementInput;
        
        // Enable the InputAction
        accelerometerAction.Enable();
    }

    void OnDisable()
    {
        // Unsubscribe from the event when the script is disabled
        accelerometerAction.performed -= OnMovementInput;

        // Disable the InputAction to free up resources
        accelerometerAction.Disable();
    }

    public void OnMovementInput(InputAction.CallbackContext context)
    {
        deviceInput = context.ReadValue<Vector3>();
        if(debugText) debugText.text = deviceInput.ToString();
        if (InputManager.Instance != null)
        {
            InputManager.Instance.HandleInput(deviceInput);
        }
        else
        {
            if(debugText) debugText.text = "No";
        }
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
