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

    public bool invuln {get; private set;}

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
            // check that the PLAYER died!
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
        invuln = true;
        // Made invulnerable on collision

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

        if (gameObject.layer == 9)
        {
            // For enemy
            gameObject.layer = 8;
        }
        else
        {
            // For player
            Physics2D.IgnoreLayerCollision(7,8,false);
        }
        invuln = false;
    }

    // Ignore collision w/ player, wait a bit, then despawn
    private IEnumerator EnemyDeath()
    {
        Physics2D.IgnoreLayerCollision(7,8,true);
        Physics2D.IgnoreLayerCollision(6,8,true); // Enemy and attack 
        // TODO wait a while then despawn.
        yield return new WaitForSeconds(2.0f);
        Debug.Log("TODO DESPAWN HERE"); 
    }
}
