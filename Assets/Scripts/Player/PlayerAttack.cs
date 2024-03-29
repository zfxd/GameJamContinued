using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Image redOrb;
    [SerializeField] Image blueOrb;
    [SerializeField] Image yellowOrb;
    [SerializeField] Image centre;

    public State color {get; private set;}
    public enum State {EMPTY,

                RED,
                BLUE,
                YELLOW,
                PURPLE,
                GREEN, 
                ORANGE}

    // Colors
    private Color inactive = new Color(0.8f, 0.8f, 0.8f, 1);
    private Color active = Color.white;
    private Color orange = new Color(1.0f, 0.64f, 0.0f, 1);
    private Color purple = new Color(0.45f, 0f, 1.0f, 1);
    private Color grey = new Color(0.32f, 0.32f, 0.32f, 1);
    private Color blue = new Color(0f,0f,0.8f,1);
    private Color red = new Color(0.7f,0f,0f,1);
    private Color green = new Color(0f,0.7f,0f,1);
    
    // Component references
    private Animator anim;
    private Animator fireAnim;

    // Making attacks work
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform blueSpawn;
    [SerializeField] private GameObject[] yellowProjectiles;
    [SerializeField] private GameObject[] greenPuddles;
    [SerializeField] private GameObject[] greenOrbs;
    [SerializeField] private GameObject[] purpleClouds;
    [SerializeField] private GameObject[] orangeExplosion;
    [SerializeField] private GameObject[] blueBlocks;
    [SerializeField] private GameObject redFire;
    private GameObject currSpell;
    

    void Awake()
    {
        color = State.EMPTY;
        anim = GetComponent<Animator>();
        fireAnim = redFire.GetComponent<Animator>();
        // Ignore layer collisions;
    }

    // Update is called once per frame
    // What happens when multiple keys are pressed at once?
    void Update()
    {
        // A: Blue
          if (Input.GetKey(KeyCode.A))
        {
            // Change sprite
            blueOrb.color = active;

            // Change state and color
            switch (color)
            {
                case State.EMPTY:
                    BlueState();
                    break;
                case State.RED:
                    PurpleState();
                    break;
                case State.YELLOW:
                    GreenState();
                    break;
            }
        }
        else
        {
            // Reset
            blueOrb.color = inactive;
        }

        // W: Red
        if (Input.GetKey(KeyCode.W))
        {
            // Change Sprite
            redOrb.color = active;

            // Change state and color
            switch (color)
            {
                case State.EMPTY:
                    RedState();
                    break;
                case State.BLUE:
                    PurpleState();
                    break;
                case State.YELLOW:
                    OrangeState();
                    break;
            }
        }
        else 
        {
            // Reset Sprite
            redOrb.color = inactive;
        }

        // D: Yellow
        if (Input.GetKey(KeyCode.D))
        {
            // Change Sprite
            yellowOrb.color = active;

            // Change state and color
            switch (color)
            {
                case State.EMPTY:
                    YellowState();
                    break;
                case State.BLUE:
                    GreenState();
                    break;
                case State.RED:
                    OrangeState();
                    break;
            }
        }
        else
        {
            // Reset Sprite
            yellowOrb.color = inactive;
        }

        // Shift: Cancel
        if (Input.GetKey(KeyCode.LeftShift))
        {
            BlankState();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            // Most fire off once and return to normal
            switch(color)
            {
                case State.RED:
                    StartCoroutine(atkRed());
                    break;
                case State.BLUE:
                    anim.SetTrigger("atkBlue");
                    break;
                case State.YELLOW:
                    anim.SetTrigger("atkYellow");
                    break;
                case State.GREEN:
                    anim.SetTrigger("atkGreen");
                    break;
                case State.ORANGE:
                    anim.SetTrigger("atkOrange");
                    break;
                case State.PURPLE:
                    anim.SetTrigger("atkPurple");
                    break;
            }
            BlankState();
        }
    }

    private void RedState()
    {
        centre.color = red;
        color = State.RED;
    }

    private void BlueState()
    {
        centre.color = blue;
        color = State.BLUE;
    }

    private void YellowState()
    {
        centre.color = Color.yellow;
        color = State.YELLOW;
    }

    private void OrangeState()
    {
        centre.color = orange;
        color = State.ORANGE;
    }

    private void PurpleState()
    {
        centre.color = purple;
        color = State.PURPLE;
    }

    private void GreenState()
    {
        centre.color = green;
        color = State.GREEN;
    }

    private void BlankState()
    {
        centre.color = grey;
        color = State.EMPTY;
    }
    

    // Do I need these?
    private IEnumerator atkRed()
    {
        this.enabled = false;
        redFire.gameObject.SetActive(true);
        fireAnim.SetBool("firing", true);
        anim.SetBool("firing", true);
        yield return new WaitUntil(() => !Input.GetKey(KeyCode.Space));
        endRed();
        this.enabled = false;
        yield return new WaitForSeconds(0.5f);
        this.enabled = true;        
    }
    // Called by other events such as taking damage to end
    // the coroutine prematurely but elegantly
    // !! however this is being called every time the player loses control
    // - casting other spells is the most notable instance
    // Will this cause issues?
    public void endRed()
    {
        if (fireAnim.gameObject.activeSelf)
            fireAnim.SetBool("firing", false);
        anim.SetBool("firing", false);
        this.enabled = true;
    }
   
    private void atkBlue()
    {
        currSpell = blueBlocks[FindInList(blueBlocks)];
        currSpell.transform.position = blueSpawn.position;
        currSpell.gameObject.SetActive(true);
    }

    private void atkYellow()
    {
        currSpell = yellowProjectiles[FindInList(yellowProjectiles)];
        currSpell.transform.position = firePoint.position;
        currSpell.GetComponent<YellowProjectile>().SetDirection(Mathf.Sign(transform.localScale.x));  
    }

    private void atkOrange()
    {
        // Draw a raycast to determine the point of explosion
        LayerMask atks = LayerMask.GetMask("Attacks") | LayerMask.GetMask("IgnoreAttacksAndPlayer");
        // Do I want to include the "IgnoreAttacks" layer too.. I'm not sure if they will ever collide
        Vector2 dir = new Vector2(transform.localScale.x, 0);
        Vector3 dirDebug = new Vector3(transform.localScale.x, 0, 0);
        RaycastHit2D hit = Physics2D.Raycast(firePoint.transform.position, dir, Mathf.Infinity, ~atks);
        if (hit.collider != null)
        {
            Debug.DrawRay(firePoint.transform.position, dirDebug * hit.distance/3, Color.yellow, 3);
            currSpell = orangeExplosion[FindInList(orangeExplosion)];
            currSpell.transform.position = hit.point;
            currSpell.gameObject.SetActive(true);

        }
        else{
            Debug.DrawRay(firePoint.transform.position, dirDebug * 1000, Color.white, 3);
        }
    }

    private void atkPurple()
    {
        currSpell = purpleClouds[FindInList(purpleClouds)];
        Vector2 dir = new Vector2(transform.localScale.x, 0).normalized;
        LayerMask toHit = LayerMask.GetMask("Ground");  
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(firePoint.transform.position.x,
            firePoint.transform.position.y - .2f), dir, 1.862f, toHit);
        Debug.DrawRay(new Vector2(firePoint.transform.position.x,
            firePoint.transform.position.y - .2f), dir, Color.yellow, 1.862f);
        if (hit) {
            // If there's a wall in the way, then spawn it at the wall
            // If the spawn point goes over an edge
            // (I have a collider which will collide with the raycast at the edge of platforms)
            currSpell.transform.position = hit.point + new Vector2(0,0.1f);
        }
        // If not use the spawn point, if it is in mid air it will fall (see PurpleCloud.cs)
        else {
            currSpell.transform.position = spawnPoint.position + new Vector3(0,0.1f,0);
        }
        currSpell.GetComponent<PurpleCloud>().SetDirection(Mathf.Sign(transform.localScale.x));
        // Check if grounded
        // If not grounded, drop.
    }

    private void atkGreen()
    {
        // New green behavior - we want to spawn a projectile like blue
        currSpell = greenOrbs[FindInList(greenOrbs)];
        currSpell.transform.position = blueSpawn.position;
        currSpell.GetComponent<GreenOrb>().SetDirection(Mathf.Sign(transform.localScale.x)); 
        

        // Old green behavior
        // greenPuddles[FindInList(greenPuddles)].transform.position = spawnPoint.position;
        // greenPuddles[FindInList(greenPuddles)].GetComponent<GreenPuddle>().SetDirection(Mathf.Sign(transform.localScale.x)); // same here
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

// Red: Use a coroutine, and disable the atk function until the coroutine ends
// Coroutines are NOT stopped when monobehavior.enable = false