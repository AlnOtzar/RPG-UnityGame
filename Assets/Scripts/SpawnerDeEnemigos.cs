using System.Collections;
using UnityEngine;

public class SpawnerDeEnemigos : MonoBehaviour
{
    public GameObject enemigoPrefab; // Prefab del enemigo
    public Vector2 areaDeSpawnMin; // Rango mínimo del área donde pueden aparecer, aqui deber poner las coordenadas tanto minimas como maximas
    public Vector2 areaDeSpawnMax; // Rango máximo del área donde pueden aparecer
    public int cantidadMaxima = 5; // Máximo de enemigos simultáneos
    public float tiempoRespawn = 3f; // Tiempo para que reaparezca un enemigo

    private int enemigosActuales = 0;

    void Start()
    {
        
        for (int i = 0; i < cantidadMaxima; i++) //aparece un enemigo hasta llegar al limite
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
        
        // Referencia al script de enemigo para que notifique cuando muera
        //aqui debes poner el de tus enemigos
        Enemigo scriptEnemigo = nuevoEnemigo.GetComponent<Enemigo>();
        scriptEnemigo.spawner = this;

        enemigosActuales++;
    }

    public void EnemigoEliminado()
    {
        enemigosActuales--;
        StartCoroutine(RespawnEnemigo());
    }

    private IEnumerator RespawnEnemigo() //tiempo que tarda en aparecer
    {
        yield return new WaitForSeconds(tiempoRespawn);
        SpawnearEnemigo();
    }
}
