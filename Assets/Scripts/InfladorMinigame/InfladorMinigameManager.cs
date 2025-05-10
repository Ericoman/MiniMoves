using UnityEngine;
using System.Collections;

public class InfladorMinigameManager : MonoBehaviour
{
    public GameObject inflatingBall;

    public int ballPump = 0;
    public int points = 0;
    public int maxPumps = 10;
    public float scaleFactor = 0.2f;
    public float timeBetweenBalls = 1.0f;
    public bool ballInStation = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballPump = 0;
        points = 0;
        
        ballInStation = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PumpBall()
    {
        
        ballPump++;

        if (ballPump < maxPumps && ballInStation == true)
        {
            // Scale the inflatingBall by 0.2 each time PumpBall is called
            Vector3 currentScale = inflatingBall.transform.localScale;
            inflatingBall.transform.localScale = new Vector3(
                currentScale.x + scaleFactor,
                currentScale.y + scaleFactor,
                currentScale.z + scaleFactor);
        }

        if (ballPump == maxPumps)
        {
            ballInStation = false;
            StartCoroutine(WaitForBall(timeBetweenBalls));
        }
    }

    public void PopBall()
    {
        points++;
        MiniGameManager.Instance.SetGamePoints(points);
        Debug.Log("Points: " + points);
        inflatingBall.SetActive(false);
    }


    IEnumerator WaitForBall(float delay)
    {
        // Wait for the specified delay
        PopBall();
        yield return new WaitForSeconds(delay);

        // Reset the ball state for a new round
        ballPump = 0;
        ballInStation = true;
        inflatingBall.transform.localScale = Vector3.one; // Reset scale
        inflatingBall.SetActive(true);
    }
}
