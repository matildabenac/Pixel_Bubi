using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float respawnDelay;
    public PlayerController gamePlayer;
    private Rigidbody2D gamePlayerRigidBody;
    private CinemachineVirtualCamera VCam;
    
    public int stars = 0;
    
    public Text starScore;
    public Image hud;
    public Button exitButton;
    
    public Sprite[] hudSprites = new Sprite[4];
    
    private int lives = 3;
    
    
    // Start is called before the first frame update
    void Start()
    {
        gamePlayer = FindObjectOfType<PlayerController>();
        gamePlayerRigidBody = gamePlayer.gameObject.GetComponent<Rigidbody2D>();
        
        VCam = FindObjectOfType<CinemachineVirtualCamera>();
        
        starScore.text = "0";
        
        hud.sprite = hudSprites[3];
                
        exitButton.onClick.AddListener(Exit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Respawn(){
        if(lives > 0){
            StartCoroutine("RespawnCoroutine");
            --lives;
            hud.sprite = hudSprites[lives];
        } else {
            StartCoroutine("LoseCoroutine");
        }
    }
    
    public void AddStars(int value){
        stars += value;
        starScore.text = stars.ToString();
    }
    
    public void EndReached(){
        StartCoroutine("EndReachedCoroutine");
    }
    
    
    public IEnumerator RespawnCoroutine(){
        RigidbodyConstraints2D tmpConstraints = gamePlayerRigidBody.constraints;
        
        gamePlayerRigidBody.velocity = new Vector2(0, 0);
        gamePlayerRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        gamePlayer.enabled = false;

        VCam.enabled = false;
        
        yield return new WaitForSeconds(respawnDelay);
        gamePlayer.transform.position = gamePlayer.respawnPoint;
        
        VCam.enabled = true;
        
        gamePlayerRigidBody.constraints = tmpConstraints;
        gamePlayer.enabled = true;
    }
    
    public IEnumerator LoseCoroutine(){
        gamePlayerRigidBody.velocity = new Vector2(0, 0);
        gamePlayerRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        
        VCam.enabled = false;
        
        Static.statusText = "YOU LOST!";
        yield return new WaitForSeconds(respawnDelay);
        
        SceneManager.LoadScene("Menu");
    }
    
    public IEnumerator EndReachedCoroutine(){
        gamePlayerRigidBody.velocity = new Vector2(0, 0);
        gamePlayerRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        
        Static.statusText = "YOU WON!";
        yield return new WaitForSeconds(respawnDelay);
        
        SceneManager.LoadScene("Menu");
    }
    
    void Exit() {
        Static.statusText = "";
        SceneManager.LoadScene("Menu");
    }
}
