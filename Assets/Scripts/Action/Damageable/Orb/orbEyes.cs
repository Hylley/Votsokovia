using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbEyes : MonoBehaviour
{
    public orb parent;
    public int insistDuration;
    public float dealyBetweenAttacks;
    bool playerInSight;
    bool canAttack = true;

    void Update()
    {
        if(playerInSight && parent.follow && canAttack)
        {
            GameStatus.instance.TakeDamage(parent.baseDamage);
            StartCoroutine(Wait());
        }
    }

    void OnTriggerEnter(Collider hit)
	{
		if(!hit.gameObject.CompareTag("Player"))
			return;
		
		parent.follow = hit.transform;
        playerInSight = true;
	}

	void OnTriggerExit(Collider hit)
	{
		if(!hit.gameObject.CompareTag("Player"))
			return;
		
        playerInSight = false;
		StartCoroutine(StopFollowing());
	}

    IEnumerator StopFollowing()
    {
        yield return new WaitForSeconds(insistDuration);

        if(playerInSight)
            yield break;

        parent.follow = null;
    }

    IEnumerator Wait()
    {
        canAttack = false;
        yield return new WaitForSeconds(dealyBetweenAttacks);
        canAttack = true;
    }
}
