using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleCloud : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    
    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
