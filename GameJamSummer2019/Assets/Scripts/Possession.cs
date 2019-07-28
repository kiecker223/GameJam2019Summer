using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possession : MonoBehaviour
{
    private Rigidbody2D m_Rb;

    public Sprite currentSprite;

    private bool isPossessionButtonPressed = false;

    public CreatureState state;
    public CreatureState ghost;
    
    private CreatureState colliderState = new CreatureState();


    private Collider2D currentCollider;

    public Transform redpandaprefab;
    public Transform koifishprefab;
    public Transform tigerprefab;
    public Transform craneprefab;

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

        isPossessionButtonPressed = InputManager.Instance.GetPossessionButtonDown_Player1();

        if (isPossessionButtonPressed)
        {
            Debug.Log("trying to possess");
            if (state.name == "ghost" && colliderState.name != "null"){
                Debug.Log("You possessed a " + colliderState.name);

                state = colliderState;
                colliderState.name = "null";
                Destroy(currentCollider.gameObject);
            }
       
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

                {
                    Instantiate(craneprefab, transform.position,Quaternion.identity);
                }
                

                else if (state.name == "koifish")
                {
                    Instantiate(koifishprefab, transform.position, Quaternion.identity);
                }


                state = ghost;
                colliderState.name = "null";

                Debug.Log("You turned back into a ghost");
                //Spawn in the creature that you used to be

            }
        }
   
    }



       private void OnCollisionStay2D(Collision2D collision)
        {
            Collider2D collider = collision.collider;
            if (collision.collider.tag == "possessable" && state.name=="ghost")
            {
                Debug.Log("Ran into something you can possess");

                Debug.Log("Pressing C?: " + InputManager.Instance.GetPossessionButtonDown_Player1());



                colliderState = collider.GetComponent<Possessable>().state;
             currentCollider = collider;

            //Despawn collider
        
                

            }
        }


}
