// using UnityEngine;
// using System.Collections;
// using UnityEngine.UI;

// public class ColeccionablesPlayer : MonoBehaviour
// {
//     private GameObject player;
//     public static string objAColeccionar;
//     private Inventario inventario;
    



//     void Start()
//     {
//         player = GameObject.Find("Player");   
//         objAColeccionar = "";
//         inventario = FindObjectOfType<Inventario>();
//     }

//     private void OnTriggerEnter2D(Collider2D obj){
//         if(obj.tag == "vida" ){
//             VidasPlayer vp = player.GetComponent<VidasPlayer>();
//             vp.vidaActual += 5;

//             if (vp.vidaActual > vp.vidasMax){
//                 vp.vidaActual = vp.vidasMax;
//             }
            
//             vp.textoVida.text = $"{vp.vidaActual} / {vp.vidasMax}";
            
//             vp.DibujaVida(vp.vidaActual);
//             Destroy(obj.gameObject);
//         }

//         if(obj.tag == "mana" ){
//             AplicaCambios(obj);

//             // aumentarmana
            

//         }
//         if(obj.tag == "carne" ){
//             VidasPlayer vp = player.GetComponent<VidasPlayer>();
//             vp.vidaActual += 2;

//             if (vp.vidaActual > vp.vidasMax){
//                 vp.vidaActual = vp.vidasMax;
//             }
            
//             vp.textoVida.text = $"{vp.vidaActual} / {vp.vidasMax}";
            
//             vp.DibujaVida(vp.vidaActual);
//             Destroy(obj.gameObject);

//         }
//         if(obj.tag == "moneda" ){
//             AplicaCambios(obj);

//         }
//         if(obj.tag == "slime" ){
//             AplicaCambios(obj);

//         }
//         if(obj.tag == "Arco" ){
//             AplicaCambios(obj);

//         }
//         if(obj.tag == "Espada" ){
//             AplicaCambios(obj);

//         }
//         if(obj.tag == "trofeoRata" ){
//             AplicaCambios(obj);

//         }
//         if(obj.tag == "trofeoSlime" ){
//             AplicaCambios(obj);

//         }
//         if(obj.tag == "gemaRoja" ){
//             AplicaCambios(obj);

//         }
//         if(obj.tag == "gemaVerde" ){
//             AplicaCambios(obj);

//         }
//         if(obj.tag == "gemaAzul" ){
//             AplicaCambios(obj);

//         }
//         if(obj.tag == "gemaAmarilla" ){
//             AplicaCambios(obj);

//         }
//         if(obj.tag == "gemaRosa" ){
//             AplicaCambios(obj);

//         }
//         if(obj.tag == "gemaVerde" ){
//             AplicaCambios(obj);

//         }

//     }

//     private void AplicaCambios(Collider2D obj){
//         objAColeccionar = obj.tag;
//         inventario.EscribeEnArreglo();
//         Destroy(obj.gameObject);

//     }

// }
