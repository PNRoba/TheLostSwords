using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{
    public static FadeUI instance;
    public Image fadeImg;

    private bool fadeToBlack;
    private bool fadeToClear;
    public float fadeSpeed;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeToBlack){
            fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, Mathf.MoveTowards(fadeImg.color.a, 1f, fadeSpeed * Time.deltaTime));
            if(fadeImg.color.a == 1f){
                fadeToBlack=false;
            }
        }
        if(fadeToClear){
            fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, Mathf.MoveTowards(fadeImg.color.a, 0f, fadeSpeed * Time.deltaTime));
            if(fadeImg.color.a == 0f){
                fadeToClear=false;
            }
        }
    }

    public void FadeBlack(){
        fadeToBlack=true;
        fadeToClear=false;
    }

    public void ClearFade(){
        fadeToBlack=false;
        fadeToClear=true;
    }
}
