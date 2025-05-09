using System.Collections;
using UnityEngine;

public class MovementImitate : MonoBehaviour
{
    public InputManager inputManager;
    public bool posing = false;
    public GameObject[] poses;
    public int currentPose = 3;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.movementInput.x > 0 && inputManager.movementInput.y ==0)
        {
            StartCoroutine(MakePose(1));
        }
        else if (inputManager.movementInput.x < 0 && inputManager.movementInput.y == 0)
        {
            StartCoroutine(MakePose(0));
        }
        else if (inputManager.movementInput.y > 0 && inputManager.movementInput.x == 0)
        {
            StartCoroutine(MakePose(2));
        }
    }
    IEnumerator MakePose(int direction)
    {
        if (!posing)
        {
            if (GameManagerImitate.Instance.posing)
            {
                GameManagerImitate.Instance.turnsbool[GameManagerImitate.Instance.turn] = true;
                posing = true;
                poses[direction].SetActive(true);
                currentPose = direction;
                GameManagerImitate.Instance.CheckPose(direction);
                GameManagerImitate.Instance.hasPosed = true;
                yield return new WaitForSeconds(0.6f);
                currentPose = 3;

                poses[direction].SetActive(false);
                posing = false;
                GameManagerImitate.Instance.hasPosed = false;
            }
            else
            {
                GameManagerImitate.Instance.callCoroutine("Fail");
            }
            
        }
        
    }
}
