using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public Button playButton;
    public Text statusText;
    public Button exitButton;
    
    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(TaskOnClick);
        exitButton.onClick.AddListener(Exit);
        statusText.text = Static.statusText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void TaskOnClick(){
        SceneManager.LoadScene("SampleScene");
	}
	
	void Exit(){
        Application.Quit();
    }

}
