using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinScript : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer sr;
    bool isGrounded, isJumping;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        isGrounded = true;
        isJumping = false;

    }

    // Update is called once per frame
    void Update()
    {
        DoMove();
        DoJump();
        DoLand();
    }

    void DoJump()
    {

        // check for jump
        if (Input.GetKeyDown(KeyCode.LeftAlt) && (isGrounded == true))
        {
            anim.SetBool("jump", true);
            isJumping = true;

            // give the player a positive velocity in the y axis, and preserve the x velocity
            rb.velocity = new Vector3(rb.velocity.x, 8.5f, 0);
        }
    }

    void DoMove()
    {
        anim.SetBool("walk", false);
        if ( Input.GetKey("left"))
        {
            rb.velocity = new Vector2(-2, rb.velocity.y);
            anim.SetBool("walk", true);
        }

        if (Input.GetKey("right"))
        {
            rb.velocity = new Vector2(2, rb.velocity.y);
            anim.SetBool("walk", true);
        }

        if( rb.velocity.x < 0 )
        {
            sr.flipX = true;
        }

        if (rb.velocity.x > 0)
        {
            sr.flipX = false;
        }

    }

    void DoLand()
    {
        // check for player landing

        if (isJumping && isGrounded && (rb.velocity.y <= 0))
        {
            print("landed!");
            // player was jumping and has now hit the ground
            isJumping = false;
            anim.SetBool("jump", false);
        }
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
