using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMarker : MonoBehaviour
{
    // Start is called before the first frame update

    public string questToMark; // Manually set. Name of quest to mark
    public bool markComplete; // Manually set. Bool given to Quest Completion Status

    public bool markOnEnter; // Manually set. true = marks on enter, false = marks on click, if in collider.
    public bool canMark;

    public bool deactiateOnMarking; // Manually set. true = deactivates object.

    // Update is called once per frame
    void Update()
    {
        if(canMark && Input.GetButtonDown("Fire1")){
            canMark = false;
            MarkQuest();
        }
    }
    
    public void MarkQuest(){
        if(markComplete){
            QuestManager.instance.MarkQuestComplete(questToMark);
        }else{
            QuestManager.instance.MarkQuestIncomplete(questToMark);
        }

        gameObject.SetActive(!deactiateOnMarking);
    }
    
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            if(markOnEnter){
                MarkQuest();
            }else{
                canMark = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Player"){
            canMark = false;
        }
    }
}
