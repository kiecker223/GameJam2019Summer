using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyHitCollider : MonoBehaviour
{
	public CombatantComponent parentCombatant;
	public bool bIsHigh;
	
	void OnTriggerStay2D(Collider2D other)
	{
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Enemy")
		{

		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
	}
}
