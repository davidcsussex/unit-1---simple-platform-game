using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;


public class HelperScript : MonoBehaviour
{
    LayerMask groundLayerMask;

    public void FlipObject( bool flip )
    {
        FlipObject( gameObject, flip );
    }

    public void Start()
    {
        groundLayerMask = LayerMask.GetMask("Ground");
    }


    public void FlipObject(GameObject obj, bool flip )
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();

        sr.flipX = flip;
    }


    public bool IsFacingLeft()
    {
        return GetComponent<SpriteRenderer>().flipX;
    }


    public void MakeBullet( GameObject prefab,  float xpos, float ypos, float xvel, float yvel )
    {
        // instantiate the object at xpos,ypos
        GameObject instance = Instantiate(prefab, new Vector3(xpos,ypos,0), Quaternion.identity);
        
        // set the velocity of the instantiated object
        Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3( xvel, yvel, 0 );

        // set the direction of the instance based on the x velocity
        FlipObject( instance, xvel<0);
    }

    public void DoRayCollisionCheck()
    {
        float rayLength = 0.5f; // length of raycast


        //cast a ray downward 
        RaycastHit2D hit;
        Vector3 offset = new Vector3(0, -0.5f, 0);

        hit = Physics2D.Raycast(transform.position + offset, -Vector2.up, rayLength, groundLayerMask);

        Color hitColor = Color.white;


        if (hit.collider != null)
        {
            print("Player has collided with Ground layer");
            hitColor = Color.green;
        }
        // draw a debug ray to show ray position
        // You need to enable gizmos in the editor to see these
        Debug.DrawRay(transform.position + offset, -Vector2.up * rayLength, hitColor);

    }






}
