using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthWorldSpaceUI	: MonoBehaviour
{
	Animator anim;

	public CanvasGroup canvas;
	public bool showUI = true;
	bool show;
	public Slider slider;

	void Start()
	{
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		if(!showUI)
			return;

		if(show	&& canvas.alpha	< 1)
		{
			canvas.alpha += 10 * Time.deltaTime;
		}
	}

	public void	Shake()
	{
		show = true && showUI;

		anim.Play("shake");
	}
}
