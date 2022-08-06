using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player movement from https://www.youtube.com/watch?v=TcranVQUQ5U&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=1
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpHeight = 10;
    [SerializeField] private float playerScale = 3;
    [SerializeField]private LayerMask groundLayer;
    
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Flip player direction for left/right
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(playerScale, playerScale, 1);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-playerScale, playerScale, 1);
        }

        if(Input.GetKey(KeyCode.UpArrow) && isGrounded())
            Jump();

        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());
        anim.SetBool("falling", body.velocity.y < -0.01f);
    }

    // Use the Brackeys video for jumping!
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpHeight);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    // Called by Animation Events to re-enable movement after lockout (casting spells/damage)
    private void Reenable()
    {
        this.enabled = true;
    }
}
