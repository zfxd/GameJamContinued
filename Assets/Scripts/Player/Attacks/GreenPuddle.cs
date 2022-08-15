using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPuddle : MonoBehaviour
{
    [SerializeField] private float lifetime;
    private float direction;

    private Animator anim;
    private BoxCollider2D boxCollider;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDirection(float _direction)
    {
        gameObject.SetActive(true);
        direction = _direction;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
        
    }
}
