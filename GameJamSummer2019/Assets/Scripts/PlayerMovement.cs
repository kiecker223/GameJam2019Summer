using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private Rigidbody2D m_Rb;

	public GameObject playerModel;

	public Sprite currentSprite;

	public float characterSpeedHorizontal;

	private float m_VerticalVelocity;

	private bool m_bFacingRight;

    public Possession possession;

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

            //Climbing for panda
            if (possession.state == "panda")
            {

            }

            Debug.Log(numHits);
			return numHits > 1;
            
         
		}
	}

    void Start()
    {
		m_Rb = GetComponent<Rigidbody2D>();
        possession = GetComponent<Possession>();
    }

    void Update()
    {
		bool bGrounded = bIsGrounded;
		float inheritedVelocity = m_Rb.velocity.x;
        float horizontalDir = InputManager.Instance.GetHorizontalAxisLeftStick_Player1() * characterSpeedHorizontal;// : inheritedVelocity;

		if (horizontalDir > 0.001f)
		{
			m_bFacingRight = true;
		}
		else if (horizontalDir < -0.001f)
		{
			m_bFacingRight = false;
		}
		else { } // Do nothing, Also bad code!!

		bool jumping = InputManager.Instance.GetJumpButtonDown_Player1() && bGrounded;
		Debug.Log("Jumping: " + jumping.ToString());
		
		float verticalVelocity = jumping ? 5f : m_Rb.velocity.y;
		//playerModel.transform.right = new Vector3(m_bFacingRight ? 1.0f : -1.0f, 0, 0);
		m_Rb.velocity = new Vector3(horizontalDir, verticalVelocity, 0);
    }
}
