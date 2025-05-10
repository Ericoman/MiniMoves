using TMPro;
using UnityEngine;

public class MobileConfigurator : MonoBehaviour
{
    [SerializeField] 
    private MobileInputSender inputSender;

    [SerializeField] private TMP_InputField ipInput;
    [SerializeField] private TMP_InputField portInput;
    [SerializeField] private TextMeshProUGUI ipText;
    [SerializeField] private TextMeshProUGUI portText;

    private void Start()
    {
        ipInput.text = inputSender.targetIP;
        ipText.text = inputSender.targetIP;
        portInput.text = inputSender.targetPort.ToString();
        portText.text = inputSender.targetPort.ToString();
    }

    public void ChangeIp()
    {
        if (ipInput)
        {
            ipText.text = ipInput.text;
            inputSender.ChangeTargetIP(ipInput.text);
        }
    }

    public void ChangePort()
    {
        if (portInput.text != null && portInput.text != "")
        {
            portText.text = portInput.text;
            inputSender.ChangeTargetPort(portInput.text);
        }
    }
    
}
