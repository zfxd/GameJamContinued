using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player movement from https://www.youtube.com/watch?v=TcranVQUQ5U&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=1
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpHeight = 10;
    [SerializeField] private float playerScale = 3;

    private Rigidbody2D body;
    private Animator anim;
    [SerializeField] private bool grounded = true;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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

        if(Input.GetKey(KeyCode.UpArrow) && grounded)
            Jump();

        anim.SetBool("run", horizontalInput != 0);
    }

    // Use the Brackeys video for jumping!
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpHeight);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = true;
    }
}
