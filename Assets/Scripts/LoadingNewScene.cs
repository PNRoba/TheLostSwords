using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingNewScene : MonoBehaviour
{
    public float waitToLoad;

    public DialogActivator dialogActivator;

    // Start is called before the first frame update
    void Start()
    {
        FadeUI.instance.FadeBlack();
    }

    // Update is called once per frame
    void Update()
    {
        // Loads first scene after some time
        if(!DialogManager.instance.dialogBox.activeInHierarchy){
            if(waitToLoad>0){
                waitToLoad -= Time.deltaTime;
                if(waitToLoad <=0){
                    FadeUI.instance.ClearFade();
                    GameMenu.instance.HPSlider.gameObject.SetActive(true);
                    SceneManager.LoadScene("Kingdom0_3-3");
                }
            }
        }
    }
}
