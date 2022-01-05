using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyState{
    idle,
    walk,
    attack,
    stagger
}
public class Enemy : MonoBehaviour
{
    public EnemyState currentState;

    public int health;
    public int maxhealth;
    public string enemyName;
    public int baseAttack;

    public float moveSpeed;

    public RectTransform healthbar;

    void Start()
    {
        maxhealth = health;
    }

    void Update()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(this.GetComponent<SpriteRenderer>().color.r, Mathf.MoveTowards(this.GetComponent<SpriteRenderer>().color.g, 1f, 1 * Time.deltaTime), Mathf.MoveTowards(this.GetComponent<SpriteRenderer>().color.b, 1f, 1 * Time.deltaTime), this.GetComponent<SpriteRenderer>().color.a);
    }

    public void Knock(Rigidbody2D myRB, float knockTime){
        StartCoroutine(KnockCo(myRB, knockTime));
    }

    private IEnumerator KnockCo(Rigidbody2D myRB, float knockTime){
        if(myRB != null){
            yield return new WaitForSeconds(knockTime);
            myRB.velocity = Vector2.zero;
            myRB.GetComponent<Enemy>().currentState = EnemyState.idle;
        }
    }
}
