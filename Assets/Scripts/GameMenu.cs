using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public GameObject theMenu;
    private CharacterStats[] playerStats;

    // --- Assigned objects in Unity Inspector ---
    public GameObject[] windows; // Items, Stats and Controls
    public Text[] nameText, hpText, mpText, lvlText, expText;
    public Slider[] exp;
    public Image[] charImage;
    public GameObject[] charStatHolder;

    public GameObject[] statusButtons;

    public Text statusMainName, statusName, statusHP, statusMP, statusStr, statusDef, statusWpn, statusWpnPow, statusArmour, statusArmourPow, statusEXP;
    public Image statusImage;

    public ItemButton[] itemButtons;
    // ------------------------------------------
    public string selectedItem; // Selected item in the inventory name
    public Item activeItem; // Selected item in the inventory
    public Text itemName, itemDesc, useButtonText; // Item info

    public static GameMenu instance;

    public GameObject charChoiceMenu; // Choose character to use item on
    public Text[] charChoiceNames; // Character names

    public Text moneyText;

    public GameManager gameManager;

    public string mainMenuName;

    public GameObject gameOverWindow;

    public Slider HPSlider; // Players HP Slider

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // Opens (And closes) in game menu
        if(Input.GetKeyDown(KeyCode.Escape) && !(Shop.instance.shopMenu.activeInHierarchy || DialogManager.instance.dialogBox.activeInHierarchy)){
            if(theMenu.activeInHierarchy){
                // theMenu.SetActive(false);
                // GameManager.instance.gameMenuOpen = false;
                CloseMenu();
            }else{
                theMenu.SetActive(true);
                UpdateMainStats();
                GameManager.instance.gameMenuOpen = true;
            }
            AudioManager.instance.PlaySFX(5);
        }

        // Checks if HP is less than or equal zero to open "game over" window
        if(GameManager.instance.playerStats[0].currentHP <=0){
            PlayerController.instance.canMove = false;
            gameOverWindow.SetActive(true);
        }
        HPSlider.maxValue = GameManager.instance.playerStats[0].maxHP;
        HPSlider.value = GameManager.instance.playerStats[0].currentHP;
    }

    // Updates Player stats
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

    // Used to close any open windows before opening another one in in-game menu
    // Items, Stats, Controls
    public void ToggleWindow(int winNum){
        // Debug.Log(winNum);
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

    // Close Menu button
    public void CloseMenu(){
        for(int i = 0; i<windows.Length; i++){
            windows[i].SetActive(false);
        }
        theMenu.SetActive(false);

        GameManager.instance.gameMenuOpen = false;

        charChoiceMenu.SetActive(false);
    }

    // Open Status button
    public void OpenStatus(){
        UpdateMainStats();
        CharStats(0);
        // update info shown
        for(int i = 0; i < statusButtons.Length; i++){
            statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            statusButtons[i].GetComponentInChildren<Text>().text = playerStats[i].charName;
        }
    }

    // Simple character stats, when first opening in-game menu
    public void CharStats(int selected){
        statusMainName.text = playerStats[selected].charName;
        statusName.text = playerStats[selected].charName;
        statusHP.text = "" + playerStats[selected].currentHP + "/" + playerStats[selected].maxHP;
        statusMP.text = "" + playerStats[selected].currentMP + "/" + playerStats[selected].maxMP;
        statusStr.text = playerStats[selected].strength.ToString();
        statusDef.text = playerStats[selected].defence.ToString();
        if(playerStats[selected].equippedWpn != ""){
            statusWpn.text = playerStats[selected].equippedWpn;
            statusWpnPow.text = playerStats[selected].wpnPwr.ToString();
        }else{
            statusWpn.text = "None";
            statusWpnPow.text = "0";
        }
        if(playerStats[selected].equippedArmr != ""){
            statusArmour.text = playerStats[selected].equippedArmr;
            statusArmourPow.text = playerStats[selected].armrPwr.ToString();
        }else{
            statusArmour.text = "None";
            statusArmourPow.text = "0";
        }
        statusEXP.text = (playerStats[selected].expToNextLevel[playerStats[selected].playerLevel]-playerStats[selected].currentEXP).ToString();
        statusImage.sprite = playerStats[selected].charImage;
    }

    // Update the items in players inventory
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

    // When selected an Item in inventory
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

    // Remove Item from inventory
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

    // Activates Save function with Save button
    public void SaveButton(){
        gameManager.Save();
    }

    // Play button sound in inventory
    public void PlayButtonSound(){
        AudioManager.instance.PlaySFX(4);
    }

    // Quit game button
    public void QuitGame(){
        SceneManager.LoadScene(mainMenuName);
        Destroy(GameManager.instance.gameObject);
        Destroy(PlayerController.instance.gameObject);
        Destroy(AudioManager.instance.gameObject);
        Destroy(gameObject);
    }
}
