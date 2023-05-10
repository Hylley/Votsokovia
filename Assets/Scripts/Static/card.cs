using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card :	MonoBehaviour
{
	public card match;
	public static card selectedCard;
	public GameObject outline;
	bool over = false;

	LineRenderer line;
	bool drawing = false;

	void Update()
	{
		if(over && Input.GetMouseButtonDown(0))
		{
			if(selectedCard)
			{
				if(selectedCard.match != this)
				{
					selectedCard.UnMactchCard();

					selectedCard = null;
					return;
				}

				MatchCard(selectedCard);

				selectedCard = null;
				return;
			}

			selectedCard = this;
			DrawLine();
		}

		if(drawing && selectedCard == this)
		{
			Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			worldPosition.z = Camera.main.nearClipPlane;
			line.SetPosition(1, worldPosition);	
		}
	}

	void DrawLine()
	{
		line = gameObject.AddComponent(typeof(LineRenderer)) as LineRenderer;
		line.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
		line.SetPosition(0, transform.position + new Vector3(0, .945f, 0));
		line.SetPosition(1, transform.position + new Vector3(0, .945f, 0));
		drawing = true;
	}

	void MatchCard(card _match)
	{

	}

	public void UnMactchCard()
	{
		Destroy(line);
	}

	void OnMouseOver()
	{
		over = true;
		outline.SetActive(over);
	}

	void OnMouseExit()
	{
		over = false;
		outline.SetActive(over);
	}
}
