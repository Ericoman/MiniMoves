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

    public float damping = 0.9f;
    private Vector3 velocity;
    private Vector3 acceleration;

    private static PCInputReceiver _instance;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
                string messageString = Encoding.UTF8.GetString(result.Buffer);
                //Debug.Log($"[UDP] Received from {result.RemoteEndPoint}: {messageString}");
                MobileInputSender.UDPMessage message = JsonUtility.FromJson<MobileInputSender.UDPMessage>(messageString);
                //Debug.Log(message.inputVector);
                
                InputManager.Instance.HandleInput(message.inputVector);
            }
            catch (SocketException ex)
            {
                Debug.LogError($"Socket exception: {ex.Message}");
                break;
            }
        }
        udpClient?.Close();
    }

    private void OnDestroy()
    {
        isListening = false;
    }

    void OnApplicationQuit()
    {
        
        isListening = false;
    }
}
