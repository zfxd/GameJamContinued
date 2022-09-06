using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    [SerializeField] private float damage;

    private Health health;
    
    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("TRIGGERENTER MUSHROOM");
        // Damage upon touching player
        GameObject other = collision.otherCollider.gameObject;
        Debug.Log(collision.otherCollider);
        if (other.tag == "Player")
        {
            Debug.Log("Touched player!");
            other.GetComponent<Health>().TakeDamage(damage);
        }

        // Take damage if hit by correctly colored attack
        if (other.tag == gameObject.tag)
        {
            Debug.Log("Hit by attack" + other.tag);
            // Player Damage 1
            health.TakeDamage(1);
        }
    }
}
