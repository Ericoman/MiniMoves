using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    private CarreritaManager carreritaManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        carreritaManager = FindFirstObjectByType<CarreritaManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * carreritaManager.obstacleSpeed * Time.deltaTime);
    }
}
