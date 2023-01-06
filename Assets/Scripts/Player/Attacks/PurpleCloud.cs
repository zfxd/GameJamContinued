using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleCloud : MonoBehaviour
{
    [SerializeField] float speed;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    
    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Drop()
    {
        rb.velocity = new Vector3(0, -1 * speed, 0);
        // Start falling
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("PURPLE COLLIDE");
        // Stop drop (velocity = 0?)
        if (other.gameObject.layer != 8){
            rb.velocity = new Vector3(0,0,0); // Don't stop on enemies!
        }
    }

    // Initializing values and flipping sprite as needed
    public void SetDirection(float _direction)
    {
        gameObject.SetActive(true);
        boxCollider.enabled = true;
        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z); 
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
