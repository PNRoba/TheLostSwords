using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CharacterStats[] playerStats;
    public bool gameMenuOpen, dialogActive, fadingBetweenAreas, shopOpen;

    public string[] theItems;
    public int[] numberOfItem;
    public Item[] referenceItems; // all existing items

    public int currentMoney;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);

        ItemSorter();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameMenuOpen || dialogActive || fadingBetweenAreas || shopOpen){
            PlayerController.instance.canMove = false;
        }else{
            PlayerController.instance.canMove = true;
        }

        if(Input.GetKeyDown(KeyCode.J)){ // testing
            AddItem("Iron Armour");
            AddItem("blabla");

            RemoveItem("Health Potion");
            RemoveItem("Beep Boop");
        }
    }

    public Item GetItemDetails(string itemToGet){
        for(int i=0; i<referenceItems.Length; i++){
            if(referenceItems[i].itemName == itemToGet){
                return referenceItems[i];
            }
        }
        return null;
    }

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

    public void RemoveItem(string itemToRemove){
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
        }else{
            Debug.LogError("Couldn't find " + itemToRemove + "!");
        }
    }
}
