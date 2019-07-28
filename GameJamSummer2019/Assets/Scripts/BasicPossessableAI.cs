using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPossessableAI : MonoBehaviour
{
    private Rigidbody2D m_Rb;

    public float range = 3;

    public Sprite currentSprite;

    private float maxLeft;
    private float maxRight;

    public Possessable possessable;
    private float dir;

    private bool isInWater = false;

    // Start is called before the first frame update
    void Start()
    {
        m_Rb = GetComponent<Rigidbody2D>();
        maxLeft = m_Rb.position.x;
        maxRight = maxLeft + range;

        possessable = GetComponent<Possessable>();
        dir = possessable.state.speed;
    }

    // Update is called once per frame

   

  

    public bool bIsGrounded
    {
        get
        {
            if (isInWater) return true;


            RaycastHit2D[] hits2d = new RaycastHit2D[12];
            int numHits;

            ContactFilter2D contactFilter = new ContactFilter2D();
            if (currentSprite)
                numHits = Physics2D.Raycast(transform.position, Vector2.down, contactFilter, hits2d, currentSprite.bounds.size.y / 2f);
            else
                numHits = Physics2D.Raycast(transform.position, Vector2.down, contactFilter, hits2d, 0.55f);

            Debug.Log(numHits);
            return numHits > 1;


        }
    }

    float verticalVelocity;

    void Update()
    {

        verticalVelocity = m_Rb.velocity.y;
        
        if (isInWater)
        {
            float change = 0;
            if (possessable.state.canSwim) change = .8f;
            verticalVelocity = (Mathf.Clamp(transform.position.y+change, -1, 1) * -1);
        }

        Debug.Log(possessable.state.name);
        float x = m_Rb.position.x;
        if (x < maxLeft && dir == possessable.state.speed*-1 || x > maxRight && dir == possessable.state.speed)
        {
            dir *= -1;
        }
        
        if(bIsGrounded)
            m_Rb.velocity = new Vector3((dir/3)/(isInWater?2:1), verticalVelocity, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "water") isInWater = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "water") isInWater = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "water") isInWater = true;
    }
   
}
