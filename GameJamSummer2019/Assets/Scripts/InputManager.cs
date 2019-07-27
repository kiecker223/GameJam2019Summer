using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	public static InputManager Instance { get; private set; }

	bool m_bHasController;

	void Awake()
	{
		Instance = this;
		int numJoysticks = Input.GetJoystickNames().Length;
		m_bHasController = numJoysticks > 0;
	}

	// TODO: Player controller support
	public float GetHorizontalAxisLeftStick_Player1()
	{
		return Input.GetAxis("Joy_1_LH");
	}

	public float GetVerticalAxisRightStick_Player1()
	{
		return Input.GetAxis("Joy_1_LV");
	}

	public bool GetJumpButtonDown_Player1()
	{
		return Input.GetButtonDown("Button_A");
	}

	void Update()
	{

	}
}
