using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatController : MonoBehaviour
{
    public GameObject press;
    public Image realBubi;
    public Text exitText;
    
    private bool canViewImage = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        press.SetActive(false);
        realBubi.gameObject.SetActive(false);
        exitText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(canViewImage){
            if(Input.GetKeyDown(KeyCode.B)){
                realBubi.gameObject.SetActive(true);
                exitText.gameObject.SetActive(true);
            }
            if(Input.GetKeyDown(KeyCode.Escape)){
                realBubi.gameObject.SetActive(false);
                exitText.gameObject.SetActive(false);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            press.SetActive(true);
            canViewImage = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Player"){
            press.SetActive(false);
            canViewImage = false;
        }
        realBubi.gameObject.SetActive(false);
        exitText.gameObject.SetActive(false);
    }
}
