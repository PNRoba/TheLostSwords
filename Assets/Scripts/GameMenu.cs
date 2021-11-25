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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
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
    }

    public void ToggleWindow(int winNum){
        for(int i = 0; i<windows.Length; i++){
            if(i==winNum){
                windows[i].SetActive(!windows[i].activeInHierarchy);
            }else{
                windows[i].SetActive(false);
            }
        }
    }

    public void CloseMenu(){
        for(int i = 0; i<windows.Length; i++){
            windows[i].SetActive(false);
        }
        theMenu.SetActive(false);

        GameManager.instance.gameMenuOpen = false;
    }
}
