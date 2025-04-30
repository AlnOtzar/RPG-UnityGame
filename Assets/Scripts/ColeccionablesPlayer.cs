using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ColeccionablesPlayer : MonoBehaviour
{
    private GameObject player;
    // [SerializeField]private int[] valoresInventario; // 0-sin elemento, # elemento diferente a cero
    private Inventario inventario;
    public static string objColeccionar;


    void Start()
    {
        player = GameObject.Find("Player");   
        BorraArreglo();     
        objColeccionar = "";
        inventario = FindObjectOfType<Inventario>();
    }

    private void OnTriggerEnter2D(Collider2D obj){
        if(obj.tag == "vida" ){
            VidasPlayer vp = player.GetComponent<VidasPlayer>();
            vp.vidaActual += 5;

            if (vp.vidaActual > vp.vidasMax){
                vp.vidaActual = vp.vidasMax;
            }
            
            vp.textoVida.text = $"{vp.vidaActual} / {vp.vidasMax}";
            
            vp.DibujaVida(vp.vidaActual);
            Destroy(obj.gameObject);
        }

        if(obj.tag == "mana" ){
            Destroy(obj.gameObject);

            // aumentarmana
            

        }
        if(obj.tag == "carne" ){
            VidasPlayer vp = player.GetComponent<VidasPlayer>();
            vp.vidaActual += 2;

            if (vp.vidaActual > vp.vidasMax){
                vp.vidaActual = vp.vidasMax;
            }
            
            vp.textoVida.text = $"{vp.vidaActual} / {vp.vidasMax}";
            
            vp.DibujaVida(vp.vidaActual);
            Destroy(obj.gameObject);

        }
        if(obj.tag == "moneda" ){
            AplicaCambios(obj);

        }
        if(obj.tag == "slime" ){
            AplicaCambios(obj);

        }
        if(obj.tag == "arco" ){
            AplicaCambios(obj);

        }
        if(obj.tag == "espada" ){
            AplicaCambios(obj);

        }
    }

    // private void BorraArreglo(){
    //     for(int i=0; i < valoresInventario.Length; i++){
    //         valoresInventario[i] = 0;
    //     }
    // }

    private void AplicaCambios(Collider2D obj){
        objColeccionar = obj.tag;
        inventario.EscribeEnArreglo();
        Destroy(obj.gameObject);

    }

}
