using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;


public class HelperScript : MonoBehaviour
{
    LayerMask groundLayerMask;
    SpriteRenderer sr;


    public void FlipObject( bool flip )
    {
        FlipObject( gameObject, flip );
    }

    public void Start()
    {
        groundLayerMask = LayerMask.GetMask("Ground");
        sr = gameObject.GetComponent<SpriteRenderer>();
    }


    public void FlipObject(GameObject obj, bool flip )
    {

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


    public bool ExtendedRayCollisionCheck( float xoffs, float yoffs )
    {
        float rayLength = 0.5f; // length of raycast
        bool hitSomething = false;

        // convert x and y offset into a Vector3 
        Vector3 offset = new Vector3( xoffs, yoffs, 0);

        //cast a ray downward 
        RaycastHit2D hit;
        

        hit = Physics2D.Raycast(transform.position + offset, -Vector2.up, rayLength, groundLayerMask);

        Color hitColor = Color.white;


        if (hit.collider != null)
        {
            print("Player has collided with Ground layer");
            hitColor = Color.green;
            hitSomething = true;
        }
        // draw a debug ray to show ray position
        // You need to enable gizmos in the editor to see these
        Debug.DrawRay(transform.position + offset, -Vector2.up * rayLength, hitColor);

        return hitSomething;

    }





}
