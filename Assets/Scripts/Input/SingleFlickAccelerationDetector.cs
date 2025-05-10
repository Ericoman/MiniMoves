using UnityEngine;

public class SingleFlickAccelerationDetector : BaseInputManager
{
    public float accelThreshold = 1.2f;        // How hard you need to flick
    public float settleAccelThreshold = 0.3f;  // How still phone must be to reset
    public float cooldownTime = 0.3f;          // Time to wait before accepting next flick

    private bool hasDetected = false;
    private float lastDetectionTime = -1f;

    private Vector3 accel;
    
    public delegate void FlickEventHandler(Vector3 movement); 
    public event FlickEventHandler FlickEvent;
    
    protected override void InputManagerOnMovementInputEvent(Vector3 movement)
    {
        accel = movement;
    }

    public float GetCooldownTime()
    {
        return cooldownTime;
    }
    void Update()
    {
        float now = Time.time;

        // Already detected something — wait to settle
        if (hasDetected)
        {
            if (accel.magnitude < settleAccelThreshold && now - lastDetectionTime > cooldownTime)
            {
                hasDetected = false;
                Debug.Log("Settled. Ready for next flick.");
            }
            return;
        }

        // Focus only on strong horizontal flicks
        if (Mathf.Abs(accel.x) > accelThreshold && Mathf.Abs(accel.x) > Mathf.Abs(accel.y))
        {
            if (accel.x < 0)
            {
                FlickEvent?.Invoke(new Vector3(-1, 0, 0));
            }
            else
            {
                FlickEvent?.Invoke(new Vector3(1, 0, 0));
            }

            hasDetected = true;
            lastDetectionTime = now;
        }
        if (Mathf.Abs(accel.y) > accelThreshold && Mathf.Abs(accel.y) > Mathf.Abs(accel.x))
        {
            if (accel.y < 0)
            {
                FlickEvent?.Invoke(new Vector3(0, -1, 0));
            }
            else
            {
                FlickEvent?.Invoke(new Vector3(0, 1, 0));
            }

            hasDetected = true;
            lastDetectionTime = now;
        }
    }
}
