using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public bool reached;
    private Animator checkpointAnimation;
    // Start is called before the first frame update
    void Start()
    {
        checkpointAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D (Collider2D other) {
        if(other.tag == "Player"){
            checkpointAnimation.SetBool("IsChecked", true);
        }
    }
}
