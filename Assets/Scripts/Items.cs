using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public class Items : ScriptableObject
{
    [Header("Datos base")]
    public string nombre;
    

    [Header("Solo gameplay")]
    public TileBase tile;
    public ItemType tipo;
    public ActionType tipoDeAccion;
    public Vector2Int rango = new Vector2Int(5, 4);
    public int cantidadEfecto = 10;
    


    [Header("Solo UI")]
    public bool stackable = true;

    [Header("Ambos")]
    public Sprite image;

    public enum ItemType
    {
        Estadistica,
        Coleccionable
    }

    public enum ActionType
    {
        curar,
        manaear,
        nada
    }

    public void Usar(GameObject jugador)
    {
        VidasPlayer stats = jugador.GetComponent<VidasPlayer>();
        if (stats != null)
        {
            switch (tipoDeAccion)
            {
                case ActionType.curar:
                    stats.vidaActual = Mathf.Min(stats.vidaActual + cantidadEfecto, stats.vidasMax);
                    stats.DibujaVida(stats.vidaActual);
                    stats.ActualizarUI();
                    break;

                case ActionType.manaear:
                    stats.energiaActual = Mathf.Min(stats.energiaActual + cantidadEfecto, stats.energiaMax);
                    stats.DibujaEnergia(stats.energiaActual);
                    stats.ActualizarUI();
                    break;

                case ActionType.nada:
                    // No hace nada
                    break;
            }
        }
    }


}
