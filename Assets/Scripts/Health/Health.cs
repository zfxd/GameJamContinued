using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startHealth;
    public float currHealth {get; private set;}
    private Animator anim;
    private PlayerMovement move;

    private void Awake()
    {
        currHealth = startHealth;
        anim = GetComponent<Animator>();
        move = GetComponent<PlayerMovement>();
    }

    public void TakeDamage(float _damage)
    {
        currHealth = Mathf.Clamp(currHealth - _damage, 0, startHealth);
        if (currHealth > 0)
        {
            anim.SetTrigger("hurt");
            // iframes 
        }
        else
        {
            anim.SetTrigger("die");
            // Lose
            
            anim.SetBool("grounded", true);
            move.enabled = false;
        }
    }

    // temp "hurt yourself" button
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TakeDamage(1);
    }
}
