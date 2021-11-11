using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text dialogText;
    public Text nameText;
    public GameObject dialogBox;
    public GameObject nameBox;

    public string[] dialogLines;

    public int currentLine;

    public static DialogManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //dialogText.text = dialogLines[currentLine];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonUp("Fire1")){
            if(currentLine>= dialogLines.Length){
                dialogBox.SetActive(false);
                PlayerController.instance.canMove = true;
            }else{
                dialogText.text = dialogLines[currentLine];
            }
            currentLine++;
        }
    }

    public void ShowDialog(string[] newLines){
        dialogLines = newLines;
        currentLine = 0;

        dialogText.text = dialogLines[currentLine];
        dialogBox.SetActive(true);

        PlayerController.instance.canMove = false;
    }
}
