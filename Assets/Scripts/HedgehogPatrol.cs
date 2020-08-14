using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgehogPatrol : MonoBehaviour
{
    public float speed;
    public float distance;
    public Transform groundDetection;
    
    private bool movingLeft = true;
    private bool shouldMove = true;
    
    private Animator enemyAnimation;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldMove){
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
    
        if(groundInfo.collider == false){
            if(movingLeft){
                transform.eulerAngles = new Vector3(0, -180, 0);
            } else {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            movingLeft = !movingLeft;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            enemyAnimation.SetBool("rollUp", true);
            shouldMove = false;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Player"){
            StartCoroutine("RolledCoroutine");
        }
    }
    
    public IEnumerator RolledCoroutine(){
        yield return new WaitForSeconds(3);
        enemyAnimation.SetBool("rollUp", false);
        shouldMove = true;
    }
}
