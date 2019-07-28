using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyHitCollider : MonoBehaviour
{
	public CombatantComponent parentCombatant;
	public bool bIsHigh;
	
	void OnTriggerStay2D(Collider2D other)
	{
		Debug.Log("Hit something");
		if (other.tag == "Hitable")
		{
			Debug.Log("Hit hitable");
			if (bIsHigh)
			{
				parentCombatant.highHitObject = other.gameObject;
			}
			else
			{
				parentCombatant.lowHitObject = other.gameObject;
			}
		}
		if (other.tag == "Player")
		{
			parentCombatant.optimalHit = true;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Hit something");
		if (other.tag == "Hitable")
		{
			Debug.Log("Hit hitable");
			if (bIsHigh)
			{
				parentCombatant.highHitObject = other.gameObject;
			}
			else
			{
				parentCombatant.lowHitObject = other.gameObject;
			}
		}
	}
}
