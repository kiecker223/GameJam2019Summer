using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
	public CombatantComponent combatant;

    void Start()
    {
		combatant = GetComponent<CombatantComponent>();
    }

    void Update()
    {
    }
}
