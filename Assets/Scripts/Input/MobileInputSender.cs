using System.Net.Sockets;
using System.Text;
using TMPro;
using UnityEngine;

public class MobileInputSender : MonoBehaviour
{
    private UdpClient udpClient;
    public string targetIP = "192.168.105.175";
    public TextMeshProUGUI debugText = null;

    public int targetPort = 9000;
    void Start()
    {
        udpClient = new UdpClient();
        //udpClient.EnableBroadcast = true;
    }

    void Update()
    {
        // Example: send a message when the screen is touched
        if (Input.touchCount > 0)
        {
            debugText.text = "Touch at: " + Input.GetTouch(0).position;
            string message = "Touch at: " + Input.GetTouch(0).position;
            SendUdpMessage(message);
        }
    }

    void SendUdpMessage(string message)
    {
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            udpClient.Send(data, data.Length, targetIP, targetPort);
            debugText.text = "UDP Sent.";
        }
        catch (SocketException ex)
        {
            debugText.text = "UDP Send failed: " + ex.Message;
            Debug.LogError("UDP Send failed: " + ex.Message);
        }
    }

    void OnApplicationQuit()
    {
        udpClient?.Close();
    }
}
