using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleeAttack : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D hitbox;

    private bool isAttacking = false;


    private void Start()
    {
        animator = GetComponent<Animator>();
        hitbox = transform.Find("Hitbox").GetComponent<BoxCollider2D>();
    }

    
    private void Update()
    {
      

        /*
        if (Input.GetKeyDown(KeyCode.F)) // Attack on Space key press.
        {
            animator.SetTrigger("punch");
            Invoke("ActivateHitbox", 1f); // Activate hitbox after 0.2 seconds.
            Invoke("DeactivateHitbox", 1f); // Deactivate hitbox after 0.4 seconds.
        }
        */


    }




    /*
    void ActivateHitbox()
    {
        hitbox.gameObject.SetActive(true);
    }

    void DeactivateHitbox()
    {
        hitbox.gameObject.SetActive(false);
    }

    */
}
