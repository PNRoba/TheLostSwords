using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text dialogText; // line display text
    public Text nameText; // name display text
    public GameObject dialogBox; // line display
    public GameObject nameBox; // name display

    public string[] dialogLines;
    public int currentLine;
    public static DialogManager instance;
    private string questToMark; // the Quest to Mark in the Quest array
    private bool markQuestComplete; // assignd this walue to questToMark in the Quest array
    private bool shouldMarkQuest; // When dialog done, activates quest

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //dialogText.text = dialogLines[currentLine];
    }

    // Update is called once per frame
    void Update()
    {
        // Changes the dialog line to next one
        if(Input.GetButtonUp("Fire1")){
            if(currentLine>= dialogLines.Length){
                dialogBox.SetActive(false);
                GameManager.instance.dialogActive = false;

                if(shouldMarkQuest){
                    shouldMarkQuest = false;
                    if(markQuestComplete){
                        QuestManager.instance.MarkQuestComplete(questToMark);
                    }else{
                        QuestManager.instance.MarkQuestIncomplete(questToMark);
                    }
                }
            }else{
                CheckIfName();
                dialogText.text = dialogLines[currentLine];
            }
            currentLine++;
        }
    }

    // Starts the dialog (Opens the dialog window)
    public void ShowDialog(string[] newLines, bool isPerson, bool isStart){
        dialogLines = newLines;
        currentLine = 0;

        CheckIfName();

        dialogText.text = dialogLines[currentLine];
        dialogBox.SetActive(true);

        nameBox.SetActive(isPerson);

        GameManager.instance.dialogActive = true;

        if(isStart){
            currentLine++;
        }
    }
    
    // Checks if the current line is a name and assigns it to name text
    // and doesnt display it in the dialog window
    public void CheckIfName(){
        if(dialogLines[currentLine].StartsWith("n-")){
            nameText.text = dialogLines[currentLine].Replace("n-","");
            currentLine++;
        }
    }

    // Activates quest (Used when the dialog is done)
    public void ShouldActivateQuest(string questName, bool markAsComplete){
        questToMark = questName;
        markQuestComplete = markAsComplete;

        shouldMarkQuest = true;
    }
}
