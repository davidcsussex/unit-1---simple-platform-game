using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static Globals;



public class EnemyPatrol : MonoBehaviour
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
    int dir;

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

        dir = 1;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyWalk();
    }

    void EnemyWalk()
    {
        rb.velocity = new Vector2(dir * 2, 0);
        anim.SetBool("walk", true);

        if (helper.ExtendedRayCollisionCheck(-0.5f, 0) == false && dir < 0)
        {
            dir = -dir;
        }
        else
        {
            if (helper.ExtendedRayCollisionCheck(0.5f, 0) == false && dir > 0)
            {
                dir = -dir;
            }
        }

        helper.FlipObject(dir < 0);
    }

}
