using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
	public IFollower follower;
	public int uncatchTime = 15;
	void OnTriggerEnter(Collider hit)
	{
		PlayerMove player = hit.GetComponent<PlayerMove>();

		if(player == null)
			return;

		follower.Catch(player);
	}

	void OnTriggerExit(Collider hit)
	{
		if(hit.GetComponent<PlayerMove>() != follower.GetPlayer())
			return;
		
		StartCoroutine(Uncatch());
	}

	IEnumerator Uncatch()
	{
		yield return new WaitForSeconds(uncatchTime);
		follower.Uncatch();
	}
}
