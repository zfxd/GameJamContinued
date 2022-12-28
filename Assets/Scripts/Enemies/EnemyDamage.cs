using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    [SerializeField] private float damage;

    private Health health;
    private new Rigidbody2D rigidbody;
    public BoxCollider2D boxcollider;
    
    private void Awake()
    {
        health = GetComponent<Health>();
        rigidbody = GetComponent<Rigidbody2D>();
        boxcollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("COLLISIONENTER MUSHROOM");
        rigidbody.velocity = new Vector2(0f,0f);
        // Damage upon touching player
        Collider2D other = collision.collider;
        Debug.Log("Touched " + other);
        Debug.Log("TAG " + other.tag);
        if (other.tag == "Player")
        {
            Debug.Log("Touched player!");
            Physics2D.IgnoreLayerCollision(7,8,true);
            // Physics2D.IgnoreLayerCollision(7,9,true); // Do I need this? Or will it automatically go back to layer 8
            other.GetComponent<Health>().TakeDamage(damage);
        }

        /* MOVE THIS CODE TO ONTRIGGER ENTER
        // Take damage if hit by correctly colored attack
        if (other.tag == gameObject.tag)
        {
            Debug.Log("Hit by attack" + other.tag);
            // Player Damage 1
            health.TakeDamage(1);
            return; // Return early because we want the same ability cast to hit multiple times
                    // IF AND ONLY IF it's the correct color
        }
        
        // Disable further attack collisions until the attack no longer overlaps
        // This allows harmless attacks to pass through the enemy
        if (other.gameObject.layer == 6)
        {
            Debug.Log("Disabling attack collision");
            gameObject.layer = 9; // Layer9 is IgnoreAttacks layer
            StartCoroutine(ReenableAttack());
        } */
    }

// Attacks are triggers..
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TRIGGERENTER MUSHROOM");
        // Take damage if hit by correctly colored attack
        if (other.tag == gameObject.tag && !health.invuln)
        {
            Debug.Log("Hit by attack" + other.tag);
            // Player Damage 1
            gameObject.layer = 9;
            Debug.Log("Disabling attack collision");
            health.TakeDamage(1);
            return; // Return early because we want the same ability cast to hit multiple times
                    // IF AND ONLY IF it's the correct color
        }
        
        /*/ DO I EVEN NEED THIS NOW THAT THEY'RE TRIGGERS??
        // Disable further attack collisions until the attack no longer overlaps
        // This allows harmless attacks to pass through the enemy
        if (other.gameObject.layer == 6)
        {
            Debug.Log("Disabling attack collision");
            gameObject.layer = 9; // Layer9 is IgnoreAttacks layer
            // StartCoroutine(ReenableAttack());
        }       */ 
    }

    
    // Ignore collision w/ player, wait a bit, then despawn 

    private IEnumerator EnemyDeath()
    {
        // THERE IS SOMETHING WRONG WITH THIS FUNCTION!
        this.gameObject.layer = 10;
        Debug.Log("Moving " + gameObject + " to layer 10");
        Physics2D.IgnoreLayerCollision(7,8,false);
        Physics2D.IgnoreLayerCollision(6,8,false);
        // Forcing layer collisions back on
        // WHY ARE LAYER COLLISIONS TURNING OFF FOR NO REASON?
        // TODO find out WHY
        // TODO wait a while then despawn.
        yield return new WaitForSeconds(2.0f);
        Debug.Log("TODO DESPAWN");
    }

    /*/ Allow attacks to hit again if no damage was taken
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!health.invuln)
        {
            Debug.Log("Re-enabling attack collision");  
            gameObject.layer = 8;
        }
    }*/
}
