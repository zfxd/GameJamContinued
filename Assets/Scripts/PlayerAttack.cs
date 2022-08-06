using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Image red;
    [SerializeField] Image blue;
    [SerializeField] Image yellow;
    [SerializeField] Image centre;

    public State color {get; private set;}
    public enum State {EMPTY, 
                RED, 
                BLUE,
                YELLOW, 
                PURPLE, 
                GREEN, 
                ORANGE}
    private Color inactive = new Color(0.8f, 0.8f, 0.8f, 1);
    private Color active = Color.white;
    // Reference to playermove?

    

    // Start is called before the first frame update
    void Start()
    {
        color = State.EMPTY;
    }

    // Update is called once per frame
    // What happens when multiple keys are pressed at once?
    void Update()
    {
        // A: Blue
          if (Input.GetKey(KeyCode.A))
        {
            // Change sprite
            blue.color = active;

            // Change state and color

        }
        else
        {
            // Reset
            blue.color = inactive;
        }

        // W: Red
        if (Input.GetKey(KeyCode.W))
        {
            // Change Sprite
            red.color = active;
        }
        else 
        {
            // Reset Sprite
            red.color = inactive;
        }

        // D: Yellow
        if (Input.GetKey(KeyCode.D))
        {
            // Change Sprite
            yellow.color = active;
        }
        else
        {
            // Reset Sprite
            yellow.color = inactive;
        }

        // Shift: Cancel
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Change state and color
        }

        if (Input.GetKey(KeyCode.Space))
        {
            // Most fire off once and return to normal
            // Special case: FIRE.
        }
    }
}
