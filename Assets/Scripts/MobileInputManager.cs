using System;
using TMPro;
using UnityEngine;

public class MobileInputManager : BaseInputManager
{
    [SerializeField]
    private MobileInputSender mobileInputSender;

    public TextMeshProUGUI debugText;
    protected override void OnEnable()
    {
        base.OnEnable();
        debugText.text = "subscribed";
    }

    protected override void InputManagerOnMovementInputEvent(Vector3 movement)
    {
        MobileInputSender.UDPMessage message = new MobileInputSender.UDPMessage(MobileInputSender.UDPMessage.EMessageType.LinearAcceleration, movement);
        mobileInputSender.SendUdpMessage(message);
        debugText.text = "" + movement;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        debugText.text = "unsubscribed";
    }
}
