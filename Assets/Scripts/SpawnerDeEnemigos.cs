using System.Collections;
using UnityEngine;

public class SpawnerDeEnemigos : MonoBehaviour
{
    public GameObject enemigoPrefab; 
    public Vector2 areaDeSpawnMin; 
    public Vector2 areaDeSpawnMax; 
    public int cantidadMaxima = 5; 
    public float tiempoRespawn = 3f; 

    private int enemigosActuales = 0;

    void Start()
    {
        
        for (int i = 0; i < cantidadMaxima; i++)
        {
            SpawnearEnemigo();
        }
    }

    public void SpawnearEnemigo()
    {
        if (enemigosActuales >= cantidadMaxima) return;

        float randomX = Random.Range(areaDeSpawnMin.x, areaDeSpawnMax.x);
        float randomY = Random.Range(areaDeSpawnMin.y, areaDeSpawnMax.y);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

        GameObject nuevoEnemigo = Instantiate(enemigoPrefab, spawnPosition, Quaternion.identity);
        
      
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
