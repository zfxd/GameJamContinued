using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startHealth;
    public float currHealth {get; private set;}
    private Animator anim;
    private PlayerMovement move;
    private Rigidbody2D body;
    

    [SerializeField] private float knockbackForce;
    [SerializeField] private float knockbackHeight;

    private void Awake()
    {
        currHealth = startHealth;
        anim = GetComponent<Animator>();
        move = GetComponent<PlayerMovement>();
        body = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float _damage)
    {
        currHealth = Mathf.Clamp(currHealth - _damage, 0, startHealth);
        if (currHealth > 0)
        {
            anim.SetTrigger("hurt");
            // knockback
            anim.SetBool("grounded", true);
            move.enabled = false;
            body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * knockbackForce, knockbackHeight);
            // iframes
        }
        else
        {
            anim.SetTrigger("die");
            // Lose
            
            move.enabled = false;
            anim.SetBool("grounded", true);
        }
    }

    // temp "hurt yourself" button
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TakeDamage(1);
    }
}
