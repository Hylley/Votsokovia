using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbEyes : MonoBehaviour
{
    public orb parent;
    public int insistDuration;
    bool playerInSight;

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
}
