using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class CharacterMovement : MonoBehaviour
{
    public InputManager inputManager;
    
    public GameObject currentWaypoint;
    public int idWaypoint = 2;
    public bool moving = false;
    private float t = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = currentWaypoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        t = Time.deltaTime * 20;
        if (!moving)
        {
            if (inputManager.movementInput.x > 0)
            {
                if (idWaypoint != GameManagerBoxingTopDown.Instance.stimulus.Length)
                {
                    idWaypoint++;
                    currentWaypoint = GameManagerBoxingTopDown.Instance.waypoint[idWaypoint];
                    moving = true;
                }              
            }
            else if (inputManager.movementInput.x < 0)
            {
                if (idWaypoint != 0)
                {
                    idWaypoint--;
                    currentWaypoint = GameManagerBoxingTopDown.Instance.waypoint[idWaypoint];
                    moving = true;
                }
            }
            if(inputManager.movementInput.y> 0)
            {
                //if (idWaypoint == GameManagerBoxingTopDown.Instance.activatedStimulus)
                //{
                //    GameManagerBoxingTopDown.Instance.ChangeFeedback("Good");
                //}
                //else
                //{
                //   GameManagerBoxingTopDown.Instance.ChangeFeedback("Fail");
                //}
                GameManagerBoxingTopDown.Instance.CheckHit(idWaypoint);
            }
          
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, GameManagerBoxingTopDown.Instance.waypoint[idWaypoint].transform.position, t);
        }

        if (Vector3.Distance(transform.position, currentWaypoint.transform.position) < 0.01f)
        {
            Debug.Log("¡Hemos llegado!");
            moving = false;
        }


        //currentWaypoint = GameManagerBoxingTopDown.Instance.waypoint[idWaypoint];
        //transform.position = GameManagerBoxingTopDown.Instance.waypoint[idWaypoint].transform.position;


    }
}
