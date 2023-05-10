using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class introductionHolder	: MonoBehaviour
{
	int stage = 0;
	delegate void callback();

	[Header("Starter dialog")]
	public dialogData initialDialogs;
	public TMPro.TextMeshProUGUI displayInput;
	public float timeBetweenCharacters = .125f;
	public float forwardTimeBetweenCharacters = .01f;
	float defaultTimeBetweenCharacters;
	public float timeBetweenDialogs = 2;
	public float forwardTimeBetweenDialogs = 1;
	float defaultTimeBetweenDialogs;
	public KeyCode forwardKey;
	bool finished;

	[Header("Planet name")]
	public GameObject planetNameInput;

	[Header("Advanged dialog")]
	public dialogData advancedDialog;

	void Start()
	{
		defaultTimeBetweenCharacters = timeBetweenCharacters;
		defaultTimeBetweenDialogs = timeBetweenDialogs;

		stage = 1;
		StartCoroutine(Type(initialDialogs.dialogs, PlanetName));
	}

	void Update()
	{
		if(Input.GetKeyDown(forwardKey))
		{
			timeBetweenCharacters = forwardTimeBetweenCharacters;
			timeBetweenDialogs = forwardTimeBetweenDialogs;
		}else if(Input.GetKeyUp(forwardKey))
		{
			timeBetweenCharacters = defaultTimeBetweenCharacters;
			timeBetweenDialogs = defaultTimeBetweenDialogs;
		}

		if(stage == 2 && Input.GetKeyDown(KeyCode.Return))
		{
			AdvanceDialog();
		}
	}

	IEnumerator	Type(List<string> texts, callback _callback)
	{

		foreach(string text in texts)
		{
			displayInput.text = "";

			foreach(char c in text)	
			{
				displayInput.text += c;
				yield return new WaitForSeconds(timeBetweenCharacters);
			}

			yield return new WaitForSeconds(timeBetweenDialogs);
		}

		_callback();
	}

	public void PlanetName()
	{
		stage = 2;
		displayInput.enabled = false;
		planetNameInput.SetActive(true);
	}

	public void AdvanceDialog()
	{
		stage = 3;
		displayInput.enabled = true;
		planetNameInput.SetActive(false);
		StartCoroutine(Type(advancedDialog.dialogs, Finish));
	}

	public void Finish()
	{
		Debug.Log("Finished");
	}
}
