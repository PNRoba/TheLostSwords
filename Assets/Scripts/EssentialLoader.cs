using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialLoader : MonoBehaviour
{
    public GameObject UIScreen;
    public GameObject player;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}