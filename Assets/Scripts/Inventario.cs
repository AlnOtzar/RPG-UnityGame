using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    private bool muestraInventario;
    public GameObject goInventario;

    void Start()
    {
        muestraInventario = false;
    }

    void Update()
    {

    }

    public void StatusInventario()
    {

        if (muestraInventario)
        {
            muestraInventario = false;
            goInventario.SetActive(false);
            Time.timeScale = 1;

        }
        else
        {
            muestraInventario = true;
            goInventario.SetActive(true);
            Time.timeScale = 0;
        }
    
    }
}
