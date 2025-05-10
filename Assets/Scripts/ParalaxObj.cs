using UnityEngine;

public class ParalaxObj : MonoBehaviour
{
    public float velocidad = 1f;

    void Update()
    {
        transform.position += Vector3.left * velocidad * Time.deltaTime;

        // Destruir cuando salga de pantalla
        if (transform.position.x < -20f) // Ajusta esto según tu escena
        {
            Destroy(gameObject);
        }
    }
}
