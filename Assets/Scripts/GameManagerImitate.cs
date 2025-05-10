using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerImitate : MonoBehaviour
{
    public GameObject[] poses;
    public int directionPose=0;
    public bool hasPosed = false;
    public Text feedback;
    public bool[] turnsbool = new bool[50];
    public int turn;
    public bool posing = false;
    public AudioSource startPose, hitPose, missPose, backgroundMusic;
    
    public int minigamePoints = 5;
    
    public static GameManagerImitate Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        directionPose = 0;
        StartCoroutine(ChangePoses());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        // Asegurarse de que solo exista una instancia
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persiste entre escenas
    }

    IEnumerator ChangePoses()
    {
        backgroundMusic.Play();
        yield return new WaitForSeconds(1);
        while (true)
        {
            turn++;
            posing = true;
            int random = Random.Range(0, poses.Length);
            poses[random].SetActive(true);
            startPose.Play();
            directionPose = random;
            yield return new WaitForSeconds(1);
            posing = false;
            directionPose = 0;
            poses[random].SetActive(false);
            if (!turnsbool[turn])
            {
                missPose.Play();
                StartCoroutine(ChangeText("Fail"));
            }
            yield return new WaitForSeconds(Random.Range(1,3));
        }
        //int random = Random.Range(0, poses.Length);

    }
    

    public void CheckPose(int direction)
    {
        if (direction < poses.Length && directionPose==direction)
        {
            hitPose.Play();
            StartCoroutine(ChangeText("Good"));
            MiniGameManager.Instance.AddGamePoints(minigamePoints);
        }
        else
        {
            missPose.Play();
            StartCoroutine(ChangeText("Fail"));
            MiniGameManager.Instance.RemoveGamePoints(minigamePoints);
        }
    }

    public void callCoroutine(string value)
    {
        StartCoroutine(ChangeText(value));
    }

    IEnumerator ChangeText(string value)
    {
        feedback.text = value;
        yield return new WaitForSeconds(0.9f);
        feedback.text = "";
    }
}
