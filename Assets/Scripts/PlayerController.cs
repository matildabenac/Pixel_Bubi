using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
	public float speed = 5f;
    public float jumpForce = 500f;
 	private float movement = 0f;
	private Rigidbody2D playerRigidBody;

    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;

	public Transform ceilingCheckPoint;
    public float ceilingCheckRadius;
	private bool isTouchingCeiling;

    private Animator playerAnimation;
    
    public Vector3 respawnPoint;
    
    public LevelManager gameLevelManager;

    
    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    	playerAnimation = GetComponent<Animator>();
        respawnPoint = new Vector3(-7.951f, -1.5f, 0);
        
        gameLevelManager = FindObjectOfType<LevelManager>();
    }

    
    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
		isTouchingCeiling = Physics2D.OverlapCircle(ceilingCheckPoint.position, ceilingCheckRadius, groundLayer);

        movement = Input.GetAxis("Horizontal");
        if(movement != 0f){
            playerRigidBody.velocity = new Vector2(movement*speed, playerRigidBody.velocity.y);
			float scale_x = Mathf.Abs(transform.localScale.x);
			float scale_y = Mathf.Abs(transform.localScale.y);
            transform.localScale = new Vector2((movement > 0 ? 1 : -1) * scale_x, scale_y);
		}else {
            playerRigidBody.velocity = new Vector2(0, playerRigidBody.velocity.y);
        }
        
        
        if(isTouchingGround && Input.GetButtonDown("Jump")){
            playerRigidBody.AddForce(new Vector2(0, jumpForce));
        }
	
		if((isTouchingGround && Input.GetButton("Crouch")) || isTouchingCeiling){
			playerAnimation.SetBool("isCrouching", true);
		} else {
			playerAnimation.SetBool("isCrouching", false);
		}

        playerAnimation.SetFloat("runSpeed", Mathf.Abs(playerRigidBody.velocity.x));
        playerAnimation.SetFloat("jumpSpeed", playerRigidBody.velocity.y);
        playerAnimation.SetBool("onGround", isTouchingGround);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 tmp;
        float offset = 0.5f;
        
        if (other.tag == "FallDetector" || other.tag == "Enemy")
        {
            gameLevelManager.Respawn();
        }
        
        if (other.tag == "Checkpoint" && !other.gameObject.GetComponent<CheckpointController>().reached){
            tmp = new Vector3(other.transform.position.x, other.transform.position.y + offset, other.transform.position.z);
            respawnPoint = tmp;
            other.gameObject.GetComponent<CheckpointController>().reached = true;
        }
        
        if(other.tag == "Endpoint"){
            gameLevelManager.EndReached();
        }
        
        if(other.tag == "Cat"){
            Debug.Log("You found a cat!");
        }
    }
}
