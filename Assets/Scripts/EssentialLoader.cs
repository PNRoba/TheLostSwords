using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialLoader : MonoBehaviour
{
    public GameObject UIScreen;
    public GameObject player;
    public GameObject gameManager;

    public GameObject audioManager;

    // Start is called before the first frame update
    void Start()
    {
        if(FadeUI.instance == null){
            FadeUI.instance = Instantiate(UIScreen).GetComponent<FadeUI>();
        }
        if(PlayerController.instance == null){
            PlayerController clone = Instantiate(player).GetComponent<PlayerController>();
            PlayerController.instance = clone;
        }

        if(GameManager.instance == null){
            Instantiate(gameManager);
        }

        if(AudioManager.instance == null){
            AudioManager clone = Instantiate(audioManager).GetComponent<AudioManager>();
            AudioManager.instance = clone;
        }
    }
}
