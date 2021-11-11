using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove){
            // Player Movement
            theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal")*playerSpeed, Input.GetAxisRaw("Vertical")*playerSpeed);
        }else{
            theRB.velocity = Vector2.zero;
        }
        
        // Player Animation
        myAnim.SetFloat("moveX", theRB.velocity.x);
        myAnim.SetFloat("moveY", theRB.velocity.y);

        if(Input.GetAxisRaw("Horizontal")==1 || Input.GetAxisRaw("Horizontal")==-1 || Input.GetAxisRaw("Vertical")==1 || Input.GetAxisRaw("Vertical")==-1){
            if(canMove){
                myAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                myAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }
        }

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
            Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y),
            transform.position.z
        );
    }

    public void SetBoundaries(Vector3 bottomLeft, Vector3 topRight){
        bottomLeftLimit = bottomLeft + new Vector3(0.6f,0.9f,0f);
        topRightLimit = topRight+ new Vector3(-0.6f,-0.9f,0f);
    }
}
