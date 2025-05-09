using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public class Items : ScriptableObject
{


    [Header("Solo gameplay")]
    public TileBase tile;
    public ItemType tipo;
    public ActionType tipoDeAccion;
    public Vector2Int rango = new Vector2Int(5, 4);

    [Header("Solo UI")]
    public bool stackable = true;
    public bool esMoneda = false;

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

    
}