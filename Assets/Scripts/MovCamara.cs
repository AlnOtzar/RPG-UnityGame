using UnityEngine;

public class MovCamara : MonoBehaviour
{
    public Camera camara;

    void OnTriggerEnter2D(Collider2D obj){
        if(obj.gameObject.tag == "portal1"){
            Vector3 posicionPlayer = new Vector3(-130,149,0);
            this.transform.position = posicionPlayer;
        } 
        if(obj.gameObject.tag == "portal2"){
            Vector3 posicionPlayer = new Vector3(29,-283,0);
            this.transform.position = posicionPlayer;
        } 
        if(obj.gameObject.tag == "portal3"){
            Vector3 posicionPlayer = new Vector3(-180,-33,0);
            this.transform.position = posicionPlayer;
        }
        if(obj.gameObject.tag == "portal4"){
            Vector3 posicionPlayer = new Vector3(-126, -63,0);
            this.transform.position = posicionPlayer;
        }  
        if(obj.gameObject.tag == "portal5"){
            Vector3 posicionPlayer = new Vector3(-230,-33,0);
            this.transform.position = posicionPlayer;
        } 
        if(obj.gameObject.tag == "portal6"){
            Vector3 posicionPlayer = new Vector3(-105,180,0);
            this.transform.position = posicionPlayer;
        }
        if(obj.gameObject.tag == "portal7"){
            Vector3 posicionPlayer = new Vector3(0,-324,0);
            this.transform.position = posicionPlayer;
        }
        if(obj.gameObject.tag == "portal8"){
            Vector3 posicionPlayer = new Vector3(18,-304,0);
            this.transform.position = posicionPlayer;
        }
        if(obj.gameObject.tag == "portal9"){
            Vector3 posicionPlayer = new Vector3(-78,-318,0);
            this.transform.position = posicionPlayer;
        }
        if(obj.gameObject.tag == "portal10"){
            Vector3 posicionPlayer = new Vector3(-47,-343,0);
            this.transform.position = posicionPlayer;
        }
        if(obj.gameObject.tag == "portal11"){
            Vector3 posicionPlayer = new Vector3(-42,-268,0);
            this.transform.position = posicionPlayer;
        }
        if(obj.gameObject.tag == "portal12"){
            Vector3 posicionPlayer = new Vector3(-112,-329,0);
            this.transform.position = posicionPlayer;
        }
        if(obj.gameObject.tag == "portal13"){
            Vector3 posicionPlayer = new Vector3(19,-266,0);
            this.transform.position = posicionPlayer;
        }
         
    }
 
}
