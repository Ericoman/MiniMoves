using UnityEngine;

public class Paralax : MonoBehaviour
{
    [Header("Objetos a Spawnear")]
    public GameObject[] objetosParaSpawnear;

    [Header("Puntos de Spawn")]
    public Transform[] puntosDeSpawn;

    [Header("Parámetros de Escala")]
    public Vector2 rangoDeEscala = new Vector2(0.5f, 2f);

    [Header("Velocidad")]
    public float velocidadBase = 1f;

    [Header("Frecuencia de Spawns")]
    public float tiempoEntreSpawns = 1f;

    [Header("Rotación")]
    public Vector3 rotacionFija = Vector3.zero;

    private float tiempoSiguienteSpawn;

    void Update()
    {
        if (Time.time >= tiempoSiguienteSpawn)
        {
            SpawnearObjeto();
            tiempoSiguienteSpawn = Time.time + tiempoEntreSpawns;
        }
    }

    void SpawnearObjeto()
    {
        if (objetosParaSpawnear.Length == 0 || puntosDeSpawn.Length == 0)
            return;

        GameObject prefab = objetosParaSpawnear[Random.Range(0, objetosParaSpawnear.Length)];
        Transform puntoSpawn = puntosDeSpawn[Random.Range(0, puntosDeSpawn.Length)];

        Quaternion rotacion;
        rotacion = Quaternion.Euler(rotacionFija);

        GameObject instancia = Instantiate(prefab, puntoSpawn.position, rotacion);

        float escala = Random.Range(rangoDeEscala.x, rangoDeEscala.y);
        instancia.transform.localScale = Vector3.one * escala;

        // Añadir movimiento al objeto instanciado
        ParalaxObj mover = instancia.AddComponent<ParalaxObj>();
        mover.velocidad = velocidadBase * escala; // Más grande = más rápido
    }
}
