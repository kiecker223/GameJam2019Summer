using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possession : MonoBehaviour
{
    private Rigidbody2D m_Rb;

    public Sprite currentSprite;

<<<<<<< Updated upstream
    public string state = "ghost";
    private string colliderState = "null";
<<<<<<< HEAD
<<<<<<< HEAD
    private bool isPossessionButtonPressed = false;
=======
    public CreatureState state;
    public CreatureState ghost;
    //public string state = "ghost";
    private CreatureState colliderState = new CreatureState();
>>>>>>> Stashed changes

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
        //Initialize Ghost
        ghost.canSwim = false;
        ghost.canClimb = false;
        ghost.canFly = false;
        ghost.speed = 3;
        ghost.name="ghost";



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


<<<<<<< Updated upstream
            if (state == "ghost" && colliderState != "null")
<<<<<<< HEAD
>>>>>>> master
=======
>>>>>>> master
            {
                Debug.Log("You possessed a " + colliderState);
=======
            if (state.name == "ghost" && colliderState.name != "null"){
                Debug.Log("You possessed a " + colliderState.name);
>>>>>>> Stashed changes
                state = colliderState;
                colliderState.name = "null";
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
<<<<<<< Updated upstream

            else if (state != "ghost")
            {
                if(state=="panda")
=======
            else if (state.name != "ghost")
            {
                if (state.name == "panda")
                {
                    Instantiate(redpandaprefab, transform.position, Quaternion.identity);
                }
                else if (state.name == "tiger")
                {
                    Instantiate(tigerprefab, transform.position, Quaternion.identity);
                }
                else if (state.name == "crane")
>>>>>>> Stashed changes
                {
                    Instantiate(redpandaprefab, transform.position,Quaternion.identity);
                }
<<<<<<< Updated upstream

<<<<<<< HEAD
>>>>>>> master
=======
>>>>>>> master
                state = "ghost";
                colliderState = "null";
=======
                else if (state.name == "koifish")
                {
                    Instantiate(koifishprefab, transform.position, Quaternion.identity);
                }


                state = ghost;
                colliderState.name = "null";
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        if (collision.collider.tag == "possessable")
=======
        if (collision.collider.tag == "possessable" && state.name == "ghost")
>>>>>>> Stashed changes
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
        colliderState.name = "null";
    }
}

<<<<<<< HEAD
>>>>>>> master
=======
>>>>>>> master
