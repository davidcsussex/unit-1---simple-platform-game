using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PenguinScript : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer sr;
    bool isGrounded, isJumping;
    HelperScript hs;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        hs = gameObject.AddComponent<HelperScript>();

        isGrounded = true;
        isJumping = false;

    }

    // Update is called once per frame
    void Update()
    {
        DoMove();
        DoJump();
        
        DoFall();

        DoLand();
    }

    void DoJump()
    {

        // check for jump
        if (Input.GetKeyDown(KeyCode.LeftAlt) && (isGrounded == true))
        {
            isJumping = true;

            // give the player a positive velocity in the y axis, and preserve the x velocity
            rb.velocity = new Vector3(rb.velocity.x, 8.5f, 0);
        }

        if (isJumping == true)
        {
            if (rb.velocity.y < -1)
            {
                anim.SetBool("fall", true);
            }
            if (rb.velocity.y > 0)
            {
                anim.SetBool("jump", true);
            }

        }

    }

    void DoMove()
    {
        anim.SetBool("walk", false);
        if ( Input.GetKey("left"))
        {
            rb.velocity = new Vector2(-2, rb.velocity.y);
        }

        if (Input.GetKey("right"))
        {
            rb.velocity = new Vector2(2, rb.velocity.y);
            
        }

        if( rb.velocity.x < -0.1f )
        {
            hs.FlipObject(true);
        }

        if (rb.velocity.x > 0.1f)
        {
            hs.FlipObject(false);

        }

        if( rb.velocity.x != 0 && isJumping == false && rb.velocity.y >= 0)
        {
            anim.SetBool("walk", true);

        }

    }

    void DoLand()
    {
        // check for player landing

        if ( isGrounded && (rb.velocity.y <= 0))
        {
            print("landed!");
            // player was jumping and has now hit the ground
            isJumping = false;
            anim.SetBool("jump", false);
            anim.SetBool("fall", false);
        }
    }

    void DoFall()
    {
        if( isJumping == false && isGrounded == false && rb.velocity.y <= 0)
        {
            anim.SetBool("fall", true);
        }
    }

    void AttackEnded()
    {
        anim.SetBool("attack", false);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }


}
