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
    public bool loadAfterFade;

    // Start is called before the first frame update
    void Start()
    {
        theEnterance.enteranceName = exitName;
    }

    // Update is called once per frame
    void Update()
    {
        if(loadAfterFade){
            waitToLoad-=Time.deltaTime;
            if(waitToLoad<=0){
                loadAfterFade = false;
                SceneManager.LoadScene(areaToLoad);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){

            //SceneManager.LoadScene(areaToLoad);
            loadAfterFade = true;
            GameManager.instance.fadingBetweenAreas = true;

            FadeUI.instance.FadeBlack();


            // exit name of scene from which Player came from assigned to Player exit name(remember)
            PlayerController.instance.exitName = exitName;
        }
    }
}
