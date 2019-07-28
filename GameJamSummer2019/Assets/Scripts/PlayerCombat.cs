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
        if (InputManager.Instance.GetPrimaryAttackButtonDown_Player1())
		{
			combatant.Punch();
		}
		else if (InputManager.Instance.GetSecondaryButtonDown_Player1())
		{
			combatant.Kick();
		}
		else
		{
			combatant.anim.SetTrigger("None");
		}
    }
}
