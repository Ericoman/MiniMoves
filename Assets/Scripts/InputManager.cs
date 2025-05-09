using UnityEngine;
using UnityEngine.PlayerLoop;


public class InputManager : MonoBehaviour
{

    public Vector2 movementInput;
    
    public void HandleInput(Vector2 input)
    {
        movementInput = input;
        
        // Example: Log it or use it in device-specific logic
        Debug.Log($"Received input from InputManager: {movementInput}");
    }
}
