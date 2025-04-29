using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ColeccionablesPlayer : MonoBehaviour
{
    private GameObject player;
    [SerializeField]private int[] valoresInventario; // 0-sin elemento, # elemento diferente a cero

    void Start()
    {
        player = GameObject.Find("Player");   
        BorraArreglo();     
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
            Destroy(obj.gameObject);

        }
    }

    private void BorraArreglo(){
        for(int i=0; i < valoresInventario.Length; i++){
            valoresInventario[i] = 0;

        }
    }

}
