using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitArea : MonoBehaviour
{
    public string areaToLoad;
    public string exitName; // area transition name

    public AreaEnterance theEnterance;

    public float waitToLoad = 1f;
    public bool loadAfterFade; //

    public bool ifSouthNorthExit; // 1 - up, down enterances/exits; 0 left, right enterances/exits
    public bool ifNormalExit;

    // Start is called before the first frame update
    void Start()
    {
        theEnterance.enteranceName = exitName;
    }

    // Update is called once per frame
    void Update()
    {
        // Waits until faded and then loads new scene
        if(loadAfterFade){
            waitToLoad-=Time.deltaTime;
            if(waitToLoad<=0){
                loadAfterFade = false;
                SceneManager.LoadScene(areaToLoad);
            }
        }
    }

    // When enters, starts fading. Player can't move.
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){

            PlayerController.instance.ifSouthNorthEnterance = ifSouthNorthExit;
            PlayerController.instance.ifNormalExit = ifNormalExit;

            //SceneManager.LoadScene(areaToLoad);
            loadAfterFade = true;
            GameManager.instance.fadingBetweenAreas = true;

            FadeUI.instance.FadeBlack();


            // exit name of scene from which Player came from assigned to Player exit name(remember)
            PlayerController.instance.exitName = exitName;
        }
    }
}
