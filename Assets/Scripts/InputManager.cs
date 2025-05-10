using System;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class InputManager : MonoBehaviour
{
    public delegate void MovementInputEventHandler(Vector3 movement); 
    public event MovementInputEventHandler MovementInputEvent;
    public Vector3 movementInput;
    public TextMeshProUGUI debugText;
    private static InputManager _instance;
    public static InputManager Instance => _instance;

    public bool bDebug = false;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void HandleInput(Vector3 input)
    {
        movementInput = input;
        try
        {
            if(bDebug && debugText) debugText.text = "invocando";
            MovementInputEvent?.Invoke(movementInput);
        }
        catch (Exception e)
        {
            if(bDebug && debugText) debugText.text = "excepcion: " + e.Message;
        }
        if(bDebug && debugText) debugText.text = $"Received input from InputManager: {movementInput}";
        
        // Example: Log it or use it in device-specific logic
        if(bDebug) Debug.Log($"Received input from InputManager: {movementInput}");
    }
}
