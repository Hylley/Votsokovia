using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sickle	: MonoBehaviour
{
	public bool	equiped;
	public PlayerLook playerVision;
	public LayerMask attackLayer;
	bool attacking = false;
	bool second_slice = false;
	public int baseDamage =	10;
	public float durability	= 10;
	float originalDurability;
	// public PlayerLook playerVision;

	Animator anim;

	void Start()
	{
		anim = GetComponent<Animator>();
		originalDurability = durability;
	}

	void Update()
	{
		if(equiped && !attacking && Input.GetMouseButton(0))
		{
			RaycastHit hit;
			if(Physics.Raycast(playerVision.transform.position, playerVision.transform.forward, out hit, playerVision.hitDistance, attackLayer))
			{
				StartCoroutine(Attack());
				Debug.Log("Hit: " + hit.transform);

				IDamageable	damageableObject = hit.transform.GetComponent<IDamageable>();
				if(damageableObject	== null)
					return;

				damageableObject.TakeDamage(baseDamage);

				if(durability - 1 <= 0)
				{
					Broke();
					return;					
				}
				durability--;
			}

		}
	}

	IEnumerator	Attack()
	{
		attacking =	true;

		if(second_slice)
		{
			anim.Play("attack_2");
		}
		else
		{
			anim.Play("attack_1");
		}

		playerVision.shakeDuration = .3f;
		yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length * .7f);

		attacking =	false;
		second_slice = !second_slice;
	}

	void Broke()
	{
		if(GameStatus.instance.sickles - 1 <= 0)
		{
			Destroy(gameObject);
			return;
		}

		GameStatus.instance.sickles--;
		durability = originalDurability;
	}
}
