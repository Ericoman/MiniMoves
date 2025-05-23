using UnityEngine;
using System.Collections;
using TMPro;

public class InfladorMinigameManager : MonoBehaviour
{
    public GameObject inflatingBall;

    public int ballPump = 0;
    private int points = 0;
    public int maxPumps = 10;
    public float scaleFactor = 0.2f;
    public float timeBetweenBalls = 1.0f;
    public bool ballInStation = false;
    public int minigamePoints = 5;
    public AudioSource explode, squeak, background;
    public TMP_Text pointsText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballPump = 0;
        points = 0;
        background.Play();
        ballInStation = true;
    }

    // Update is called once per frame
    void Update()
    {
        pointsText.text = points.ToString();
    }

    public void PumpBall()
    {
        
        ballPump++;
        squeak.Play();
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
        MiniGameManager.Instance.AddGamePoints(minigamePoints);
        Debug.Log("Points: " + points);
        explode.Play();
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
