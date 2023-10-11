using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static Globals;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    bool isGrounded;
    bool isJumping;
    public GameObject bulletPrefab;
    HelperScript helper;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        helper = gameObject.AddComponent<HelperScript>();

        isJumping = false;

    }

    // Update is called once per frame
    void Update()
    {
        DoJump();
        DoRun();
        DoLand();
        DoShoot();
        DoAttack();

        helper.DoRayCollisionCheck();
        
    }

    void DoJump()
    {

        // check for jump
        if (Input.GetKey(KeyCode.LeftAlt) && (isGrounded==true) )
        {
           anim.SetBool("jump",true);
           isJumping = true;

           // give the player a positive velocity in the y axis, and preserve the x velocity
           rb.velocity = new Vector3(rb.velocity.x, 8.5f, 0);
        }

        

    }

    

    void DoRun()
    {
        Vector2 velocity = rb.velocity;

        // stop player sliding when not pressing left or right
        velocity.x = 0;

        // check for moving left
        if ( Input.GetKey("left"))
        {
            velocity.x = -3;
        }

        // check for moving right
        
        if (Input.GetKey("right"))
        {
            velocity.x = 3;
        }

        if( velocity.x != 0 && (isJumping==false))
        {
            anim.SetBool("run", true );
        }
        else
        {
            anim.SetBool("run", false );
        }

        // make player face left or right depending on whether his velocity is positive or negative
        if( velocity.x < -0.5f )
        {
            helper.FlipObject(true);
        }
        if( velocity.x > 0.5f )
        {
            helper.FlipObject(false);
        }


        rb.velocity = velocity;
    }


    void DoLand()
    {
        // check for player landing

        if( isJumping && isGrounded && (rb.velocity.y <=0))
        {
            print("landed!");
            // player was jumping and has now hit the ground
            isJumping = false;
            anim.SetBool("jump",false);
        }
    }

    void DoShoot()
    {
        float x,y;
        float xvel;
        if ( Input.GetKeyDown("s"))        
        {
            x = transform.position.x;
            y = transform.position.y;



            // if player is facing left, move the bullet left
            if( helper.IsFacingLeft() == true )
            {
                xvel = -5;
                

            }
            else
            {
                xvel = 5;
                

            }

            helper.MakeBullet( bulletPrefab, x,y, xvel,0 );
        }

    }

    void DoAttack()
    {
        if( Input.GetKeyDown("a"))
        {
            anim.SetTrigger("attack");
        }

        /*
        float animPosition = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;

        AnimatorClipInfo[] animInfo = anim.GetCurrentAnimatorClipInfo(0);

        print("anim name=" + animInfo[0].clip.name);

        if( animPosition > 0.5f )
        {
            //print("Attack mode");
        }
        */
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
        //print("isgrounded");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        print("hit " + collision.gameObject.tag);
    }



}
