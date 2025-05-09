using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PCInputReceiver : MonoBehaviour
{
    UdpClient udpClient;
    public int listenPort = 9000;
    private bool isListening = true;
    
    void Start()
    {
        udpClient = new UdpClient(listenPort); // Binds to 0.0.0.0
        isListening = true;

        Debug.Log($"Started async UDP receiver on port {listenPort}");
        StartLoop();
    }

    async void StartLoop()
    {
        try
        {
            await ReceiveLoop();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Socket exception: {ex.Message}");
        }
    }
    async Task ReceiveLoop()
    {
        while (isListening)
        {
            try
            {
                UdpReceiveResult result = await udpClient.ReceiveAsync();
                string message = Encoding.UTF8.GetString(result.Buffer);
                Debug.Log($"[UDP] Received from {result.RemoteEndPoint}: {message}");
            }
            catch (SocketException ex)
            {
                Debug.LogError($"Socket exception: {ex.Message}");
                break;
            }
        }
        udpClient?.Close();
    }

    void OnApplicationQuit()
    {
        
        isListening = false;
    }
}
