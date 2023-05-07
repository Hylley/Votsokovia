using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthWorldSpaceUI	: MonoBehaviour
{
	Animator anim;

	public CanvasGroup canvas;
	bool show;
	public Slider slider;

	void Start()
	{
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		if(show	&& canvas.alpha	< 1)
		{
			canvas.alpha += 10 * Time.deltaTime;
		}
	}

	public void	Shake()
	{
		show = true;

		anim.Play("shake");
	}
}
