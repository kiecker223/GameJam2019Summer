using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CombatState
{
	None,
	Punching, 
	SecondPunch,
	LowKick,
	HighKick,
	Clench,
	Staggered,
	Blocking,
	Parry,
	FeintPunch,
	FeintKick
}

public class CombatantComponent : MonoBehaviour
{
	public float health { get; set; }

	private float m_ComboTimer;

	public CombatState combatState { get; set; }

	public Queue<CombatState> combatCombo = new Queue<CombatState>();

	bool m_bControllerHigh
	{
		get
		{
			return InputManager.Instance.GetVerticalAxisRightStick_Player1() > 0.001f;
		}
	}

	public void Punch()
	{
		if (combatState != CombatState.Staggered)
		{
			if (combatState == CombatState.Punching)
			{
				combatCombo.Enqueue(CombatState.SecondPunch);
			}
		}
	}

	public void Kick()
	{
		if (combatState != CombatState.Staggered)
		{
			if (m_bControllerHigh) combatCombo.Enqueue(CombatState.HighKick);
			else combatCombo.Enqueue(CombatState.LowKick);
		}
	}
	
	public void Grab()
	{
		if (combatState != CombatState.Staggered)
		{
			combatCombo.Enqueue(CombatState.Clench);
		}
	}

	public void FeintKick()
	{
		if (combatState != CombatState.Staggered)
		{
			combatCombo.Enqueue(CombatState.FeintKick);
		}
	}

	public void FeintPunch()
	{
		if (combatState != CombatState.Staggered)
		{
			combatCombo.Enqueue(CombatState.FeintPunch);
		}
	}
	
	public void Parry()
	{
		if (combatState != CombatState.Staggered)
		{
			combatCombo.Enqueue(CombatState.Parry);
		}
	}

	public void Stagger(float staggerTime)
	{
		combatCombo.Clear();
		combatState = CombatState.Staggered;
		m_ComboTimer = staggerTime;
	}

	void Update()
	{
		CombatState cState = CombatState.None;
		m_ComboTimer -= Time.deltaTime;
		if (m_ComboTimer <= 0.0f)
		{
			// Do nothing for now?
			if (combatCombo.Count == 0)
			{

			}
			else
			{
				cState = combatCombo.Dequeue();
			}
		}
	}
}
