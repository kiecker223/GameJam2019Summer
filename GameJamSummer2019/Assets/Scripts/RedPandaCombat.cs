using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPandaCombat : MonoBehaviour
{
	public CombatantComponent combatant;

    void Start()
    {
		combatant = GetComponent<CombatantComponent>();
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
