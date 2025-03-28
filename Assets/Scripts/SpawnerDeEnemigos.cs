using System.Collections;
using UnityEngine;

public class SpawnerDeEnemigos : MonoBehaviour
{
    public GameObject enemigoPrefab; // Prefab del enemigo
    public Vector2 areaDeSpawnMin; // Rango mínimo del área donde pueden aparecer
    public Vector2 areaDeSpawnMax; // Rango máximo del área donde pueden aparecer
    public int cantidadMaxima = 5; // Máximo de enemigos simultáneos
    public float tiempoRespawn = 3f; // Tiempo para que reaparezca un enemigo

    private int enemigosActuales = 0;

    void Start()
    {
        // Spawnear enemigos iniciales
        for (int i = 0; i < cantidadMaxima; i++)
        {
            SpawnearEnemigo();
        }
    }

    public void SpawnearEnemigo()
    {
        if (enemigosActuales >= cantidadMaxima) return;

        // Genera una posición aleatoria dentro del rango dado
        float randomX = Random.Range(areaDeSpawnMin.x, areaDeSpawnMax.x);
        float randomY = Random.Range(areaDeSpawnMin.y, areaDeSpawnMax.y);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

        GameObject nuevoEnemigo = Instantiate(enemigoPrefab, spawnPosition, Quaternion.identity);
        
        // Referencia al script de enemigo para que notifique cuando muera
        Enemigo scriptEnemigo = nuevoEnemigo.GetComponent<Enemigo>();
        scriptEnemigo.spawner = this;

        enemigosActuales++;
    }

    public void EnemigoEliminado()
    {
        enemigosActuales--;
        StartCoroutine(RespawnEnemigo());
    }

    private IEnumerator RespawnEnemigo()
    {
        yield return new WaitForSeconds(tiempoRespawn);
        SpawnearEnemigo();
    }
}
