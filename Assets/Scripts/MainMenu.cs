using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string newGameScene; // scene: K0_3-3 // the Theatre scene
    public GameObject continueButton;

    public GameManager gameManager;

    public string loadGameScene;

    public GameObject controls;

    // Start is called before the first frame update
    void Start()
    {
        // If file exists, then game can be continued from last saved progress
        if(gameManager.FileExists()){
            continueButton.SetActive(true);
        }else{
            continueButton.SetActive(false);
        }
    }

    // Buttons in Main Menu
    public void Continue(){
        SceneManager.LoadScene(loadGameScene);
    }
    public void NewGame(){
        SceneManager.LoadScene(newGameScene);
    }
    public void Exit(){
        Application.Quit();
    }

    public void Controls(){
        controls.SetActive(true);
    }
    public void ExitControls(){
        controls.SetActive(false);
    }
}
