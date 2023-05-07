using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orb : MonoBehaviour, IDamageable
{
	public int health;
	public healthWorldSpaceUI healthBar;

	public Renderer	rend;
	public Color damageColorOutside;
	Color originalColorOutside;
	public Color damageColorInside;
	Color originalColorInside;
	public float colorLerpTime;
	bool damaged;

	public float frequency = 10;
	float pi = 3.14f;
	float time;

	 void Start()
	{
		//rend = GetComponent<Renderer>();
		originalColorOutside = rend.material.GetColor("Color_cf12b49411d94583a269f83e6981abd1");
		originalColorInside	 = rend.material.GetColor("Color_027e4586f058443ca29389a6ccbed930");

		healthBar.slider.maxValue =	health;
		healthBar.slider.value = health;
	}
	// Update is called	once per frame
	void Update()
	{
		if(damaged)
		{
			rend.material.SetColor(
				"Color_cf12b49411d94583a269f83e6981abd1",
				Color.Lerp(rend.material.GetColor("Color_cf12b49411d94583a269f83e6981abd1"), damageColorOutside, colorLerpTime)
			);

			rend.material.SetColor(
				"Color_027e4586f058443ca29389a6ccbed930",
				Color.Lerp(rend.material.GetColor("Color_027e4586f058443ca29389a6ccbed930"), damageColorInside,	colorLerpTime)
			);
		}
		else if(rend.material.GetColor("Color_027e4586f058443ca29389a6ccbed930") != originalColorInside)
		{
			rend.material.SetColor(
				"Color_cf12b49411d94583a269f83e6981abd1",
				Color.Lerp(rend.material.GetColor("Color_cf12b49411d94583a269f83e6981abd1"), originalColorOutside, colorLerpTime)
			);

			rend.material.SetColor(
				"Color_027e4586f058443ca29389a6ccbed930",
				Color.Lerp(rend.material.GetColor("Color_027e4586f058443ca29389a6ccbed930"), originalColorInside, colorLerpTime)
			);
		}

		rend.material.SetFloat("_Disolve", pulse() * .37f);
		time += Time.deltaTime;
	}
	public void	TakeDamage(int damage)
	{	
		if(health -	damage <= 0)
		{
			Die();
			return;
		}

		health -= damage;
		healthBar.slider.value = health;
		healthBar.Shake();
		StartCoroutine(FadeColor());
		//anim.Play("hit");
	}

	void Die()
	{
		Destroy(gameObject);
	}

	IEnumerator	FadeColor()
	{
		damaged	= true;
		yield return new WaitForSeconds(.2f);
		damaged	= false;
	}

	float pulse()
	{
		return .5f * (1 + Mathf.Sin(2 * pi * frequency * time));
	}
}
