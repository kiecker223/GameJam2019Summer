using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	public static InputManager Instance { get; private set; }

	void Awake()
	{
		Instance = this;
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
