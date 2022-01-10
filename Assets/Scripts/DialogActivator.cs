using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    public string[] lines; // lines to show
    private bool canActivate; // can activate dialog
    
    // this is only used at the start for new game, so it already has
    // the dialog window open
    public bool isStart; 

    // If isPerson is true, then activates name tag for the dialog
    public bool isPerson; 

    public bool markComplete; // assignd this walue to questToMark in the Quest array
    public string questToMark; // the Quest to Mark in the Quest array


    // Update is called once per frame
    void Update()
    {
        if(canActivate && Input.GetButtonDown("Fire1") && !DialogManager.instance.dialogBox.activeInHierarchy && !GameMenu.instance.theMenu.activeInHierarchy){
            DialogManager.instance.ShowDialog(lines, isPerson, isStart);
            DialogManager.instance.ShouldActivateQuest(questToMark, markComplete);
        }
        if(isStart && !DialogManager.instance.dialogBox.activeInHierarchy && !GameMenu.instance.theMenu.activeInHierarchy){
            GameMenu.instance.HPSlider.gameObject.SetActive(false);
            DialogManager.instance.ShowDialog(lines, isPerson, isStart);
            if(isStart){
                isStart = false;
            }
        }
    }

    // Allows to activate dialog if Player has entered the Collider
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            canActivate = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Player"){
            canActivate = false;
        }
    }
}
