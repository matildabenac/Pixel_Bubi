using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SecretController : MonoBehaviour
{
    private TilemapRenderer floorTiles;
    private SpriteRenderer catSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        floorTiles = GameObject.Find("SecretTilemap").GetComponent<TilemapRenderer>();
        catSprite = GameObject.Find("cat").GetComponent<SpriteRenderer>();
        
        floorTiles.enabled = false;
        catSprite.enabled = false;
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            floorTiles.enabled = true;
            catSprite.enabled = true;
        }
    }
}
