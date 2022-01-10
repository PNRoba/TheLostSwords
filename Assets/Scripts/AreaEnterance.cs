using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEnterance : MonoBehaviour
{
    public string enteranceName;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 p = PlayerController.instance.transform.position;
        if(enteranceName == PlayerController.instance.exitName){
            //PlayerController.instance.transform.position = transform.position;
            if(!PlayerController.instance.ifNormalExit){
                if(PlayerController.instance.ifSouthNorthEnterance){
                    p.y = transform.position.y;
                    PlayerController.instance.transform.position = p;
                }else{
                    p.x = transform.position.x;
                    PlayerController.instance.transform.position = p;
                }
            }else{
                PlayerController.instance.transform.position = transform.position;
            }
        }

        FadeUI.instance.ClearFade();
        GameManager.instance.fadingBetweenAreas = false;
    }
}
