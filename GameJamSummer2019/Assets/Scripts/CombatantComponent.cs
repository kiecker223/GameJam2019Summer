using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CombatState
{
	None,
	Jab,
	SecondPunch,
	ThirdPunch,
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

public struct CombatEvent
{
	public CombatState combatState;
	public float comboTime;
}

public class CombatantComponent : MonoBehaviour
{
	public float health;
	public float punchDamage;
	public float kickDamage;

	public float comboTimer
	{
		get;
		private set;
	}
	public float comboTime
	{
		get;
		private set;
	}
	public float optimalTimeStart;
	public float optimalTimeEnd;

	public float speedMultiplier;

	public CombatState combatState { get; set; }

	public List<CombatEvent> combatCombo = new List<CombatEvent>();

	bool m_bControllerHigh
	{
		get
		{
			return InputManager.Instance.GetVerticalAxisRightStick_Player1() > 0.001f;
		}
	}

	public static void GetOptimalTime(ref float timeStart, ref float timeEnd, CombatState combatState)
	{
		switch (combatState)
		{
		case CombatState.Jab:
			timeStart = 14f / 60f;
			timeEnd = 17f / 60f;
			break;
		case CombatState.SecondPunch:
			timeStart = 10f / 60f;
			timeEnd = 15f / 60f;
			break;
		case CombatState.ThirdPunch:
			timeStart = 6f / 60f;
			timeEnd = 10f / 60f;
			break;
		case CombatState.LowKick:
			timeStart = 10f / 60f;
			timeEnd = 17f / 60f;
			break;
		case CombatState.HighKick:
			break;
		case CombatState.ParryLow:
		case CombatState.ParryHigh:
			timeStart = 11f / 60f;
			timeEnd = 22f / 60f;
			break;
		}
	}

	public void Punch()
	{
		if (combatState != CombatState.Staggered)
		{
			if (combatState == CombatState.Jab || combatCombo[combatCombo.Count - 1].combatState == CombatState.Jab)
			{
				combatCombo.Add(new CombatEvent() { combatState = CombatState.SecondPunch, comboTime = 15f / 60f });
			}
			else if (combatState == CombatState.SecondPunch || combatCombo[combatCombo.Count - 1].combatState == CombatState.SecondPunch)
			{
				combatCombo.Add(new CombatEvent() { combatState = CombatState.ThirdPunch, comboTime = 10f / 60f });
			}
			else if (combatCombo[combatCombo.Count - 1].combatState != CombatState.Jab)
			{
				combatCombo.Add(new CombatEvent() { combatState = CombatState.Jab, comboTime = 20f / 60f });
			}
		}
	}

	public void Kick()
	{
		if (combatState != CombatState.Staggered)
		{
			combatCombo.Add(new CombatEvent() { combatState = CombatState.HighKick, comboTime = 30f / 60f });
		}
	}
	
	public void Grab()
	{
		if (combatState != CombatState.Staggered)
		{
			//combatCombo.Add(CombatState.Clench);
		}
	}

	public void FeintKick()
	{
		if (combatState != CombatState.Staggered)
		{
			//combatCombo.Add(CombatState.FeintKick);
		}
	}

	public void FeintPunch()
	{
		if (combatState != CombatState.Staggered)
		{
			//combatCombo.Add(CombatState.FeintPunch);
		}
	}
	
	public void Parry()
	{
		if (combatState != CombatState.Staggered)
		{
			if (m_bControllerHigh) combatCombo.Add(new CombatEvent() { combatState = CombatState.ParryHigh, comboTime = 31f / 60f });
			//else combatCombo.Add(CombatState.ParryLow);
		}
	}

	public void Stagger(float staggerTime)
	{
		combatCombo.Clear();
		combatState = CombatState.Staggered;
		comboTimer = staggerTime;
	}

	public float GetDamageForCurrentAttack()
	{
		switch (combatState)
		{
		case CombatState.Jab:
		case CombatState.SecondPunch:
		case CombatState.ThirdPunch:
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
		return inState == CombatState.HighKick || inState == CombatState.Jab || inState == CombatState.SecondPunch || inState == CombatState.ThirdPunch;
	}

	public static bool IsLowAttackState(CombatState inState)
	{
		return inState == CombatState.LowKick;
	}

	public static bool HasParried(float o1Start, float o1End, float o1ComboTime, float o2Start, float o2End, float o2ComboTime, CombatState o1CombatState, float o1SpeedMultipliers, CombatState o2CombatState, float o2SpeedMultipliers)
	{
		if (!(IsHighAttackState(o1CombatState) && o2CombatState == CombatState.ParryHigh))
		{
			return false;
		}
		if (!(IsLowAttackState(o1CombatState) && o2CombatState == CombatState.ParryLow))
		{
			return false;
		}
		// If it is at a time when it can be parried?
		if (o1Start < o1ComboTime && o1ComboTime < o1End)
		{
			// Check if the other guy is actually parrying
			if (o2Start < o2ComboTime && o2ComboTime < o2End)
			{
				return true;
			}
		}
		return false;
	}

	public void RecieveInteraction(CombatantComponent other)
	{
		Debug.Log("Recieved interaction");
		float otherOptimalStart = 0.0f, otherOptimalEnd = 0.0f;
		GetOptimalTime(ref otherOptimalStart, ref otherOptimalEnd, other.combatState);

		float optimalStart = 0.0f, optimalEnd = 0.0f;
		GetOptimalTime(ref optimalStart, ref optimalEnd, combatState);

		if (IsHighAttackState(other.combatState) && combatState == CombatState.ParryHigh)
		{
			if (HasParried(optimalStart, optimalEnd, comboTime, otherOptimalStart, otherOptimalEnd, other.comboTime, combatState, 0.0f, other.combatState, 0.0f))
			{
				other.Stagger(1.5f);
			}
		}
		else if (IsLowAttackState(other.combatState) && combatState == CombatState.ParryLow)
		{
			if (HasParried(optimalStart, optimalEnd, comboTime, otherOptimalStart, otherOptimalEnd, other.comboTime, combatState, 0.0f, other.combatState, 0.0f))
			{
				other.Stagger(1.5f);
			}
		}

		if (combatState == CombatState.Blocking)
		{
			if (otherOptimalStart < other.comboTime && other.comboTime < otherOptimalEnd)
			{
				float damage = other.GetDamageForCurrentAttack();
				damage *= 0.2f;
				health -= damage;
				return;
			}
		}
		else
		{
			if (otherOptimalStart < other.comboTime && other.comboTime < otherOptimalEnd)
			{
				float damage = other.GetDamageForCurrentAttack();
				health -= damage;
				return;
			}
		}
	}

	CombatantComponent GetEnemyCurrentlyHitting()
	{
		RaycastHit2D[] rayHits = new RaycastHit2D[12];
		ContactFilter2D contactFilter = new ContactFilter2D();
		int numHits = Physics2D.Raycast(transform.position, transform.right, contactFilter, rayHits, 0.4f);
		Debug.Log("Numhits: " + numHits.ToString());
		if (numHits > 1)
		{
			for (int i = 1; i < numHits; i++)
			{
				var collider = rayHits[i].collider;
				if (collider)
				{
					var obj = collider.gameObject.GetComponent<CombatantComponent>();
					if (obj != null)
					{
						return obj;
					}
				}
			}
		}
		else
		{
			Debug.Log("Didn't hit shit jack");
		}
		return null;
	}

	void Update()
	{
		CombatState cState = combatState;
		comboTimer += Time.deltaTime;
		if (comboTimer >= comboTime)
		{
			// Do nothing for now?
			if (combatCombo.Count == 0)
			{
				cState = CombatState.None;
			}
			else
			{
				// Not ideal to keep a list, but you can't analyze a queue without changing it
				var comboEvent = combatCombo[combatCombo.Count - 1];
				comboTime = comboEvent.comboTime;
				cState = comboEvent.combatState;
				comboTimer = 0.0f;
				combatCombo.RemoveAt(combatCombo.Count - 1);
			}
			combatState = cState;
		}
		else if (comboTimer < comboTime)
		{
			var enemy = GetEnemyCurrentlyHitting();

			if (enemy != null)
			{
				enemy.RecieveInteraction(this);
			}
			else
			{
				Debug.Log("enemy was null");
			}
		}


		if (health <= 0.0f)
		{
			Debug.Log("I are died");
		}
	}
}
