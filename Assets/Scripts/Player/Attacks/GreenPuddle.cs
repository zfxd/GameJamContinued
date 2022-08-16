using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPuddle : MonoBehaviour
{
    [SerializeField] private float _lifetime;
    private float lifetime;

    private BoxCollider2D boxCollider;
    
    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        lifetime += Time.deltaTime;
        if (lifetime > _lifetime)
            gameObject.SetActive(false);
    }

    // Initializing values and flipping sprite as needed
    public void SetDirection(float _direction)
    {
        gameObject.SetActive(true);
        boxCollider.enabled = true;
        lifetime = 0;
        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
        
    }
}
