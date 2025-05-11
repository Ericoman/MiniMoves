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
        ipText.text = "Target IP: " + inputSender.targetIP;
        portInput.text = inputSender.targetPort.ToString();
        portText.text = "Target Port: " + inputSender.targetPort;
    }

    public void ChangeIp()
    {
        if (ipInput)
        {
            ipText.text = "Target IP: " + ipInput.text;
            inputSender.ChangeTargetIP(ipInput.text);
        }
    }

    public void ChangePort()
    {
        if (portInput.text != null && portInput.text != "")
        {
            portText.text = "Target Port: " + portInput.text;
            inputSender.ChangeTargetPort(portInput.text);
        }
    }
    
}
