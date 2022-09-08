using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    [SerializeField] private float damage;

    private Health health;
    private new Rigidbody2D rigidbody;
    private BoxCollider2D boxcollider;
    
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
        }
    }

    private IEnumerator ReenableAttack()
    {
        bool loop = true;
        List<Collider2D> results = new List<Collider2D>();
        ContactFilter2D cf = new ContactFilter2D().NoFilter();
        // Check that an attack hitbox NO LONGER overlaps this enemy
        while (loop)
        {
            Debug.Log("Loop");
            loop = false;
            boxcollider.OverlapCollider(cf, results);
            foreach (Collider2D n in results)
            {
                Debug.Log(n);
                if (n.gameObject.layer == 6)
                {
                    loop = true;
                }
            }
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("out of loop");
        // Re-enable attack hits
        Debug.Log("Re-enabling hits");
        gameObject.layer = 8; // Back to the Enemy layer
    }
}
