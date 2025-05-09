using UnityEngine;

public class InfladorMinigameManager : MonoBehaviour
{
    public GameObject inflatingBall;

    public int ballPump = 0;
    public int points = 0;

    public bool ballInStation = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballPump = 0;
        points = 0;
        
        ballInStation = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PumpBall()
    {
        
        ballPump++;
    }

    public void PopBall()
    {
        points++;
    }
}
