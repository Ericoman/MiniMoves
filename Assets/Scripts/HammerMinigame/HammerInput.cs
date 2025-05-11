using System.Collections;
using UnityEngine;

public class HammerInput : BaseInputManager
{
    
    public delegate void HammerInputEventHandler();
    public static event HammerInputEventHandler HammerInputEvent;

    public float threshold = 1f;
    public float cooldownTime = 0.5f;
    [SerializeField] bool bCanPress = true;
    protected override void InputManagerOnMovementInputEvent(Vector3 movement)
    {
        if(!bCanPress) return;
        if (movement.z < -threshold)
        {
            HammerInputEvent?.Invoke();
            StartCoroutine(InputCooldown());
        }
    }
    
    IEnumerator InputCooldown()
    {
        bCanPress = false;
        yield return new WaitForSeconds(cooldownTime);
        bCanPress = true;
    }
}
