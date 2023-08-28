using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenOrb : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float _lifetime;
    private Animator anim;
    private Rigidbody2D rb;
    private float lifetime;
    float direction = 1.0f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        lifetime += Time.deltaTime;
        if (lifetime > _lifetime)
            Deactivate();
    }

    public void SetDirection(float _direction)
    {
        if (Mathf.Sign(direction) != _direction)
            direction = -direction;

        gameObject.SetActive(true);
    }

    // Set velocity
    public void OnEnable()
    {
        // anim.SetBool("hit", false);
        rb.velocity = new Vector3(direction * speed, 0, 0);
        Debug.Log("VELOCITY IS");
        Debug.Log(rb.velocity);
        // Start falling
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("GREEN COLLIDE");
        // Stop drop (velocity = 0?)
        if (other.gameObject.layer != 8){
            // anim.SetBool("hit", true);
            rb.velocity = new Vector3(0,0,0); // Don't stop on enemies!
            // Deactivate();
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
        lifetime = 0;
    }
}
