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
	ParryHigh,
	ParryLow,
	FeintPunch,
	FeintKick
}

public class CombatantComponent : MonoBehaviour
{
	public float health { get; set; }

	public float punchDamage;
	public float kickDamage;

	private float m_ComboTimer;
	public float optimalTime;

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
			if (m_bControllerHigh) combatCombo.Enqueue(CombatState.ParryHigh);
			else combatCombo.Enqueue(CombatState.ParryLow);
		}
	}

	public void Stagger(float staggerTime)
	{
		combatCombo.Clear();
		combatState = CombatState.Staggered;
		m_ComboTimer = staggerTime;
	}

	public float GetDamageForCurrentAttack()
	{
		switch (combatState)
		{
		case CombatState.Punching:
		case CombatState.SecondPunch:
			return punchDamage;
		case CombatState.LowKick:
			return kickDamage * 0.89f;
		case CombatState.HighKick:
			return kickDamage;
		default:
			return 0.0f;
		};
	}

	public static bool IsHighAttackState(CombatState inState)
	{
		return inState == CombatState.HighKick || inState == CombatState.Punching || inState == CombatState.SecondPunch;
	}

	public static bool IsLowAttackState(CombatState inState)
	{
		return inState == CombatState.LowKick;
	}

	public void RecieveInteraction(CombatantComponent other)
	{
		 
	}

	CombatantComponent GetEnemyCurrentlyHitting()
	{
		RaycastHit2D[] rayHits = new RaycastHit2D[12];
		ContactFilter2D contactFilter = new ContactFilter2D();
		int numHits = Physics2D.Raycast(transform.position, transform.right, contactFilter, rayHits, 0.4f);
		if (numHits > 1)
		{
			var collider = rayHits[1].collider;
			if (collider)
			{
				return collider.gameObject.GetComponent<CombatantComponent>();
			}
		}
		return null;
	}

	void Update()
	{
		CombatState cState = combatState;
		m_ComboTimer -= Time.deltaTime;
		if (m_ComboTimer <= 0.0f)
		{
			// Do nothing for now?
			if (combatCombo.Count == 0)
			{
				cState = CombatState.None;
			}
			else
			{
				cState = combatCombo.Dequeue();
			}
			combatState = cState;
		}

		var enemy = GetEnemyCurrentlyHitting();

		
	}
}
