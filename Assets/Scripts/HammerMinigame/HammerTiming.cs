using System;
using UnityEngine;

public class HammerTiming : MonoBehaviour
{
    public float barWith = 3f;
    public Transform movingLine;
    private float linePosition;
    
    [Range(1f, 10f)] public float lineSpeed =2f;
    
    private bool isHammerDown = false;

    private float perfectZoneStart = 0.10f;
    private float perfectZoneEnd = 0.50f;

    public int perfectPoints = 10;
    public int goodPoints = 5;

    private void OnEnable()
    {
        HammerInput.HammerInputEvent += ProcessInput;
    }

    private void OnDisable()
    {
        HammerInput.HammerInputEvent -= ProcessInput;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLineMovement();
    }
    
    void UpdateLineMovement()
    {
        linePosition = Mathf.PingPong(Time.time * lineSpeed, 1f);
        Vector3 newPos = movingLine.localPosition;
        newPos.x = Mathf.Lerp(-barWith / 2f, barWith / 2f, linePosition);
        movingLine.localPosition = newPos;
    }
    
    void ProcessInput()
    {
        // Determine accuracy based on the line position
        if (linePosition >= perfectZoneStart && linePosition <= perfectZoneEnd)
        {
            //MiniGameManager.Instance.AddGamePoints(goodPoints);
            Debug.Log("Good! 70 Points");
        }
        else if (linePosition < perfectZoneStart) // Can divide zones further
        {
            //MiniGameManager.Instance.AddGamePoints(perfectPoints);
            Debug.Log("Perfect! 100 Points");
        }
        else
        {
            //MiniGameManager.Instance.RemoveGamePoints(goodPoints);
            Debug.Log("Miss! 0 Points");
        }

        // Reset flag
        isHammerDown = false;
    }

    
}
