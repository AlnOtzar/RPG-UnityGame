using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    private bool muestraInventario;   
    public GameObject goInventario;

    [SerializeField] private string[] valoresInventario; 
    // "" - Sin elemento, string Elemento
    
    private int numMonedas, numGemasVerdes, numGemasAzules, 
            numGemasRojas, numGemasAmarilla, numGemasRosa,numPocionesVida, 
            numPocionesMana, numSlimes;
    Button boton; // Botones del inventario
    public Sprite TrofeoRata, TrofeoSlime, Espada, 
            Arco, gemaRoja, gemaVerde, gemaAzul, gemaAmarilla, gemaRosa, 
            pocionVida, pocionMana;


    void Start()
    {
        muestraInventario = false;
        BorraArreglo();
        numMonedas = 0; 
        numGemasVerdes = 0; 
        numGemasAzules = 0; 
        numGemasRojas = 0;
        numGemasAmarilla = 0; 
        numGemasRosa = 0;
        numPocionesVida = 0; 
        numPocionesMana = 0; 
        numSlimes = 0;

    }

    
    public void StatusInventario()
    {
        if (muestraInventario){
            muestraInventario = false;
            goInventario.SetActive(false);
            Time.timeScale = 1;
        }else{
            muestraInventario = true;
            goInventario.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void EscribeEnArreglo(){
        if (VerificaEnArreglo() == -1) { // No está en el inventario
            for (int i = 0; i < valoresInventario.Length; i++) {
                if (valoresInventario[i] == "") { // Lo coloca en la primera posición
                    valoresInventario[i] = ColeccionablesPlayer.objAcoleccionar;
                    DibujaElementos(i);
                    break;
                }
            }
        }else{ // Si ya está en el inventario, ubica su posición y suma uno al elemento
        DibujaElementos(VerificaEnArreglo());
        }
    }

    private int VerificaEnArreglo() {
        int pos = -1;
        for (int i = 0; i < valoresInventario.Length; i++) {
            if (valoresInventario[i] == ColeccionablesPlayer.objAcoleccionar) {
                pos = i;
                break;
            }
        }
        return pos;
    }

    public void DibujalEmentos(int pos) {
    StatusInventario();  
    boton = GameObject.Find("elemento ("+pos+")").GetComponent<Button>();
    switch (ColeccionablesPlayer.objAcoleccionar) {
        // me quede en el min 17:47 donde hablaba de los tags
        case "trofeoRata":
            contenedor = mochila;
            break;
        case "trofeoSlime":
            contenedor = sobre;
            break;
        case "Espada":
            contenedor = Espada;
            break;
        case "Arco":
            contenedor = Arco;
            break;
        case "gemaRoja":
            contenedor = gemaRoja;
            break;
        case "gemaVerde":
            contenedor = gemaVerde;
            break;
        case "gemaAzul":
            contenedor = gemaAzul;
            break;
        case "gemaAmarilla":
            contenedor = gemaAmarilla;
            break;
        case "gemaRosa":
            contenedor = gemaRosa;
            break;
        case "pocionVida":
            contenedor = pocionVida;
            break;
        case "pocionMana":
            contenedor = pocionMana;
            break;
        case "gemaRoja":
            contenedor = gemaRoja;
            numGemasRojas++;
            boton.GetComponentInChildrensText>().text = "x" + numGemasRojas.ToString();
            break;
        "---"

    

    private void BorraArreglo(){
        for(int i = 0; i < valoresInventario.Length; i++){
            valoresInventario[i] = "";
        }
    }

}
