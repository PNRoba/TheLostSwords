using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : Enemy
{
    private Rigidbody2D myRB;
    public Transform target;
    public float chaseRadius; // If target enters, Enemy follows target
    public float attackRadius; // If target enters, Enemy stops following, because its close enough to hurt the target
    // public Transform homePosition;

    public bool canMove = true;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        maxhealth = health;
        currentState = EnemyState.idle;
        myRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = FindObjectOfType<CameraController>().target;
    }
    // Smoother than update (for RB)
    void FixedUpdate()
    {
        CheckDistance();
    }

    // Checks distance between Enemy and target
    // if its inside chase radius and not inside attack radius, then Enemy
    // is awake and moving towards the player
    void CheckDistance(){
        if(Vector3.Distance(target.position, transform.position)<= chaseRadius && Vector3.Distance(target.position, transform.position)> attackRadius){
            if(currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger){
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                
                ChangeAnim(temp - transform.position);
                myRB.MovePosition(temp);
                ChangeState(EnemyState.walk);
                anim.SetBool("wakeUp", true);
            }
        }else{
            anim.SetBool("wakeUp", false);
        }
    }

    // Changes animation according to direction
    private void ChangeAnim(Vector2 direction){
        direction = direction.normalized;
        anim.SetFloat("moveX", direction.x);
        anim.SetFloat("moveY", direction.y);
    }

    // Change Enemy state
    private void ChangeState(EnemyState newState){
        if(currentState !=newState){
            currentState = newState;
        }
    }
}
