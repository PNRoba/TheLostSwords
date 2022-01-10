using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Smashing animation + rewards
    public void Smash(){
        anim.SetBool("smash", true);
        StartCoroutine(breakCo());
        System.Random rnd = new System.Random();
        int num = rnd.Next(6);
        if(num>0 && num<=2){
            GameManager.instance.playerStats[0].currentEXP +=5;
        }else if(num>2 && num<=4){
            GameManager.instance.currentMoney += 1;
        }else if(num==5){
            GameManager.instance.playerStats[0].currentHP +=5;
        }
    }

    public IEnumerator breakCo(){
        yield return new WaitForSeconds(0.3f);
        this.gameObject.SetActive(false);
        anim.SetBool("smash", false);
    }
}
