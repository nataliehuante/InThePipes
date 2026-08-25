using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Animator animator;
    public GameObject impactEffect;
    private LevelsSounds sounds;

    void Start()
    {
        // animator = GetComponent<Animator>();
        // // Trigger the growing animation at start. Assuming "Grow" is the name of your animation state.
        // animator.Play("BulletGrow");
        animator = GetComponent<Animator>();
        sounds = FindObjectOfType<LevelsSounds>();
        animator.SetBool(GlobalVariables.currentWebColor, true);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Player")
        {
            return;
        }
        // if (col.collider.tag == "Bullet") 
        //     // Physics2D.IgnoreCollision(col.collider, gameObject.GetComponent<Collider2D>());
        //     return;
        Instantiate(impactEffect, gameObject.transform.position, Quaternion.identity);
        sounds.PlayThudSound();
        Destroy(gameObject);
    }
}
