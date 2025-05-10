using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonGame : BaseInputManager
{
    [Header("Botones")]
    public GameObject[] normalButtons;    
    public GameObject[] activeButtons;

    [Header("Textos")]
    public GameObject playerTurnText;
    public GameObject playerFailText;

    [Header("Controles del jugador")]
    public KeyCode[] inputKeys;     

    private List<int> pattern = new List<int>();
    private int currentStep = 0;
    private bool isPlayerTurn = false;

    [Header("Velocidad")]
    public float startDelay = 1f;
    public float minDelay = 0.3f;
    public float delayDecrease = 0.05f;
    private float currentDelay;

    [SerializeField] 
    private float thresholdX = 2f;
    [SerializeField]
    private float thresholdY = 2f;

    //[SerializeField] private float inputCooldown = 0.6f;
    bool bCanPress = true;

    void Start()
    {
        currentDelay = startDelay;
        AddToPattern();
        StartCoroutine(ShowPattern());
    }

    protected override void InputManagerOnMovementInputEvent(Vector3 movement)
    {
        if (!isPlayerTurn || !bCanPress) return;
        if (movement.x > thresholdX && Mathf.Abs(movement.y) <= thresholdY)
        {
            CheckPattern(0);
            Debug.Log("Izq");
        }
        else if (movement.x < -thresholdX && Mathf.Abs(movement.y) <= thresholdY)
        {
            CheckPattern(1);
            Debug.Log("Derecha");
        }
        else if (movement.y < -thresholdY && Mathf.Abs(movement.x) <= thresholdX)
        {
            CheckPattern(2);
            Debug.Log("Arriba");
        }
        else if (movement.y > thresholdY && Mathf.Abs(movement.x) <= thresholdX)
        {
            CheckPattern(3);
            Debug.Log("Abajo");
        }
    }

    IEnumerator InputCooldown(int index)
    {
        bCanPress = false;
        ActivateButton(index);
        yield return new WaitForSeconds(currentDelay);
        DeactivateButton(index);
        bCanPress = true;
    }
    void CheckPattern(int patternNumber)
    {
        StartCoroutine(InputCooldown(patternNumber));
        if (patternNumber == pattern[currentStep])
        {
            currentStep++;
            if (currentStep >= pattern.Count)
            {
                playerTurnText.gameObject.SetActive(true);
                StartCoroutine(NextRound());
            }
        }
        else
        {
            playerFailText.gameObject.SetActive(true);
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
        activeButtons[index].SetActive(true);
        normalButtons[index].SetActive(false);
    }

    void DeactivateButton(int index)
    {
        activeButtons[index].SetActive(false);
        normalButtons[index].SetActive(true);
    }

    IEnumerator NextRound()
    {
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
        isPlayerTurn = false;
        yield return new WaitForSeconds(1f);
        playerFailText.gameObject.SetActive(false);
        pattern.Clear();
        currentDelay = startDelay;
        AddToPattern();
        StartCoroutine(ShowPattern());
    }

}
