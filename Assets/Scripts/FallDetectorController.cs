using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetectorController : MonoBehaviour
{
    public GameObject player;
    private Vector3 tmp;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tmp = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        transform.position = tmp;
    }
}
