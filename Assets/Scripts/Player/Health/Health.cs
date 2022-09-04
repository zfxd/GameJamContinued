using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startHealth;
    public float currHealth {get; private set;}
    private Animator anim;
    private Rigidbody2D body;
    private PlayerMovement move;

    [SerializeField] private float iFrameDuration;
    [SerializeField] private float numFlashes;
    private SpriteRenderer sprite;
    

    [SerializeField] private float knockbackForce;
    [SerializeField] private float knockbackHeight;

    private void Awake()
    {
        currHealth = startHealth;
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        move = GetComponent<PlayerMovement>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        currHealth = Mathf.Clamp(currHealth - _damage, 0, startHealth);
        if (currHealth > 0)
        {
            anim.SetTrigger("hurt");
            // LoseControl and Knockback handled by Animation Events
            // iframes
            StartCoroutine(Invulnerability());
        }
        else
        {
            anim.SetTrigger("die");
            // Lose condition TODO
        }
    }

    private void Knockback()
    {
        body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * knockbackForce, knockbackHeight);
    }

    // temp "hurt yourself" button
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TakeDamage(1);
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(7,8,true);
        for(int i = 0; i < numFlashes; i++)
        {
            Color temp = sprite.color;
            temp.a = 0.5f;
            sprite.color = temp;
            yield return new WaitForSeconds(iFrameDuration / (numFlashes * 2));
            temp.a = 1.0f;
            sprite.color = temp;
            yield return new WaitForSeconds(iFrameDuration / (numFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(7,8,false);
    }
}
