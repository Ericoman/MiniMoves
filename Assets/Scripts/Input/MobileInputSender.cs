using System;
using System.Net.Sockets;
using System.Text;
using TMPro;
using UnityEngine;

public class MobileInputSender : MonoBehaviour
{
    [Serializable]
    public class UDPMessage
    {
        public enum EMessageType
        {
            None = 0,
            LinearAcceleration,
        }
        public EMessageType messageType;
        public Vector3 inputVector;

        public UDPMessage(EMessageType _messageType, Vector3 _inputVector)
        {
            messageType = _messageType;
            inputVector = _inputVector;
        }
    }
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
        // // Example: send a message when the screen is touched
        // if (Input.touchCount > 0)
        // {
        //     debugText.text = "Touch at: " + Input.GetTouch(0).position;
        //     string message = "Touch at: " + Input.GetTouch(0).position;
        //     SendUdpMessage(message);
        // }
    }

    public void SendUdpMessage(UDPMessage message)
    {
        try
        {
            string messageString = JsonUtility.ToJson(message);
            byte[] data = Encoding.UTF8.GetBytes(messageString);
            udpClient.Send(data, data.Length, targetIP, targetPort);
            debugText.text = "UDP Sent: " + message;
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

    public void ChangeTargetIP(string _ip)
    {
        targetIP = _ip;
    }

    public void ChangeTargetPort(string _port)
    {
        try
        {
            targetPort = Int32.Parse(_port);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            throw;
        }
    }
}
