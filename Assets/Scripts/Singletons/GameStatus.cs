using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatus	: MonoBehaviour
{
	public int health =	100;
	public static GameStatus instance {	get; private set; }
	int	maxHealth;
	public int sickles;
	public int jellies;
	public float distance;
	[Space(7)]
	public PlayerMove playerMove;
	public PlayerLook playerLook;
	public Image screenDamage;
	void Start()
	{
		if(instance	!= null)
		{
			Destroy(this);
			return;			
		}

		instance = this;
		DontDestroyOnLoad(gameObject);
		maxHealth =	health;

		TakeDamage(0);
		//DontDestroyOnLoad(UI);
	}

	public void	TakeDamage(int damage)
	{
		if(health -	damage <= 0)
		{
			GameOver();
			return;
		}

		health -= damage;
		playerLook.shakeAmount = .5f;
		playerLook.shakeDuration = .3f;

		// Change UI

		Color newColor = screenDamage.color;
		newColor.a = 1 - (float)health/(float)maxHealth;
		screenDamage.color = newColor;
	}

	void GameOver()
	{
		GameStatus.instance.jellies++;
		Debug.Log("Game	over");
	}
}
