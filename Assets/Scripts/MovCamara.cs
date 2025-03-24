using UnityEngine;

public class MovCamara : MonoBehaviour
{
    public Camera camara;

    void OnTriggerEnter2D(Collider2D obj){
        if(obj.gameObject.tag == "portal1"){
            //Vector3 posicioncamara = new Vector3(4,20,-10);
            //camara.transform.position = posicioncamara;
            Vector3 posicionPlayer = new Vector3(125,182,0);
            this.transform.position = posicionPlayer;
        } 
        if(obj.gameObject.tag == "portal2"){
            //Vector3 posicioncamara = new Vector3(3.73f,-23.38f,-10);
            //camara.transform.position = posicioncamara;
            Vector3 posicionPlayer = new Vector3(-3.5f,-15.34f,0);
            this.transform.position = posicionPlayer;
        } 
        if(obj.gameObject.tag == "portal3"){
            //Vector3 posicioncamara = new Vector3(80,0,-10);
            //camara.transform.position = posicioncamara;
            Vector3 posicionPlayer = new Vector3(80,0,0);
            this.transform.position = posicionPlayer;
        }
        if(obj.gameObject.tag == "portal4"){
            //Vector3 posicioncamara = new Vector3(4,0,-10);
            //camara.transform.position = posicioncamara;
            Vector3 posicionPlayer = new Vector3(128, -30,0);
            this.transform.position = posicionPlayer;
        }  
        if(obj.gameObject.tag == "portal5"){
            //Vector3 posicioncamara = new Vector3(4,0,-10);
            //camara.transform.position = posicioncamara;
            Vector3 posicionPlayer = new Vector3(26,0,0);
            this.transform.position = posicionPlayer;
        } 
        if(obj.gameObject.tag == "portal6"){
            Vector3 posicioncamara = new Vector3(4,0,-10);
            camara.transform.position = posicioncamara;
            Vector3 posicionPlayer = new Vector3(-4,7.9f,0);
            this.transform.position = posicionPlayer;
        } 
    }
 
}
