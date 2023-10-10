using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision != null && collision.gameObject.name == "Player")
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionTrigger2D(Collider2D collider)
    {
        if (collider != null && collider.gameObject.name == "Player")
        {
            Destroy(gameObject);
        }
    }

}
