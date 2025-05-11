using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerImitate : MonoBehaviour
{
    public GameObject[] poses;
    [SerializeField] private GameObject banderaIdle;
    public int directionPose=0;
    public bool hasPosed = false;
    public Image greatImage, failImage;
    public Sprite none, great, fail;
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
        greatImage.enabled = false;
        failImage.enabled = false;
        
        
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
            banderaIdle.SetActive(false);
            poses[random].SetActive(true);
            startPose.Play();
            directionPose = random;
            yield return new WaitForSeconds(1);
            posing = false;
            directionPose = 0;
            poses[random].SetActive(false);
            banderaIdle.SetActive(true);
            if (!turnsbool[turn])
            {
                missPose.Play();
                StartCoroutine(ChangeText(false));
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
            StartCoroutine(ChangeText(true));
            MiniGameManager.Instance.AddGamePoints(minigamePoints);
        }
        else
        {
            missPose.Play();
            StartCoroutine(ChangeText(false));
            MiniGameManager.Instance.RemoveGamePoints(minigamePoints);
        }
    }

    public void callCoroutine()
    {
        StartCoroutine(ChangeText(false));
    }

    IEnumerator ChangeText(bool value)
    {
        if (value)
        {
            greatImage.enabled=true;
            failImage.enabled = false;
            yield return new WaitForSeconds(0.6f);
            greatImage.enabled = false;
            failImage.enabled = false;
        }
        else
        {
            greatImage.enabled = false;
            failImage.enabled = true;
            yield return new WaitForSeconds(0.6f);
            greatImage.enabled = false;
            failImage.enabled = false;
        }
        
    }
}
