using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEnterance : MonoBehaviour
{
    public string enteranceName;

    // Start is called before the first frame update
    void Start()
    {
        if(enteranceName == PlayerController.instance.exitName){
            PlayerController.instance.transform.position = transform.position;
        }

        FadeUI.instance.ClearFade();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
