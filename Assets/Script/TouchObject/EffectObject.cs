using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObject : MonoBehaviour
{
	SpriteRenderer sp;
	void Start()
	{
		sp = GetComponent<SpriteRenderer>();
		zoom = true;

	}
	bool increase = false;
	bool zoom;
	void Update()
	{
		if (zoom)
		{
			if (transform.localScale.x > 2)
			{
				sp.color -= 5 * Color.black * Time.deltaTime;
				if (sp.color.a < 0)
				{
					zoom = false;
					transform.localScale = new Vector3(1, 1, 1);
				}
			}
			else
				transform.localScale += new Vector3(3 * Time.deltaTime, 3 * Time.deltaTime, 0);

		}
		else
		{
			if (increase)
			{
				sp.color += Color.black * Time.deltaTime;
				if (sp.color.a >= 1)
					increase = false;
			}
			else
			{
				sp.color -= Color.black * Time.deltaTime;
				if (sp.color.a <= 0.5f)
					increase = true;
			}
		}

	}
}
