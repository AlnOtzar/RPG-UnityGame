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

    private void Start()
    {
        CambioSeleccionSlot(0);
    }

    private void Update()
    {
        if (Input.inputString != null)
        {
            bool esNumero = int.TryParse(Input.inputString, out int number);
            if (esNumero && number > 0 && number <= inventarioSlots.Length)
            {
                CambioSeleccionSlot(number - 1);
            }

            // Usa el ítem del slot seleccionado con tecla U
            if (Input.GetKeyDown(KeyCode.U))
            {
                Items item = GetSelectedItem(usando: true);
                if (item != null)
                {
                    Debug.Log("Usaste: " + item.nombre);
                }
            }
        }
    }

    void CambioSeleccionSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventarioSlots[selectedSlot].Deselect();
        }

        inventarioSlots[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AgregarItem(Items items)
    {
        for (int i = 0; i < inventarioSlots.Length; i++)
        {
            InventarioSlot slot = inventarioSlots[i];
            ColeccionablesPlayer itemEnSlot = slot.GetComponentInChildren<ColeccionablesPlayer>();

            if (itemEnSlot != null &&
                itemEnSlot.items == items &&
                itemEnSlot.count < maxStackItems &&
                itemEnSlot.items.stackable == true)
            {
                itemEnSlot.count++;
                itemEnSlot.RecargarContador();
                return true;
            }
        }

        for (int i = 0; i < inventarioSlots.Length; i++)
        {
            InventarioSlot slot = inventarioSlots[i];
            ColeccionablesPlayer itemEnSlot = slot.GetComponentInChildren<ColeccionablesPlayer>();

            if (itemEnSlot == null)
            {
                SpawnNuevoItem(items, slot);
                return true;
            }
        }

        return false;
    }

    public void SpawnNuevoItem(Items items, InventarioSlot slot)
    {
        GameObject newItemGo = Instantiate(inventarioItemPrefab, slot.transform);
        ColeccionablesPlayer item = newItemGo.GetComponent<ColeccionablesPlayer>();
        item.InicializarItem(items);
    }

    public Items GetSelectedItem(bool usando)
    {
        InventarioSlot slot = inventarioSlots[selectedSlot];
        ColeccionablesPlayer itemEnSlot = slot.GetComponentInChildren<ColeccionablesPlayer>();

        if (itemEnSlot != null)
        {
            Items item = itemEnSlot.items;

            if (usando)
            {
                // Verificamos si el ítem es consumible
                if (item.tipoDeAccion == Items.ActionType.nada)
                {
                    Debug.Log($"{item.nombre} no es un objeto consumible.");
                    return null;
                }

                // Consumir y aplicar efecto
                itemEnSlot.count--;
                item.Usar(GameObject.FindGameObjectWithTag("Player"));

                if (itemEnSlot.count <= 0)
                {
                    Destroy(itemEnSlot.gameObject);
                }
                else
                {
                    itemEnSlot.RecargarContador();
                }
            }

            return item;
        }

        return null;
    }

    public bool GastarItemPorNombre(string nombreItem, int cantidad)
    {
        for (int i = 0; i < inventarioSlots.Length; i++)
        {
            InventarioSlot slot = inventarioSlots[i];
            ColeccionablesPlayer itemEnSlot = slot.GetComponentInChildren<ColeccionablesPlayer>();

            if (itemEnSlot != null && itemEnSlot.items.nombre == nombreItem)
            {
                // Verificamos si es consumible
                if (itemEnSlot.items.tipoDeAccion == Items.ActionType.nada)
                {
                    Debug.Log($"{nombreItem} no es un objeto consumible.");
                    return false;
                }

                if (itemEnSlot.count >= cantidad)
                {
                    itemEnSlot.count -= cantidad;

                    if (itemEnSlot.count <= 0)
                    {
                        Destroy(itemEnSlot.gameObject);
                    }
                    else
                    {
                        itemEnSlot.RecargarContador();
                    }

                    return true;
                }
                else
                {
                    Debug.Log("No hay suficientes " + nombreItem + " en el inventario.");
                    return false;
                }
            }
        }

        Debug.Log("Item " + nombreItem + " no encontrado en el inventario.");
        return false;
    }
}
