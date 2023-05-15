using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameStatus	: MonoBehaviour
{
	public static bool pause = false;
	public int health =	100;
	public static GameStatus instance {	get; private set; }
	int	maxHealth;
	public int sickles;
	public int rocks;
	public float distance;
	[Space(7)]
	public PlayerMove playerMove;
	public PlayerLook playerLook;
	
	public TextMeshProUGUI fragmentsTextUI;
	public GameObject gameOverScreen;
	public AudioSource deathSound;
	public AudioSource backgroundMusic;

	public AudioSource altarSoundEffect;
	public List<Sprite> altarsSprites;
	public Image iconPlaceholder;
	public int altar = 0;
	public RawImage blackScreen;
	// public Image screenDamage;
	void Start()
	{
		if(instance	!= null)
		{
			Destroy(this);
			return;			
		}

		instance = this;
		//DontDestroyOnLoad(gameObject);
		maxHealth =	health;

		TakeDamage(0);
		//DontDestroyOnLoad(UI);
	}
	
	void Update()
	{
		fragmentsTextUI.text = rocks.ToString();
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
		Debug.Log("Player health: " + health);

		// // Change UI

		// Color newColor = screenDamage.color;
		// newColor.a = 1 - (float)health/(float)maxHealth;
		// screenDamage.color = newColor;
	}

	void GameOver()
	{
		pause = true;
		backgroundMusic.Pause();
		deathSound.Play();
		gameOverScreen.SetActive(true);

		StartCoroutine(countQuit());
	}

	IEnumerator countQuit()
	{
		yield return new WaitForSeconds(3);
		#if UNITY_STANDALONE
			Application.Quit();
		#endif
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}

	public void clearAltar()
	{
		Debug.Log("Cleared");
		altar++;
		altarSoundEffect.Play();	
		iconPlaceholder.sprite = altarsSprites[altar];

		if(altar == 4)
			StartCoroutine(Win());
	}

	IEnumerator Win()
	{
		Color newColor = blackScreen.color;
		while(blackScreen.color.a < 1)
		{
			newColor.a += Time.deltaTime / 10;
			blackScreen.color = newColor;

			yield return null;
		}

		SceneManager.LoadScene(2, LoadSceneMode.Single);
	}
}
