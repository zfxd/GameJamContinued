using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBlock : MonoBehaviour
{
    [SerializeField] private float speed;
    private Animator anim;
    private Rigidbody2D rb;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Set velocity
    void Drop()
    {
        anim.SetBool("hit", false);
        rb.velocity = new Vector3(0, -1 * speed, 0);
        // Start falling
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("BLUE COLLIDE");
        // Stop drop (velocity = 0?)
        if (other.gameObject.layer != 8){
            anim.SetBool("hit", true);
            rb.velocity = new Vector3(0,0,0); // Don't stop on enemies!
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
