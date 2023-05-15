using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nick :	MonoBehaviour, IDamageable,	IFollower
{
	Rigidbody rb;

	public int health;
	public healthWorldSpaceUI healthBar;
	public float speed;
	public int hitDamage = 1;
	public float hitDelay = 1;
	bool canAttack = true;
	bool playerColliding;
	public float knockbackForce;
	public trigger triggerScript;
	public int maxRandomMoveRange;
	Vector3 randomMovePosition;
	public GameObject prefab;
	public AudioSource musicPlayer;
	public AudioSource supportPlayer;
	
	PlayerMove target;

	void Start()
	{
		triggerScript.follower = this;
		rb = GetComponent<Rigidbody>();
		healthBar.slider.maxValue =	health;
		healthBar.slider.value = health;

		StartCoroutine(generateRandomWalkPosition());
	}

	void Update()
	{
		if(GameStatus.pause)
		{
			musicPlayer.Pause();
			supportPlayer.Pause();
		}
	}

	void FixedUpdate()
	{
		if(target == null)
		{
			rb.MovePosition(Vector3.MoveTowards(transform.position, randomMovePosition, speed));
			
			return;
		}
		
		rb.MovePosition(Vector3.MoveTowards(transform.position, target.transform.position, speed));
	}

	void OnCollisionEnter(Collision hit) { playerColliding = hit.gameObject.GetComponent<PlayerMove>() != null; }
	void OnCollisionExit(Collision hit) { playerColliding = hit.gameObject.GetComponent<PlayerMove>() != null; }

	void OnCollisionStay(Collision hit)
	{
		if(!playerColliding || target == null || !canAttack)
			return;
		
		GameStatus.instance.TakeDamage(hitDamage);
		StartCoroutine(Wait());
	}

	IEnumerator Wait()
	{
		canAttack = false;
		yield return new WaitForSeconds(hitDelay);
		canAttack = true;
	}

	public void	Catch(PlayerMove player)
	{
		target = player;
		Debug.Log("Catch: " + target);
	}

	public PlayerMove GetPlayer()
	{
		return target;
	}
	
	public void	Uncatch()
	{
		Debug.Log("Uncatch: " + target);
		target = null;
	}

	public void	TakeDamage(int damage)
	{	
		if(health -	damage <= 0)
		{
			Die();
			return;
		}

		health -= damage;
		
		rb.AddForce(transform.forward * knockbackForce, ForceMode.Impulse);

		healthBar.slider.value = health;
		healthBar.Shake();
		//anim.Play("hit");
	}

	void Die()
	{
		Instantiate(prefab,
			new Vector3(Random.Range(-maxRandomMoveRange, maxRandomMoveRange), transform.position.y, Random.Range(-maxRandomMoveRange, maxRandomMoveRange)),
			Quaternion.identity
		);

		Instantiate(prefab,
			new Vector3(Random.Range(-maxRandomMoveRange, maxRandomMoveRange), transform.position.y, Random.Range(-maxRandomMoveRange, maxRandomMoveRange)),
			Quaternion.identity
		);

		Destroy(gameObject);
	}

	IEnumerator generateRandomWalkPosition()
	{
		while(true)
		{
			randomMovePosition = new Vector3(Random.Range(-maxRandomMoveRange, maxRandomMoveRange), transform.position.y, Random.Range(-maxRandomMoveRange, maxRandomMoveRange));

			yield return new WaitForSeconds(5);
		}
	}
}
