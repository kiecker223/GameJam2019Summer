using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CombatantComponent : MonoBehaviour
{
	public float health;
	public float Damage;

	public Animator anim;
	public CombatantComponent enemy;

	void Start()
	{

	}

	void Attack()
	{

	}

	void DoKick()
	{
		if (enemy)
		{
			enemy.health -= Damage * 1.1f;
		}
	}

	void DoPunch()
	{
		if (enemy)
		{
			enemy.health -= Damage;
		}
	}

	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		if (otherCollider.gameObject.tag == "Enemy")
		{
			enemy = otherCollider.gameObject.GetComponent<CombatantComponent>();
		}
	}

	void OnTriggerExit2D(Collider2D otherCollider)
	{
		if (otherCollider.gameObject.tag == "Enemy")
		{
			enemy = null;
		}
	}

	void Update()
	{
		if (InputManager.Instance.GetPrimaryAttackButtonDown_Player1())
		{
			anim.SetTrigger("Jab");
			DoPunch();
		}
		if (InputManager.Instance.GetSecondaryButtonDown_Player1())
		{
			anim.SetTrigger("Kick");
			DoKick();
		}
	}
}
