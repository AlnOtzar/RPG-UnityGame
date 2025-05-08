using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    public int maxStackItems = 9;
    public InventarioSlot[] inventarioSlots;
    public GameObject inventarioItemPrefab;

    int selectedSlot = -1;

    private void Start(){
        CambioSeleccionSlot(0);
    }

    private void Update(){
        if (Input.inputString != null){
            bool IsNumero = int.TryParse(Input.inputString, out int number);
            if (IsNumero && number > 0 && number < 4) {
                CambioSeleccionSlot(number - 1);
            }
        }
    }

    void CambioSeleccionSlot(int newValue) {
        if (selectedSlot >= 0){
            inventarioSlots[selectedSlot].Deselect();    
        }
        
        inventarioSlots[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AgregarItem(Items items){
        
        // revisa si un slot tiene un item mas de una vez
        for (int i = 0; i < inventarioSlots.Length; i++){
            InventarioSlot slot = inventarioSlots[i];
            ColeccionablesPlayer itemEnSlot = slot.GetComponentInChildren<ColeccionablesPlayer>();

            if (itemEnSlot != null && 
                itemEnSlot.items == items &&
                itemEnSlot.count < maxStackItems &&
                itemEnSlot.items.stackable == true){
                itemEnSlot.count++;
                itemEnSlot.RecargarContador();
                return true;
            }
        }

        // busca un espacio vacio
        for (int i = 0; i < inventarioSlots.Length; i++){
            InventarioSlot slot = inventarioSlots[i];
            ColeccionablesPlayer itemEnSlot = slot.GetComponentInChildren<ColeccionablesPlayer>();

            if (itemEnSlot == null){
                SpawnNuevoItem(items, slot);
                return true;
            }
        }
        return false;
    }

    public void  SpawnNuevoItem (Items items, InventarioSlot slot){
        GameObject newItemGo = Instantiate(inventarioItemPrefab, slot.transform);
        ColeccionablesPlayer item = newItemGo.GetComponent<ColeccionablesPlayer>();
        item.InicializarItem(items); 
         
    }

    public Items GetSelectedItem(bool usando){
        InventarioSlot slot = inventarioSlots[selectedSlot];
        ColeccionablesPlayer itemEnSlot = slot.GetComponentInChildren<ColeccionablesPlayer>();
            if (itemEnSlot != null) {
                Items item = itemEnSlot.items;
                if (usando == true){
                    itemEnSlot.count--;
                    if (itemEnSlot.count <= 0){
                        Destroy(itemEnSlot.gameObject);
                    } else {
                        itemEnSlot.RecargarContador();
                    }

                }
                return item;
            }

            return null;
    }

}

//private bool muestraInventario;   
//     public GameObject goInventario;
//     private Sprite contenedor;

//     [SerializeField] public static string[] valoresInventario;
    
//     private int numTrofeosRata, numTrofeosSlime, numGemasVerdes, 
//             numGemasAzules, numGemasAmarilla, numGemasRosa,
//             numGemasRojas, numMonedas, numSlimes;
//     Button boton; // Botones del inventario
//     // contenedores
//     public Sprite vida, mana, carne, moneda, slime, Arco, Espada, 
//             trofeoRata, trofeoSlime, gemaRoja, gemaVerde, gemaAzul, 
//             gemaAmarilla, gemaRosa;


//     void Start()
//     {
//         muestraInventario = false;
//         BorraArreglo();

//         numTrofeosRata = 0;
//         numTrofeosSlime = 0;

//         numGemasVerdes = 0; 
//         numGemasAzules = 0; 
//         numGemasAmarilla = 0;
//         numGemasRosa = 0; 
//         numGemasRojas = 0;
        
//         numMonedas = 0; 
//         // numPocionesVida = 0; 
//         // numPocionesMana = 0; 
//         numSlimes = 0;

//         valoresInventario = new string[20]; // o el tamaño que necesites


//     }

    
//     public void StatusInventario()
//     {
//         if (muestraInventario){
//             muestraInventario = false;
//             goInventario.SetActive(false);
//             Time.timeScale = 1;
//         }else{
//             muestraInventario = true;
//             goInventario.SetActive(true);
//             Time.timeScale = 0;
//         }
//     }

//     public void EscribeEnArreglo(){
//         if (VerificaEnArreglo() == -1) { // No está en el inventario
//             for (int i = 0; i < valoresInventario.Length; i++) {
//                 if (valoresInventario[i] == "") { // Lo coloca en la primera posición
//                     valoresInventario[i] = ColeccionablesPlayer.objAcoleccionar;
//                     DibujaElementos(i);
//                     break;
//                 }
//             }
//         }else{ // Si ya está en el inventario, ubica su posición y suma uno al elemento
//         DibujaElementos(VerificaEnArreglo());
//         }
//     }

//     private int VerificaEnArreglo() {
//         int pos = -1;
//         for (int i = 0; i < valoresInventario.Length; i++) {
//             if (valoresInventario[i] == ColeccionablesPlayer.objAcoleccionar) {
//                 pos = i;
//                 break;
//             }
//         }
//         return pos;
//     }

//     public void DibujaElementos(int pos) {
//     StatusInventario();  
//     boton = GameObject.Find("elemento ("+pos+")").GetComponent<Button>();
//         switch (ColeccionablesPlayer.objAcoleccionar) {
//         // me quede en el min 17:47 donde hablaba de los tags
//             case "trofeoRata":
//                 contenedor = trofeoRata;
//                 numTrofeosRata++;
//                 boton.GetComponentInChildren<Text>().text = "x" 
//                 + numTrofeosRata.ToString();
//                 break;
//             case "trofeoSlime":
//                 contenedor = trofeoSlime;
//                 numTrofeosSlime++;
//                 boton.GetComponentInChildren<Text>().text = "x" 
//                 + numTrofeosSlime.ToString();
//                 break;
//             case "Espada":
//                 contenedor = Espada;
//                 break;
//             case "Arco":
//                 contenedor = Arco;
//                 break;
//             case "gemaVerde":
//                 contenedor = gemaVerde;
//                 numGemasVerdes++;
//                 boton.GetComponentInChildren<Text>().text = "x" 
//                 + numGemasVerdes.ToString();
//                 break;
//             case "gemaAzul":
//                 contenedor = gemaAzul;
//                 numGemasAzules++;
//                 boton.GetComponentInChildren<Text>().text = "x" 
//                 + numGemasAzules.ToString();
//                     break;
//             case "gemaAmarilla":
//                 contenedor = gemaAmarilla;
//                 numGemasAmarilla++;
//                 boton.GetComponentInChildren<Text>().text = "x" 
//                 + numGemasAmarilla.ToString();
//                 break;
//             case "gemaRosa":
//                 contenedor = gemaRosa;
//                 numGemasRosa++;
//                 boton.GetComponentInChildren<Text>().text = "x" 
//                 + numGemasRosa.ToString();
//                 break;
//             case "gemaRoja":
//                 contenedor = gemaRoja;
//                 numGemasRojas++;
//                 boton.GetComponentInChildren<Text>().text = "x" 
//                 + numGemasRojas.ToString();
//                 break;
//             case "vida":
//                 contenedor = vida;
//                 break;
//             case "mana":
//                 contenedor = mana;
//                 break;
//             case "carne":
//                 contenedor = carne;
//                 break;
//             case "moneda":
//                 contenedor = moneda;
//                 numMonedas++;
//                 boton.GetComponentInChildren<Text>().text = "x" 
//                 + numMonedas.ToString();
//                 break;
//             case "slime":
//                 contenedor = slime;
//                 numSlimes++;
//                 boton.GetComponentInChildren<Text>().text = "x" 
//                 + numSlimes.ToString();
//                 break;
//         }
//         boton.GetComponent<Image>().sprite = contenedor;
//     }

//     public void BorraArreglo(){
//         for(int i = 0; i < valoresInventario.Length; i++){
//             valoresInventario[i] = "";
//         }
//     }
