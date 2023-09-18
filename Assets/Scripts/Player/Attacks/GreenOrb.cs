using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenOrb : MonoBehaviour
{
    [SerializeField] GameObject[] greenPuddles;

    [SerializeField] private float speed;
    [SerializeField] private float _lifetime;
    [SerializeField]private LayerMask groundLayer;
    private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    
    private float lifetime;
    float direction = 1.0f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); 
        circleCollider = GetComponent<CircleCollider2D>();
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
        // Stop drop
        if (other.gameObject.layer != 8){
            // anim.SetBool("hit", true);
            rb.velocity = new Vector3(0,0,0); // Don't stop on enemies!
            // Spawn a puddle!
            greenPuddles[FindInList(greenPuddles)].transform.position = gameObject.transform.position;
            // Also detect floor or wall and pass into SetDirection
            float r = 0f;
            if (hitRight()) {
                r = 90f;
                Debug.Log("GREEN HIT RIGHT");
            }
            else if (hitLeft()){
                r = -90f;
                Debug.Log("GREEN HIT LEFT");
            }

            greenPuddles[FindInList(greenPuddles)].GetComponent<GreenPuddle>().SetDirection(Mathf.Sign(transform.localScale.x), r);


            Deactivate();
        }
    }

    private bool hitRight()
    {
        RaycastHit2D raycastHitRight = Physics2D.BoxCast(circleCollider.bounds.center, circleCollider.bounds.size, 0, Vector2.right, 0.1f, groundLayer);

        return raycastHitRight.collider != null;;
    }

    private bool hitLeft(){
        RaycastHit2D raycastHitLeft = Physics2D.BoxCast(circleCollider.bounds.center, circleCollider.bounds.size, 0, Vector2.left, 0.1f, groundLayer);

        return raycastHitLeft.collider != null;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
        lifetime = 0;
    }

    private int FindInList(GameObject[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            if (!list[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
