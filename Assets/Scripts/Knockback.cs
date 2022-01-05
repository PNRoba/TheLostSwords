using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.parent.gameObject.GetComponent<SpriteRenderer>().color;
        this.transform.parent.gameObject.GetComponent<SpriteRenderer>().color = new Color(this.transform.parent.gameObject.GetComponent<SpriteRenderer>().color.r, Mathf.MoveTowards(this.transform.parent.gameObject.GetComponent<SpriteRenderer>().color.g, 1f, 1 * Time.deltaTime), Mathf.MoveTowards(this.transform.parent.gameObject.GetComponent<SpriteRenderer>().color.b, 1f, 1 * Time.deltaTime), this.transform.parent.gameObject.GetComponent<SpriteRenderer>().color.a);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("breakable") && !this.gameObject.CompareTag("Player")){
            other.GetComponent<Pot>().Smash();
        }
        if((other.gameObject.CompareTag("Enemy") && !this.gameObject.CompareTag("Enemy")) || other.gameObject.CompareTag("Player")){
            Rigidbody2D hit = other.gameObject.GetComponent<Rigidbody2D>();
            if(hit != null){
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);
                if(other.gameObject.CompareTag("Enemy")){
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, knockTime);
                    other.GetComponent<Enemy>().health -= GameManager.instance.playerStats[0].strength;
                    // other.transform.parent.gameObject.GetComponent<Enemy>().healthbar.sizeDelta = new Vector2(other.transform.parent.gameObject.GetComponent<Enemy>().health*Mathf.CeilToInt(200/other.transform.parent.gameObject.GetComponent<Enemy>().health),other.transform.parent.gameObject.GetComponent<Enemy>().healthbar.sizeDelta.y);
                    other.GetComponent<Enemy>().healthbar.sizeDelta = new Vector2(other.GetComponent<Enemy>().health*Mathf.Ceil(200/other.GetComponent<Enemy>().maxhealth),other.GetComponent<Enemy>().healthbar.sizeDelta.y);
                    if(other.GetComponent<Enemy>().health<=0){
                        other.GetComponent<Enemy>().gameObject.SetActive(false);
                        GameManager.instance.currentMoney += Mathf.FloorToInt(other.GetComponent<Enemy>().maxhealth/2);
                        GameManager.instance.playerStats[0].AddExp(other.GetComponent<Enemy>().maxhealth);
                    }
                    other.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
                }
                if(other.gameObject.CompareTag("Player")){
                    other.GetComponent<PlayerController>().Knock(knockTime);
                    if(GameManager.instance.playerStats[0].equippedArmr != ""){
                        GameManager.instance.playerStats[0].currentHP -= Mathf.FloorToInt(this.transform.parent.gameObject.GetComponent<Enemy>().baseAttack / 4);
                    }else{
                        GameManager.instance.playerStats[0].currentHP -= this.transform.parent.gameObject.GetComponent<Enemy>().baseAttack;
                        //Debug.Log(this.transform.parent.gameObject.GetComponent<Enemy>().baseAttack);
                    }
                    if(GameManager.instance.playerStats[0].currentHP>0){
                        other.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
                    }
                }
                
                //Debug.Log(other.GetComponent<SpriteRenderer>().color.g);
            }
        }
    }
}
