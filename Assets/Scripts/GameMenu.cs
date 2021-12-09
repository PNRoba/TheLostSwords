using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject theMenu;
    public GameObject[] windows;
    private CharacterStats[] playerStats;
    public Text[] nameText, hpText, mpText, lvlText, expText;
    public Slider[] exp;
    public Image[] charImage;
    public GameObject[] charStatHolder;

    public GameObject[] statusButtons;

    public Text statusName, statusHP, statusMP, statusStr, statusDef, statusWpn, statusWpnPow, statusArmour, statusArmourPow, statusEXP;
    public Image statusImage;

    public ItemButton[] itemButtons;
    public string selectedItem;
    public Item activeItem;
    public Text itemName, itemDesc, useButtonText;

    public static GameMenu instance;

    public GameObject charChoiceMenu;
    public Text[] charChoiceNames;

    public Text moneyText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !Shop.instance.shopMenu.activeInHierarchy){
            if(theMenu.activeInHierarchy){
                // theMenu.SetActive(false);
                // GameManager.instance.gameMenuOpen = false;
                CloseMenu();
            }else{
                theMenu.SetActive(true);
                UpdateMainStats();
                GameManager.instance.gameMenuOpen = true;
            }
        }
    }

    public void UpdateMainStats(){
        playerStats = GameManager.instance.playerStats;

        for(int i=0; i<playerStats.Length; i++){
            //Debug.Log(playerStats[i].charName);
            if(playerStats[i].gameObject.activeInHierarchy){
                charStatHolder[i].SetActive(true);

                nameText[i].text = playerStats[i].charName;
                hpText[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
                mpText[i].text = "MP: " + playerStats[i].currentMP + "/" + playerStats[i].maxMP;
                lvlText[i].text = "Lvl: " + playerStats[i].playerLevel;
                
                //expText[i].text = "" + playerStats[i].currentEXP + "/" + playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                //exp[i].maxValue = playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                //exp[i].value = playerStats[i].currentEXP;

                if(playerStats[i].playerLevel < playerStats[i].maxLevel){ 
                expText[i].text = $"{playerStats[i].currentEXP} / {playerStats[i].expToNextLevel[playerStats[i].playerLevel]}";  
                exp[i].maxValue = playerStats[i].expToNextLevel[playerStats[i].playerLevel]; 
                exp[i].value = playerStats[i].currentEXP;
                }else{
                expText[i].text = "Max Level";
                exp[i].maxValue = 1;
                exp[i].value = 1;  
                }  
                
                charImage[i].sprite = playerStats[i].charImage;

            }else{
                charStatHolder[i].SetActive(false);
            }
        }

        moneyText.text = GameManager.instance.currentMoney.ToString() + "m"; // m for money for now
    }

    public void ToggleWindow(int winNum){
        UpdateMainStats();
        for(int i = 0; i<windows.Length; i++){
            if(i==winNum){
                windows[i].SetActive(!windows[i].activeInHierarchy);
            }else{
                windows[i].SetActive(false);
            }
        }
        charChoiceMenu.SetActive(false);
    }

    public void CloseMenu(){
        for(int i = 0; i<windows.Length; i++){
            windows[i].SetActive(false);
        }
        theMenu.SetActive(false);

        GameManager.instance.gameMenuOpen = false;

        charChoiceMenu.SetActive(false);
    }

    public void OpenStatus(){
        UpdateMainStats();
        CharStats(0);
        // update info shown
        for(int i = 0; i < statusButtons.Length; i++){
            statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            statusButtons[i].GetComponentInChildren<Text>().text = playerStats[i].charName;
        }
    }

    public void CharStats(int selected){
        statusName.text = playerStats[selected].charName;
        statusHP.text = "" + playerStats[selected].currentHP + "/" + playerStats[selected].maxHP;
        statusMP.text = "" + playerStats[selected].currentMP + "/" + playerStats[selected].maxMP;
        statusStr.text = playerStats[selected].strength.ToString();
        statusDef.text = playerStats[selected].defence.ToString();
        if(playerStats[selected].equippedWpn != ""){
            statusWpn.text = playerStats[selected].equippedWpn;
        }else{
            statusWpn.text = "None";
        }
        if(playerStats[selected].equippedArmr != ""){
            statusArmour.text = playerStats[selected].equippedArmr;
        }else{
            statusArmour.text = "None";
        }
        statusEXP.text = (playerStats[selected].expToNextLevel[playerStats[selected].playerLevel]-playerStats[selected].currentEXP).ToString();
        statusImage.sprite = playerStats[selected].charImage;
    }

    public void ShowItems(){
        GameManager.instance.ItemSorter();
        for(int i = 0; i<itemButtons.Length; i++){
            itemButtons[i].buttonValue = i;

            if(GameManager.instance.theItems[i] != ""){
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                itemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.theItems[i]).itemSprite;
                itemButtons[i].amountText.text = GameManager.instance.numberOfItem[i].ToString();
            }else{
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        }
    }

    public void SelectItem(Item newItem){
        activeItem = newItem;
        if(activeItem.isItem){
            useButtonText.text = "Use";
        }
        if(activeItem.isWpn || activeItem.isArmour){
            useButtonText.text = "Equip";
        }

        itemName.text = activeItem.name;
        itemDesc.text = activeItem.desc;
    }

    public void DiscardItem(){
        if(activeItem != null){
            GameManager.instance.RemoveItem(activeItem.name);
        }
    }

    // choose character to use item on (for testing purposes currently) after need to set default
    public void OpenChooseChar(){
        charChoiceMenu.SetActive(true);

        for(int i = 0; i < charChoiceNames.Length; i++){
            charChoiceNames[i].text = GameManager.instance.playerStats[i].charName;
            charChoiceNames[i].transform.parent.gameObject.SetActive(GameManager.instance.playerStats[i].gameObject.activeInHierarchy);
        }
    }
    public void CloseChooseChar(){
        charChoiceMenu.SetActive(false);
    }

    // Use/equip button select which character to use on
    public void UseItem(int selectChar){
        activeItem.Use(selectChar);
        CloseChooseChar();
    }
}
