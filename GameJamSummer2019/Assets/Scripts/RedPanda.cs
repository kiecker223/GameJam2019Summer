using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPanda : MonoBehaviour
{

    private Rigidbody2D m_Rb;

    public float range = 3;

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

    void Update()
    {
        float x = m_Rb.position.x;
        if(x<maxLeft && dir == -1 || x>maxRight && dir == 1)
        {
            dir *= -1;
        }
        m_Rb.velocity = new Vector3(dir, 0, 0);
    }
}
