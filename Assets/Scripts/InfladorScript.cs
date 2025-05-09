using UnityEngine;

public class InfladorScript : BaseInputManager
{
    public Transform position1;
    public Transform position2;
    [SerializeField] 
    private InfladorMinigameManager infladorManager;
    [SerializeField]
    private float threshold = 0.1f;
    private Vector3 currentTargetPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        currentTargetPosition = position1.position;
    }

    protected override void InputManagerOnMovementInputEvent(Vector3 movement)
    {
        Move(movement);
    }

    void Move(Vector3 movementInput)
    {

        // Check for "Up" input (positive Y)
        if (movementInput.y > threshold && currentTargetPosition != position1.position)
        {
            currentTargetPosition = position1.position; // Set target to position1
        }
        // Check for "Down" input (negative Y)
        else if (movementInput.y < -threshold && currentTargetPosition != position2.position)
        {
            currentTargetPosition = position2.position; // Set target to position2
            infladorManager.PumpBall();
        }

        // Teleport the GameObject to the target position
        transform.position = currentTargetPosition;
    }
}
