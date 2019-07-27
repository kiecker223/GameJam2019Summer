using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private Rigidbody2D m_Rb;

	public float characterSpeedHorizontal;

    void Start()
    {
		m_Rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
		float left = InputManager.Instance.GetHorizontalAxisLeftStick_Player1() * characterSpeedHorizontal;
		m_Rb.velocity = new Vector3(left, 0, 0);
    }
}
