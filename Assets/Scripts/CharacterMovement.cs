using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class CharacterMovement : BaseInputManager
{
    public GameObject currentWaypoint;
    public int idWaypoint = 2;
    public bool moving = false;
    private float t = 0;
    [SerializeField] private float thresholdX = 0.5f;
    [SerializeField] private float thresholdY = 0.5f;
    
    private Vector3 lastAcceleration;
    public float accelerationThreshold = 1f;
    private float accelerationThresholdSquared;
    public float cooldown = 0.3f;
    private float lastTriggerTime = -10f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        transform.position = currentWaypoint.transform.position;
        accelerationThresholdSquared = accelerationThreshold * accelerationThreshold;
    }

    protected override void InputManagerOnMovementInputEvent(Vector3 movement)
    {
        if (Mathf.Abs(movement.x) > thresholdX)
        {
            //Debug.Log(movement);
        }
        
        Vector3 newInput = Vector3.zero;
        float sqrMagnitude = movement.sqrMagnitude;
        if (sqrMagnitude > accelerationThresholdSquared 
            && lastAcceleration.sqrMagnitude < accelerationThresholdSquared)
        {
            if (Time.time - lastTriggerTime > cooldown)
            {
                lastTriggerTime = Time.time;
                newInput = movement;
                Debug.Log("Pasa el corte: " + movement);
            }
        }
                        
        lastAcceleration = movement;
        movement = newInput;

        if (!moving)
        {
            
            if (movement.x < -thresholdX)
            {
                Debug.Log("Muevo menor ");
                if (idWaypoint != GameManagerBoxingTopDown.Instance.stimulus.Length)
                {
                    idWaypoint++;
                    if (idWaypoint > GameManagerBoxingTopDown.Instance.waypoint.Length - 1)
                    {
                        idWaypoint = GameManagerBoxingTopDown.Instance.waypoint.Length - 1;
                    }
                    currentWaypoint = GameManagerBoxingTopDown.Instance.waypoint[idWaypoint];
                    moving = true;
                }         
                return;
            }
            else if (movement.x > thresholdX)
            {
                Debug.Log("Muevo mayor ");
                if (idWaypoint != 0)
                {
                    idWaypoint--;
                    if (idWaypoint < 0)
                    {
                        idWaypoint = 0;
                    }
                    currentWaypoint = GameManagerBoxingTopDown.Instance.waypoint[idWaypoint];
                    moving = true;
                }
                return;
            }
            if(movement.z > thresholdY)
            {
                Debug.Log("Golpeo ");
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
    }

    // Update is called once per frame
    void Update()
    {
        
        t = Time.deltaTime * 20;
        
        if(moving)
        {
            transform.position = Vector3.Lerp(transform.position, GameManagerBoxingTopDown.Instance.waypoint[idWaypoint].transform.position, t);
        }

        if (Vector3.Distance(transform.position, currentWaypoint.transform.position) < 0.01f)
        {
            //Debug.Log("�Hemos llegado!");
            moving = false;
        }


        //currentWaypoint = GameManagerBoxingTopDown.Instance.waypoint[idWaypoint];
        //transform.position = GameManagerBoxingTopDown.Instance.waypoint[idWaypoint].transform.position;


    }
}
