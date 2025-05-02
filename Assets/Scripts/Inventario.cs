using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    [Header("Configuración")]
    private bool muestraInventario;   
    public GameObject goInventario;
    
    [Header("Contadores")]
    private int numTrofeosRata, numTrofeosSlime, numGemasVerdes, 
                numGemasAzules, numGemasAmarilla, numGemasRosa,
                numGemasRojas, numMonedas, numSlimes;

    [Header("Referencias UI")]
    [SerializeField] private Button[] botonesInventario;
    
    [Header("Sprites de Items")]
    [SerializeField] private Sprite vida, mana, carne, moneda, slime, Arco, Espada, 
                         trofeoRata, trofeoSlime, gemaRoja, gemaVerde, gemaAzul, 
                         gemaAmarilla, gemaRosa;

    [Header("Datos")]
    public static string[] valoresInventario;

    void Start()
    {
        InicializarInventario();
    }

    private void InicializarInventario()
    {
        muestraInventario = false;
        BorraArreglo();
        ResetContadores();
        
        // Opcional: Buscar automáticamente los botones si no se asignan desde el inspector
        if(botonesInventario == null || botonesInventario.Length == 0)
        {
            botonesInventario = GetComponentsInChildren<Button>();
        }
    }

    private void ResetContadores()
    {
        numTrofeosRata = 0;
        numTrofeosSlime = 0;
        numGemasVerdes = 0; 
        numGemasAzules = 0; 
        numGemasAmarilla = 0;
        numGemasRosa = 0; 
        numGemasRojas = 0;
        numMonedas = 0; 
        numSlimes = 0;
    }

    public void StatusInventario()
    {
        muestraInventario = !muestraInventario;
        goInventario.SetActive(muestraInventario);
        Time.timeScale = muestraInventario ? 0 : 1;
    }

    public void EscribeEnArreglo()
    {
        int pos = VerificaEnArreglo();
        if(pos == -1) // No está en el inventario
        { 
            for(int i = 0; i < valoresInventario.Length; i++)
            {
                if(valoresInventario[i] == "")
                {
                    valoresInventario[i] = ColeccionablesPlayer.objAcoleccionar;
                    DibujaElementos(i);
                    break;
                }
            }
        }
        else // Si ya está en el inventario
        {
            DibujaElementos(pos);
        }
    }

    private int VerificaEnArreglo()
    {
        for(int i = 0; i < valoresInventario.Length; i++)
        {
            if(valoresInventario[i] == ColeccionablesPlayer.objAcoleccionar)
            {
                return i;
            }
        }
        return -1;
    }

    public void DibujaElementos(int pos)
    {
        if(pos < 0 || pos >= botonesInventario.Length)
        {
            Debug.LogError("Índice de inventario fuera de rango: " + pos);
            return;
        }

        Button boton = botonesInventario[pos];
        Image imagen = boton.GetComponent<Image>();
        Text texto = boton.GetComponentInChildren<Text>();

        string item = ColeccionablesPlayer.objAcoleccionar;
        
        switch(item)
        {
            case "trofeoRata":
                imagen.sprite = trofeoRata;
                texto.text = $"x{++numTrofeosRata}";
                break;
                
            case "trofeoSlime":
                imagen.sprite = trofeoSlime;
                texto.text = $"x{++numTrofeosSlime}";
                break;
                
            case "Espada":
                imagen.sprite = Espada;
                texto.text = ""; // Limpiar texto si es un item único
                break;
                
            case "Arco":
                imagen.sprite = Arco;
                texto.text = "";
                break;
                
            case "gemaVerde":
                imagen.sprite = gemaVerde;
                texto.text = $"x{++numGemasVerdes}";
                break;
                
            // ... otros casos similares
                
            default:
                Debug.LogWarning($"Item no reconocido: {item}");
                break;
        }
    }

    public void BorraArreglo()
    {
        for(int i = 0; i < valoresInventario.Length; i++)
        {
            valoresInventario[i] = "";
        }
    }
}