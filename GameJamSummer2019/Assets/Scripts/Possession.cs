using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possession : MonoBehaviour
{
    private Rigidbody2D m_Rb;

    public Sprite currentSprite;

    public string state = "ghost";
    private string colliderState = "null";
<<<<<<< HEAD
    private bool isPossessionButtonPressed = false;

=======
    private Collider2D currentCollider;
    private bool isPossessionButtonPressed = false;

    public Transform redpandaprefab;
>>>>>>> master

    void Start()
    {
        m_Rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

<<<<<<< HEAD
		isPossessionButtonPressed = false;// InputManager.Instance.GetPossessionButtonDown_Player1();

        if (isPossessionButtonPressed)
        {
            if (state == "ghost" && colliderState!="null")
=======



        isPossessionButtonPressed = InputManager.Instance.GetPossessionButtonDown_Player1();

        if (isPossessionButtonPressed)
        {
            Debug.Log("trying to possess");


            if (state == "ghost" && colliderState != "null")
>>>>>>> master
            {
                Debug.Log("You possessed a " + colliderState);
                state = colliderState;
                colliderState = "null";
                //Despawn the creature that you possessed
<<<<<<< HEAD
            }
            else
            {
=======
                
                Destroy(currentCollider.gameObject);
            }

            else if (state != "ghost")
            {
                if(state=="panda")
                {
                    Instantiate(redpandaprefab, transform.position,Quaternion.identity);
                }

>>>>>>> master
                state = "ghost";
                colliderState = "null";
                Debug.Log("You turned back into a ghost");
                //Spawn in the creature that you used to be
<<<<<<< HEAD
            }
        }
   
    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        if (collision.collider.tag == "possessable")
        {
            Debug.Log("Ran into something you can possess");
            //Debug.Log("Pressing C?: " + InputManager.Instance.GetPossessionButtonDown_Player1());
                colliderState = collider.GetComponent<Possessable>().state;
                
               
                //Despawn collider
            
        }
    }
}
=======


            }


        }

    }

       private void OnCollisionStay2D(Collision2D collision)
        {
            Collider2D collider = collision.collider;
            if (collision.collider.tag == "possessable" && state=="ghost")
            {
                Debug.Log("Ran into something you can possess");

                Debug.Log("Pressing C?: " + InputManager.Instance.GetPossessionButtonDown_Player1());



                colliderState = collider.GetComponent<Possessable>().state;
            currentCollider = collider;

            //Despawn collider
        
                

            }
        }

    private void OnCollisionExit2D(Collision2D collision)
    {
        currentCollider = null;
        colliderState = "null";
    }
}

>>>>>>> master
