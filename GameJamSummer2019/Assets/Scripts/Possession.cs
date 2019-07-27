using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possession : MonoBehaviour
{
    private Rigidbody2D m_Rb;

    public Sprite currentSprite;

    public string state = "ghost";
    private string colliderState = "null";
    private bool isPossessionButtonPressed = false;


    void Start()
    {
        m_Rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    { 
   
            isPossessionButtonPressed = InputManager.Instance.GetPossessionButtonDown_Player1();

        if (isPossessionButtonPressed)
        {
            if (state == "ghost" && colliderState!="null")
            {
                Debug.Log("You possessed a " + colliderState);
                state = colliderState;
                colliderState = "null";
                //Despawn the creature that you possessed
            }
            else
            {
                state = "ghost";
                colliderState = "null";
                Debug.Log("You turned back into a ghost");
                //Spawn in the creature that you used to be
            }
        }
   
    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        if (collision.collider.tag == "possessable")
        {
            Debug.Log("Ran into something you can possess");
            Debug.Log("Pressing C?: " + InputManager.Instance.GetPossessionButtonDown_Player1());
                colliderState = collider.GetComponent<Possessable>().state;
                
               
                //Despawn collider
            
        }
    }
}
