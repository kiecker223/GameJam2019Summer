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
		string[] joystickNames = Input.GetJoystickNames();
		int numJoysticks = joystickNames.Length;
		bool bHasController = false;
		foreach (var joystick in joystickNames)
		{
			if (joystick.Length != 0)
			{
				bHasController = true;
			}
		}
		m_bHasController = bHasController;
	}

	// TODO: Player controller support
	public float GetHorizontalAxisLeftStick_Player1()
	{
		if (m_bHasController)
			return Input.GetAxis("Joy_1_LH");
		return (Input.GetKey(KeyCode.A) ? -1.0f : 0.0f) + (Input.GetKey(KeyCode.D) ? 1.0f : 0.0f);
	}

	public float GetVerticalAxisRightStick_Player1()
	{
		return Input.GetAxis("Joy_1_LV");
	}

	public bool GetJumpButtonDown_Player1()
	{
		if (m_bHasController)
			return Input.GetButtonDown("Button_A");
		return Input.GetKeyDown(KeyCode.Space);
	}

	void Update()
	{

	}
}
