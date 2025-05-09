using UnityEngine;

public class InfladorScript : MonoBehaviour
{
    
    public InputManager inputManager;
    public InfladorMinigameManager infladorManager;
    
    public Transform position1;
    public Transform position2;

    private Vector3 currentTargetPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTargetPosition = position1.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Ensure the InputManager is assigned
        if (inputManager == null)
        {
            Debug.LogWarning("InputManager has not been assigned to InfladorScript.");
            return;
        }

        // Get the movement input
        Vector2 movementInput = inputManager.movementInput;

        // Check for "Up" input (positive Y)
        if (movementInput.y > 0 && currentTargetPosition != position1.position)
        {
            currentTargetPosition = position1.position; // Set target to position1
        }
        // Check for "Down" input (negative Y)
        else if (movementInput.y < 0 && currentTargetPosition != position2.position)
        {
            currentTargetPosition = position2.position; // Set target to position2
            infladorManager.PumpBall();
        }

        // Teleport the GameObject to the target position
        transform.position = currentTargetPosition;
    }
}
