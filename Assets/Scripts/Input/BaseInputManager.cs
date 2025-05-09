using System;
using UnityEngine;

public abstract class BaseInputManager : MonoBehaviour
{
 
    bool subscribed = false;
    protected virtual void OnEnable()
    {
        if (InputManager.Instance)
        {
            InputManager.Instance.MovementInputEvent += InputManagerOnMovementInputEvent; 
            subscribed = true;
        }
    }

    protected virtual void Start()
    {
        if (!subscribed)
        {
            InputManager.Instance.MovementInputEvent += InputManagerOnMovementInputEvent;
            subscribed = true;
        }
    }

    protected virtual void InputManagerOnMovementInputEvent(Vector3 movement)
    {
        
    }

    protected virtual void OnDisable()
    {
        InputManager.Instance.MovementInputEvent -= InputManagerOnMovementInputEvent; 
    }
}
