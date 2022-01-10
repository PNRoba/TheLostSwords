using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // Finds quest number
    public int GetQuestNumber(string questToFind){
        for(int i=0; i<questMarkerNames.Length; i++){
            if(questMarkerNames[i] == questToFind){
                return i;
            }
        }
        Debug.LogError("Quest --" + questToFind + "-- does not exist!");
        return 0;
    }

    // Check if quest is complete
    public bool CheckIfComplete(string questToCheck){
        if(GetQuestNumber(questToCheck) != 0){
            return questMarkersComplete[GetQuestNumber(questToCheck)];
        }
        return false;
    }

    // Mark Quest Complete
    public void MarkQuestComplete(string questToMark){
        questMarkersComplete[GetQuestNumber(questToMark)] = true;
        UpdateLocalQuestObjects();
    }

    // Mark Quest Incomplete
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