using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    private float direction;

    private Animator anim;
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hit)
             return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("exit");
    }

    // Initializing the appropriate values for the projectile
    public void SetDirection(float _direction)
    {
        gameObject.SetActive(true);
        direction = _direction;
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
