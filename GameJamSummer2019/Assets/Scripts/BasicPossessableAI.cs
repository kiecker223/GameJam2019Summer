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

    // Start is called before the first frame update
    void Start()
    {
        m_Rb = GetComponent<Rigidbody2D>();
        maxLeft = m_Rb.position.x;
        maxRight = maxLeft + range;
    }

    // Update is called once per frame

    private int dir = 1;


    public bool bIsGrounded
    {
        get
        {


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


    void Update()
    {
        float x = m_Rb.position.x;
        if (x < maxLeft && dir == -1 || x > maxRight && dir == 1)
        {
            dir *= -1;
        }
        if(bIsGrounded)m_Rb.velocity = new Vector3(dir, m_Rb.velocity.y, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    
    }
}
