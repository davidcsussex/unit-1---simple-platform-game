using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static Globals;


enum State
{
    Idle,
    Walk,
    Wait,
    Attack
}


public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float enemySpeed;
    private Animator anim;
    public GameObject spear;
    HelperScript helper;
    float delay;
    Rigidbody2D rb;
    bool isAttacking;
    bool playerInRange;
    float waitDelay;
    float dist;

    State state;
    State stateAfterWait;   // state to enter when wait has reached 0



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        helper = gameObject.AddComponent<HelperScript>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        delay = 0;
        anim.SetBool("walk", false);

        isAttacking = false;
        playerInRange = false;

        state = State.Idle;

    }

    // Update is called once per frame
    void Update()
    {
        dist = player.transform.position.x - transform.position.x;

        EnemyFacePlayer();

        if (state == State.Idle)
        {
            EnemyIdle();
        }

        if (state == State.Walk)
        {
            EnemyWalk();
        }
        if (state == State.Attack)
        {
            EnemyAttack();
        }

        if (state == State.Wait)
        {
            EnemyWait();
        }

        print("state=" + state + "  dist=" + dist);


    }

    void EnemyFacePlayer()
    {
        if (player.transform.position.x < transform.position.x)
        {
            helper.FlipObject(gameObject, true);
        }
        else
        {
            helper.FlipObject(gameObject, false);
        }
    }


    void EnemyIdle()
    {
        //check for player in range
        
        if (dist > -1.9f && dist <1.9f )
        {
            //player is in range of enemy
        }
        else
        {
            state = State.Walk;

        }
    }

    void EnemyWait()
    {
        waitDelay-=Time.deltaTime;
        if( waitDelay < 0)
        {
            state = stateAfterWait;
        }
    }
    void EnemyWalk()
    {
        if( dist < 0  )
        {
            rb.velocity = new Vector2(-2, 0);
        }
        else
        {
            rb.velocity = new Vector2(2, 0);
        }
        anim.SetBool("walk", true);

        if( playerInRange )
        {
            state = State.Attack;
        }
        
    }

    void EnemyAttack()
    {
        anim.SetBool("attack", true);
        anim.SetBool("walk", false);
        rb.velocity=new Vector2(0, 0);
    }

    public void EnemyAttackFinished()
    {
        anim.SetBool("attack", false);
        state = State.Wait;
        waitDelay = 2;
        stateAfterWait = State.Walk;


    }





    void OnCollisionEnter2D(Collision2D col)
    {
        print("tag=" + col.gameObject.tag);

        if (col.gameObject.tag == "Bullet")
        {
            print("I've been hit by a bullet!");

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerInRange = true;
        }



        print("col=" + col.isTrigger);
        if (col.tag == "Bullet")
        {
            print("Trigger - I've been hit by a bullet!");
            Destroy(col.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerInRange = false;
        }

    }
}
