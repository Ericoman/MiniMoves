using UnityEngine;

public class CarreritaMovement : MonoBehaviour
{
    public InputManager inputManager;
    
    public GameObject playerPrefab;
    
    public Transform topPosition;
    public Transform midPosition;
    public Transform botPosition;
    
    public CarreritaManager carreritaManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerPrefab.transform.position = midPosition.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(inputManager.movementInput.y > 0)
        {
            playerPrefab.transform.position = topPosition.position;
        }
        else if(inputManager.movementInput.y < 0)
        {
            playerPrefab.transform.position = botPosition.position;
        }
        else
        {
            playerPrefab.transform.position = midPosition.position;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            carreritaManager.RemovePoints();
            Destroy(other.gameObject);
        }
    }
}
