using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingEndScene : MonoBehaviour
{
    public float waitToLoad;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        GameMenu.instance.HPSlider.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Loads Main menu after some time
        if(waitToLoad>0){
            waitToLoad -= Time.deltaTime;
            if(waitToLoad <=0){
                Destroy(GameManager.instance.gameObject);
                Destroy(PlayerController.instance.gameObject);
                Destroy(AudioManager.instance.gameObject);
                Destroy(gameObject);
                Destroy(GameMenu.instance.gameObject);
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
