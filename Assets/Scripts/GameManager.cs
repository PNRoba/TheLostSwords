using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CharacterStats[] playerStats; // Player information
    public bool gameMenuOpen, dialogActive, fadingBetweenAreas, shopOpen; // All displays, that are true, if open

    public string[] theItems; // Player inventory (Items)
    public int[] numberOfItem; // Player inventory (Amounts of items)
    public Item[] referenceItems; // all existing items

    public int currentMoney;

    public PlayerData data; // Object used for saving

    public PlayerController player;
    public string file = "player.json";

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);

        player = FindObjectOfType<PlayerController>();

        data = new PlayerData();

        ItemSorter();
    }

    // Update is called once per frame
    void Update()
    {
        // If any display is open, Player cant move
        if(gameMenuOpen || dialogActive || fadingBetweenAreas || shopOpen){
            PlayerController.instance.canMove = false;
        }else{
            PlayerController.instance.canMove = true;
        }

        // Checks if Players health is less than or equal to 0
        // if yes, Player death animation is played
        if(instance.playerStats[0].currentHP <=0){
            player.myAnim.SetBool("isDead", true);
        }
    }

    // Returns Item from the Item name
    public Item GetItemDetails(string itemToGet){
        for(int i=0; i<referenceItems.Length; i++){
            if(referenceItems[i].itemName == itemToGet){
                return referenceItems[i];
            }
        }
        return null;
    }

    // Sorts items to be in top left part of the inventory, starting
    // with front row
    public void ItemSorter(){

        bool itemAfterSpace = true;
        while(itemAfterSpace){
            itemAfterSpace = false;
            // moves item back once
            for(int i=0; i<theItems.Length-1;i++){
                if(theItems[i]==""){
                    theItems[i] = theItems[i+1];
                    theItems[i+1] = "";

                    numberOfItem[i] = numberOfItem[i+1];
                    numberOfItem[i+1] = 0;
                    if(theItems[i]!=""){
                        itemAfterSpace = true;
                    }
                }

            }
        }
        // looks if there is space after item
    }

    // Checks if there is a free space in the inventory
    public bool CheckIfSpace(string itemToAdd){
        int newItemPosition = 0;
        bool isSpace = false;

        for(int i=0; i<theItems.Length; i++){
            if(theItems[i] == "" || theItems[i] == itemToAdd){
                newItemPosition = i;
                i = theItems.Length;
                isSpace = true;
                return isSpace;
            }
        }

        return isSpace;
    }

    // Adds item in the inventory
    public void AddItem(string itemToAdd){
        int newItemPosition = 0;
        bool isSpace = false;

        for(int i=0; i<theItems.Length; i++){
            if(theItems[i] == "" || theItems[i] == itemToAdd){
                newItemPosition = i;
                i = theItems.Length;
                isSpace = true;
            }
        }

        if(isSpace){
            bool itemExists = false;
            for(int i=0; i<referenceItems.Length; i++){
                if(referenceItems[i].itemName == itemToAdd){
                    itemExists = true;
                    i = referenceItems.Length;
                }
            }
            if(itemExists){
                theItems[newItemPosition] = itemToAdd;
                numberOfItem[newItemPosition]++;
            }else{
                Debug.LogError(itemToAdd + " does not exist!!");
            }
        }
        GameMenu.instance.ShowItems();
    }

    // Removes item from the inventory
    public bool RemoveItem(string itemToRemove){
        bool foundItem = false;
        int itemPosition = 0;

        for(int i = 0; i < theItems.Length; i++){
            if(theItems[i] == itemToRemove){
                foundItem = true;
                itemPosition = i;
                i = theItems.Length;
            }
        }

        if(foundItem){
            numberOfItem[itemPosition]--;
            if(numberOfItem[itemPosition]<=0){
                theItems[itemPosition] = "";
            }
            GameMenu.instance.ShowItems();
            return true;
        }else{
            Debug.LogError("Couldn't find " + itemToRemove + "!");
            return false;
        }
    }

    // Saves data
    public void Save(){
        DataToSave();
        string json = JsonUtility.ToJson(data);
        WriteToFile(file, json);
    }

    // Loads data
    public void Load(){
        if(File.Exists(GetFilePath(file))){
            data = new PlayerData();
            string json = ReadFromFile(file);
            Debug.Log("Before overwrite:");
            Debug.Log(json);
            Debug.Log(data.position[0]);
            JsonUtility.FromJsonOverwrite(json, data);
            Debug.Log("After overwrite:");
            Debug.Log(json);
            Debug.Log(data.position[0]);
            DataToLoad();
        }
    }

    // Writes given json to named file
    private void WriteToFile(string filename, string json){
        string path = GetFilePath(filename);
        FileStream filestream = new FileStream(path, FileMode.Create);

        using(StreamWriter writer = new StreamWriter(filestream)){
            writer.Write(json);
        }
    }

    // reads json from named file
    private string ReadFromFile(string filename){
        string path = GetFilePath(filename);
        if(File.Exists(path)){
            using(StreamReader reader = new StreamReader(path)){
                string json = reader.ReadToEnd();
                return json;
            }
        }
        else{
            Debug.LogWarning("File not found!");
        }
        return "";
    }

    // Gets file path for saving the file
    private string GetFilePath(string filename){
        return Application.persistentDataPath + "/" + filename;
    }

    // Check if file exists
    public bool FileExists(){
        if(File.Exists(GetFilePath(file))){
            return true;
        }else{
            return false;
        }
    }

    // All the data that needs to be saved
    public void DataToSave(){
        player = PlayerController.instance;
        data.position[0] = player.transform.position.x;
        data.position[1] = player.transform.position.y;
        data.position[2] = player.transform.position.z;

        data.questNames = QuestManager.instance.questMarkerNames;
        data.ifComplete = QuestManager.instance.questMarkersComplete;

        for(int i=0; QuestManager.instance.questMarkerNames.Length > i; i++){
            bool isFound = false;
            for(int j=0; data.questNames.Length > j; j++){
                if(data.questNames[j] == QuestManager.instance.questMarkerNames[i]){
                    data.ifComplete[j] = QuestManager.instance.questMarkersComplete[i];
                    isFound = true;
                    continue;
                }
            }
            if(!isFound){
                System.Array.Resize(ref data.questNames, data.questNames.Length + 1);
                data.questNames[data.questNames.Length - 1] = QuestManager.instance.questMarkerNames[i];

                // gameManager.data.ifComplete[gameManager.data.questNames.Length - 1] = questMarkersComplete[i];

                bool[] isDone = data.ifComplete;
                data.ifComplete = new bool[isDone.Length+1];
                for(int k=0; k<isDone.Length; k++){
                    data.ifComplete[k] = isDone[k];
                }
                data.ifComplete[data.ifComplete.Length - 1] = QuestManager.instance.questMarkersComplete[i];
            }
        }

        Debug.Log(playerStats[0].gameObject.activeInHierarchy);
        if(playerStats[0].gameObject.activeInHierarchy){
                data.ifCharActive = true;
            }else{
                data.ifCharActive = false;
            }
        data.charName = playerStats[0].charName;
        data.playerLevel = playerStats[0].playerLevel;
        data.currentEXP = playerStats[0].currentEXP;
        data.currentHP = playerStats[0].currentHP;
        data.maxHP = playerStats[0].maxHP;
        data.currentMP = playerStats[0].currentMP;
        data.maxMP = playerStats[0].maxMP;
        data.strength = playerStats[0].strength;
        data.defence = playerStats[0].defence;
        data.wpnPwr = playerStats[0].wpnPwr;
        data.armrPwr = playerStats[0].armrPwr;
        data.equippedWpn = playerStats[0].equippedWpn;
        data.equippedArmr = playerStats[0].equippedArmr;

        data.theItems = new string[theItems.Length];
        for(int i=0; i<theItems.Length; i++){
            data.theItems[i] = theItems[i];
        }
        data.numberOfItem = new int[numberOfItem.Length];
        for(int i=0; i<numberOfItem.Length; i++){
            data.numberOfItem[i] = numberOfItem[i];
        }
        data.currentScene = SceneManager.GetActiveScene().name;
        data.currentMoney = currentMoney;
    }

    // All the data that needs to be loaded
    public void DataToLoad(){
        player = PlayerController.instance;
        player.transform.position = new Vector3(
            data.position[0],
            data.position[1],
            data.position[2]
        );
        
        for(int i=0; QuestManager.instance.questMarkerNames.Length > i; i++){
            for(int j=0; data.questNames.Length > j; j++){
                if(data.questNames[j] == QuestManager.instance.questMarkerNames[i]){
                    QuestManager.instance.questMarkersComplete[i] = data.ifComplete[j];
                    continue;
                }
            }
        }

        if(data.ifCharActive){
                playerStats[0].gameObject.SetActive(true);
            }else{
                playerStats[0].gameObject.SetActive(false);
            }
        playerStats[0].charName = data.charName;
        playerStats[0].playerLevel = data.playerLevel;
        playerStats[0].currentEXP = data.currentEXP;
        playerStats[0].currentHP = data.currentHP;
        playerStats[0].maxHP = data.maxHP;
        playerStats[0].currentMP = data.currentMP;
        playerStats[0].maxMP = data.maxMP;
        playerStats[0].strength = data.strength;
        playerStats[0].defence = data.defence;
        playerStats[0].wpnPwr = data.wpnPwr;
        playerStats[0].armrPwr = data.armrPwr;
        playerStats[0].equippedWpn = data.equippedWpn;
        playerStats[0].equippedArmr = data.equippedArmr;


        for(int i=0; i<theItems.Length; i++){
            theItems[i] = data.theItems[i];
        }

        for(int i=0; i<numberOfItem.Length; i++){
            numberOfItem[i] = data.numberOfItem[i];
        }

        currentMoney = data.currentMoney;

        SceneManager.LoadScene(data.currentScene);
    }
}