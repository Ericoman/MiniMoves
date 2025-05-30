using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimonGame : MonoBehaviour
{
    [Header("Botones")]
    public GameObject[] normalButtons;    
    public GameObject[] activeButtons;

    public GameObject[] pillarGemns;
    public GameObject[] pillarInactive;
    [Header("Textos")]
    public GameObject playerTurnText;
    public GameObject playerFailText;

    [Header("Controles del jugador")]
    public KeyCode[] inputKeys;

    [SerializeField]
    private GameObject idlePose;
    [SerializeField] GameObject idleUp;
    [SerializeField] GameObject idleLeft;
    [SerializeField] GameObject idleRight;
    [SerializeField] GameObject idleDown;

    private List<int> pattern = new List<int>();
    private int currentStep = 0;
    private bool isPlayerTurn = false;

    [Header("Velocidad")]
    public float startDelay = 1f;
    public float minDelay = 0.3f;
    public float delayDecrease = 0.05f;
    private float currentDelay;

    [SerializeField]
    SingleFlickAccelerationDetector flickDetector;

    public AudioSource[] nota;
    public AudioSource music, hit, miss;

    //[SerializeField] private float inputCooldown = 0.6f;

    [SerializeField] private int points = 5;

    void Start()
    {
        currentDelay = startDelay;
        AddToPattern();
        music.Play();
        StartCoroutine(ShowPattern());
        if (flickDetector == null)
        {
            flickDetector = GetComponent<SingleFlickAccelerationDetector>();
        }
        flickDetector.FlickEvent += FlickDetectorOnFlickEvent;
    }

    private void FlickDetectorOnFlickEvent(Vector3 movement)
    {
        if (movement.x !=0 && movement.y == 0)
        {
            if (movement.x < 0)
            {
                idlePose.SetActive(false);
                idleRight.SetActive(true);
                CheckPattern(0);
                StartCoroutine(WaitPosition());


            }
            else
            {
                idlePose.SetActive(false);
                idleLeft.SetActive(true);
                CheckPattern(1);
                StartCoroutine(WaitPosition());
            }
        }
        else if (movement.y !=0 && movement.x == 0)
        {
            if (movement.y < 0)
            {
                idlePose.SetActive(false);
                idleUp.SetActive(true);
                CheckPattern(2);
                StartCoroutine(WaitPosition());
            }
            else
            {
                idlePose.SetActive(false);
                idleDown.SetActive(true);
                CheckPattern(3);
                StartCoroutine(WaitPosition());
            }
        }
    }

    IEnumerator InputCooldown(int index)
    {
        ActivateButton(index);
        yield return new WaitForSeconds(flickDetector.GetCooldownTime());
        DeactivateButton(index);
    }
    void CheckPattern(int patternNumber)
    {
        if (!isPlayerTurn) return;
        if(currentStep >= pattern.Count) return;
        StartCoroutine(InputCooldown(patternNumber));
        if (patternNumber == pattern[currentStep])
        {
            currentStep++;
            if (currentStep >= pattern.Count)
            {
                hit.Play();
                playerTurnText.gameObject.SetActive(true);
                StartCoroutine(NextRound());
            }
        }
        else
        {
            miss.Play();
            playerFailText.gameObject.SetActive(true);
            playerTurnText.gameObject.SetActive(false);
            ResetGame();
        }
    }

    void AddToPattern()
    {
        int newIndex;
        do
        {
            newIndex = Random.Range(0, normalButtons.Length);
        } while (pattern.Count > 0 && newIndex == pattern[pattern.Count - 1]);

        pattern.Add(newIndex);
    }

    IEnumerator ShowPattern()
    {
        isPlayerTurn = false;
        currentStep = 0;
        playerTurnText.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);


        for (int i = 0; i < pattern.Count; i++)
        {
            int index = pattern[i];
            ActivateButton(index);
            yield return new WaitForSeconds(currentDelay);
            DeactivateButton(index);
            yield return new WaitForSeconds(0.2f);
        }

        isPlayerTurn = true;
        playerTurnText.gameObject.SetActive(true);
        currentDelay = Mathf.Max(minDelay, currentDelay - delayDecrease);
    }

    void ActivateButton(int index)
    {
        nota[index].Play();
        activeButtons[index].SetActive(true);
        normalButtons[index].SetActive(false);
        pillarGemns[index].SetActive(true);
        pillarInactive[index].SetActive(false);
    }

    void DeactivateButton(int index)
    {
        activeButtons[index].SetActive(false);
        normalButtons[index].SetActive(true);
        pillarGemns[index].SetActive(false);
        pillarInactive[index].SetActive(true);
    }

    IEnumerator NextRound()
    {
        MiniGameManager.Instance.AddGamePoints((int)(points*pattern.Count*0.5f));
        yield return new WaitForSeconds(1f);
        playerTurnText.gameObject.SetActive(false);
        AddToPattern();
        StartCoroutine(ShowPattern());
    }

    void ResetGame()
    {
        StartCoroutine(ResetGameCoroutine());
    }

    IEnumerator ResetGameCoroutine()
    {
        MiniGameManager.Instance.RemoveGamePoints(points);
        isPlayerTurn = false;
        yield return new WaitForSeconds(1f);
        playerFailText.gameObject.SetActive(false);
        currentStep = 3;
        pattern.Clear();
        currentDelay = startDelay;
        AddToPattern();
        StartCoroutine(ShowPattern());
    }

    private void OnDestroy()
    {
        if (flickDetector != null)
        {
            flickDetector.FlickEvent -= FlickDetectorOnFlickEvent;
        }
    }

    IEnumerator WaitPosition()
    {
        yield return new WaitForSeconds(0.3f);
        idlePose.SetActive(true);
        idleLeft.SetActive(false);
        idleUp.SetActive(false);
        idleRight.SetActive(false);
        idleDown.SetActive(false);
    }
}
