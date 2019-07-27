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
<<<<<<< HEAD
    private bool isPossessionButtonPressed = false;

=======
=======
>>>>>>> master
    private Collider2D currentCollider;
    private bool isPossessionButtonPressed = false;

    public Transform redpandaprefab;
<<<<<<< HEAD
>>>>>>> master
=======
>>>>>>> master

    void Start()
    {
        m_Rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

<<<<<<< HEAD
<<<<<<< HEAD
		isPossessionButtonPressed = false;// InputManager.Instance.GetPossessionButtonDown_Player1();

        if (isPossessionButtonPressed)
        {
            if (state == "ghost" && colliderState!="null")
=======
=======
>>>>>>> master



        isPossessionButtonPressed = InputManager.Instance.GetPossessionButtonDown_Player1();

        if (isPossessionButtonPressed)
        {
            Debug.Log("trying to possess");


            if (state == "ghost" && colliderState != "null")
<<<<<<< HEAD
>>>>>>> master
=======
>>>>>>> master
            {
                Debug.Log("You possessed a " + colliderState);
                state = colliderState;
                colliderState = "null";
                //Despawn the creature that you possessed
<<<<<<< HEAD
<<<<<<< HEAD
            }
            else
            {
=======
=======
>>>>>>> master
                
                Destroy(currentCollider.gameObject);
            }

            else if (state != "ghost")
            {
                if(state=="panda")
                {
                    Instantiate(redpandaprefab, transform.position,Quaternion.identity);
                }

<<<<<<< HEAD
>>>>>>> master
=======
>>>>>>> master
                state = "ghost";
                colliderState = "null";
                Debug.Log("You turned back into a ghost");
                //Spawn in the creature that you used to be
<<<<<<< HEAD
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
=======
>>>>>>> master


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

<<<<<<< HEAD
>>>>>>> master
=======
>>>>>>> master
