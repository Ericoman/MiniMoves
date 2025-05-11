using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManagerBoxingTopDown : MonoBehaviour
{

    public GameObject[] stimulus;
    public GameObject[] stimulusOjos;
    public GameObject[] stimulusVirutas;
    public GameObject[] stimulusGlaseado;
    public Material[] materialsGlaseado;
    public Material[] materialsDonut;
    public GameObject[] waypoint;
    public int activatedStimulus = 0;
    public TextMeshProUGUI resultFeedback;
    public bool checking = false;
    public int random = 0;
    public int minigamePoints = 5;
    public float cooldownTime = 0.5f;
    public AudioSource hit, miss, background;
    public static GameManagerBoxingTopDown Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //random = Random.Range(0, stimulus.Length);
        StartCoroutine(StartGame());
        
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

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartGame()
    {
        background.Play();
        yield return new WaitForSeconds(2);
        while (true)
        {
            
            
            random = Random.Range(0, stimulus.Length);
            CleanMaterials();
            stimulus[random].gameObject.GetComponent<Renderer>().material = materialsDonut[1];
            stimulus[random].GetComponent<Stimulus>().activated = true;
            stimulus[random].GetComponent<Stimulus>().Lighten();
            activatedStimulus = random;

            stimulusGlaseado[random].gameObject.GetComponent<Renderer>().material = materialsGlaseado[1];
            stimulusGlaseado[random].GetComponent<Stimulus>().activated = true;
            stimulusGlaseado[random].GetComponent<Stimulus>().Lighten();
            activatedStimulus = random;
            
            stimulusOjos[random].gameObject.GetComponent<Renderer>().material = materialsDonut[1];
            stimulusOjos[random].GetComponent<Stimulus>().activated = true;
            stimulusOjos[random].GetComponent<Stimulus>().Lighten();
            activatedStimulus = random;
            
            stimulusVirutas[random].gameObject.GetComponent<Renderer>().material = materialsGlaseado[1];
            stimulusVirutas[random].GetComponent<Stimulus>().activated = true;
            stimulusVirutas[random].GetComponent<Stimulus>().Lighten();
            activatedStimulus = random;
            yield return new WaitForSeconds(2);
        }
        
    }

    void CleanMaterials()
    {
        for (int i = 0; i < stimulus.Length; i++)
        {
            if (stimulus[i].GetComponent<Stimulus>().activated && !stimulus[i].GetComponent<Stimulus>().hit)
            {
                miss.Play();
                ChangeFeedback("Fail");
            }
            stimulus[i].GetComponent<Stimulus>().activated = false;
            stimulus[i].gameObject.GetComponent<Renderer>().material = materialsDonut[0];
            stimulusGlaseado[i].GetComponent<Stimulus>().activated = false;
            stimulusGlaseado[i].gameObject.GetComponent<Renderer>().material = materialsGlaseado[0];
            stimulusVirutas[i].GetComponent<Stimulus>().activated = false;
            stimulusVirutas[i].gameObject.GetComponent<Renderer>().material = materialsGlaseado[0];
            stimulusOjos[i].GetComponent<Stimulus>().activated = false;
            stimulusOjos[i].gameObject.GetComponent<Renderer>().material = materialsDonut[0];
        }

    }

    

    public void ChangeFeedback(string value)
    {
        resultFeedback.text = value;
        Debug.Log(value);
    }

    public void CheckHit(int id)
    {
        if (!checking)
        {
            checking = true;
            if (stimulus[id].GetComponent<Stimulus>().activated)
            {
                stimulus[id].GetComponent<Stimulus>().hit = true;
                
            }
            StartCoroutine(HitLight(id));

            //StartCoroutine(HitLight(id));
        }
        

    }

    IEnumerator HitLight(int id)
    {
        
        if (stimulus[id].GetComponent<Stimulus>().activated)
        {
            ChangeFeedback("Good");
            hit.Play();
            MiniGameManager.Instance.AddGamePoints(minigamePoints);
            CleanMaterials();
        }
        else
        {
            ChangeFeedback("Fail");
            miss.Play();
            MiniGameManager.Instance.RemoveGamePoints(minigamePoints);
            CleanMaterials();
        }
        yield return null;
        yield return new WaitForSeconds(cooldownTime);
        checking = false;
        ChangeFeedback("");
        
    }


}
