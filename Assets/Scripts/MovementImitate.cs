using System.Collections;
using UnityEngine;

public class MovementImitate : BaseInputManager
{
    public InputManager inputManager;
    public bool posing = false;
    public GameObject[] poses;
    public int currentPose = 3;

    [SerializeField]
    private float thresholdX = 2f;
    [SerializeField]
    private float thresholdY = 2f;

    protected override void InputManagerOnMovementInputEvent(Vector3 movement)
    {
        if (movement.x > thresholdX && Mathf.Abs(movement.y) <= thresholdY)
        {
            StartCoroutine(MakePose(0));
        }
        else if (movement.x < -thresholdX && Mathf.Abs(movement.y) <= thresholdY)
        {
            StartCoroutine(MakePose(1));
        }
        else if (movement.y < -thresholdY && Mathf.Abs(movement.x) <= thresholdX)
        {
            StartCoroutine(MakePose(2));
        }
        else if (movement.y  > thresholdY && Mathf.Abs(movement.x) <= thresholdX)
        {
            StartCoroutine(MakePose(3));
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
                if (direction < poses.Length)
                {
                    poses[direction].SetActive(true);
                    currentPose = direction;
                }
                GameManagerImitate.Instance.CheckPose(direction);
                GameManagerImitate.Instance.hasPosed = true;
                yield return new WaitForSeconds(0.6f);
                currentPose = 3;

                if (direction < poses.Length)
                {
                    poses[direction].SetActive(false);
                }

                posing = false;
                GameManagerImitate.Instance.hasPosed = false;
            }
            else
            {
                GameManagerImitate.Instance.callCoroutine("Fail");
                yield return new WaitForSeconds(0.6f);
                posing = false;
            }
            
        }
        
    }
}
