using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private Rigidbody2D m_Rb;

	public Sprite currentSprite;

	public float characterSpeedHorizontal;

	private float m_VerticalVelocity;

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

    void Start()
    {
		m_Rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
		bool bGrounded = bIsGrounded;
		float inheritedVelocity = m_Rb.velocity.x;
        float left = InputManager.Instance.GetHorizontalAxisLeftStick_Player1() * characterSpeedHorizontal;// : inheritedVelocity;

		bool jumping = InputManager.Instance.GetJumpButtonDown_Player1() && bGrounded;
		Debug.Log("Jumping: " + jumping.ToString());
		
		float verticalVelocity = jumping ? 5f : m_Rb.velocity.y;
		
		m_Rb.velocity = new Vector3(left, verticalVelocity, 0);
    }
}
