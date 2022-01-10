using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float playerSpeed;

    public Animator myAnim;

    public static PlayerController instance;

    public string exitName; // area transition name

    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    public bool canMove = true;

    public bool ifSouthNorthEnterance; // 1 - up, down enterances/exits; 0 left, right enterances/exits
    public bool ifNormalExit;
    public GameManager gameManager;

    public DialogActivator dialogActivator;

    public bool canAttack;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null){
            instance = this;
        } else{
            if(instance!=this){
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
        // canAttack=true; // remove in done game
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(this.GetComponent<SpriteRenderer>().color.r, Mathf.MoveTowards(this.GetComponent<SpriteRenderer>().color.g, 1f, 1 * Time.deltaTime), Mathf.MoveTowards(this.GetComponent<SpriteRenderer>().color.b, 1f, 1 * Time.deltaTime), this.GetComponent<SpriteRenderer>().color.a);

        if(canMove){
            // Player Movement
            theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal")*playerSpeed, Input.GetAxisRaw("Vertical")*playerSpeed);
        }else{
            theRB.velocity = Vector2.zero;
        }
        
        // Player Animation
        myAnim.SetFloat("moveX", theRB.velocity.x);
        myAnim.SetFloat("moveY", theRB.velocity.y);
        if(Input.GetButtonDown("attack") && canAttack){
            if(canMove){
                // Attack animation
                StartCoroutine(AttackCo());
            }
        }
        else if(Input.GetAxisRaw("Horizontal")==1 || Input.GetAxisRaw("Horizontal")==-1 || Input.GetAxisRaw("Vertical")==1 || Input.GetAxisRaw("Vertical")==-1){
            if(canMove){
                myAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                myAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }
        }
        // Moves Player
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
            Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y),
            transform.position.z
        );
        
    }

    // the map boundaries. Player cant go outside the map
    public void SetBoundaries(Vector3 bottomLeft, Vector3 topRight){
        bottomLeftLimit = bottomLeft + new Vector3(0.6f,0.9f,0f);
        topRightLimit = topRight+ new Vector3(-0.6f,-0.9f,0f);
    }

    public IEnumerator AttackCo(){
        myAnim.SetBool("attacking", true);
        yield return null;
        myAnim.SetBool("attacking", false);
        yield return new WaitForSeconds(.21f);
    }

    public void Knock(float knockTime){
        StartCoroutine(KnockCo(knockTime));
    }

    private IEnumerator KnockCo(float knockTime){
        if(theRB != null){
            yield return new WaitForSeconds(knockTime);
            theRB.velocity = Vector2.zero;
        }
    }
}