using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public string[] questMarkerNames;
    public bool[] questMarkersComplete;

    public static QuestManager instance;

    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        questMarkersComplete = new bool[questMarkerNames.Length];

        // gameManager = GameManager.instance;
        
        // Debug.Log(gameManager.FileExists());
        // if(gameManager.FileExists()){
        //     gameManager.Load();
        //     for(int i=0; questMarkerNames.Length > i; i++){
        //         for(int j=0; gameManager.data.questNames.Length > j; j++){
        //             if(gameManager.data.questNames[j] == questMarkerNames[i]){
        //                 questMarkersComplete[i] = gameManager.data.ifComplete[j];
        //                 continue;
        //             }
        //         }
        //     }
        //     //questMarkersComplete = gameManager.data.ifComplete;
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            Debug.Log(CheckIfComplete("quest test")); //f
            MarkQuestComplete("quest test");
            Debug.Log(CheckIfComplete("quest test")); //t
            //MarkQuestIncomplete("quest test");
            //Debug.Log(CheckIfComplete("quest test")); //f
        }
    }

    public int GetQuestNumber(string questToFind){
        for(int i=0; i<questMarkerNames.Length; i++){
            if(questMarkerNames[i] == questToFind){
                return i;
            }
        }
        Debug.LogError("Quest --" + questToFind + "-- does not exist!");
        return 0;
    }

    public bool CheckIfComplete(string questToCheck){
        if(GetQuestNumber(questToCheck) != 0){
            return questMarkersComplete[GetQuestNumber(questToCheck)];
        }
        return false;
    }

    public void MarkQuestComplete(string questToMark){
        questMarkersComplete[GetQuestNumber(questToMark)] = true;
        UpdateLocalQuestObjects();
    }
    public void MarkQuestIncomplete(string questToMark){
        questMarkersComplete[GetQuestNumber(questToMark)] = false;
        UpdateLocalQuestObjects();
    }

    public void UpdateLocalQuestObjects(){
        QuestObjectActivator[] questObjects = FindObjectsOfType<QuestObjectActivator>();

        if(questObjects.Length > 0){
            for(int i=0; i< questObjects.Length; i++){
                questObjects[i].CheckCompletion();
            }
        }
    }
}