using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public float waitToLoad;
    public GameManager gameManager;

    // Update is called once per frame
    void Update()
    {
        // Loads Main menu after some time
        if(waitToLoad>0){
            waitToLoad -= Time.deltaTime;
            if(waitToLoad <=0){
                gameManager.Load();
                SceneManager.LoadScene(gameManager.data.currentScene);
            }
        }
    }
}
