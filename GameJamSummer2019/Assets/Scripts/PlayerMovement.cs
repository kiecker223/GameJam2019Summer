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
			RaycastHit2D hit2d;
			if (currentSprite)
				hit2d = Physics2D.Raycast(transform.position, Vector2.down, currentSprite.bounds.size.y / 2f);
			else
				hit2d = Physics2D.Raycast(transform.position, Vector2.down, 0.55f);

			return hit2d.collider != null || hit2d.collider.gameObject.tag == "Player";
		}
	}

    void Start()
    {
		m_Rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
		float left = InputManager.Instance.GetHorizontalAxisLeftStick_Player1() * characterSpeedHorizontal;

		bool jumping = InputManager.Instance.GetJumpButtonDown_Player1() && bIsGrounded == true;
		Debug.Log("Jumping: " + jumping.ToString());
		
		float verticalVelocity = jumping ? 5f : m_Rb.velocity.y;
		
		m_Rb.velocity = new Vector3(left, verticalVelocity, 0);
    }
}
