using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	public static InputManager Instance { get; private set; }

	bool m_bHasController;

	private Vector2 m_MouseDelta;
	private Vector2 m_LastMousePosition;

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
            Debug.Log(joystick);
		}
		m_bHasController = bHasController;
        Debug.Log(m_bHasController);
       
	}

	// TODO: Player controller support
	public float GetHorizontalAxisLeftStick_Player1()
	{
        if (m_bHasController)
         return Input.GetAxis("Joy_1_LH");
        return (Input.GetKey(KeyCode.A) ? -1.0f : 0.0f) + (Input.GetKey(KeyCode.D) ? 1.0f : 0.0f);
        
        
	}

	public float GetVerticalAxisLeftStick_Player1()
	{
		return Input.GetAxis("Joy_1_LV");
	}

	public bool GetJumpButtonDown_Player1()
	{
		if (m_bHasController)
			return Input.GetButtonDown("Button_A");
		return Input.GetKeyDown(KeyCode.Space);
	}

    public bool GetPrimaryAttackButtonDown_Player1()
    {
        if (m_bHasController)
            return Input.GetButtonDown("Button_X");
        return Input.GetKeyDown(KeyCode.X);
    }

    public bool GetSecondaryButtonDown_Player1()
    {
        if (m_bHasController)
            return Input.GetButtonDown("Button_Y");
        return Input.GetKeyDown(KeyCode.Z);
    }

    public bool GetPossessionButtonDown_Player1()
    {
        if (m_bHasController)
            return Input.GetButtonDown("Button_B");
        return Input.GetKeyDown(KeyCode.C);
    }

    public bool GetPauseButtonDown_Player1()
    {
        if (m_bHasController)
            return Input.GetButtonDown("Button_Start");
        return Input.GetKeyDown(KeyCode.Escape);
    }

    public float GetHorizontalAxisRightStick_Player1()
	{
		if (m_bHasController)
			return Input.GetAxis("Joy_1_RH");
		return m_MouseDelta.x;
	}

	public float GetVerticalAxisRightStick_Player1()
	{
		if (m_bHasController)
			return Input.GetAxis("Joy_1_RV");
		return m_MouseDelta.y;
	}

    bool isPaused = false;

	void Update()
	{
		Vector2 mousePosition = Input.mousePosition;
		m_MouseDelta = mousePosition - m_LastMousePosition;
		m_LastMousePosition = mousePosition;

        if(GetPauseButtonDown_Player1())
        {
            isPaused = !isPaused;
        }
	}
}
