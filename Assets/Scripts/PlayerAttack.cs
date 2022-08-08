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

    

    void Awake()
    {
        color = State.EMPTY;
        anim = GetComponent<Animator>();
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
                case State.BLUE:
                    anim.SetTrigger("atkBlue");
                    break;
            }
            BlankState();
            // Special case: FIRE.
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
}

// Red: Use a coroutine, and disable the atk function until the coroutine ends
// Coroutines are NOT stopped when monobehavior.enable = false