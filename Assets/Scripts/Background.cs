using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
	public float speed = 0.1f;          //Speed of the scrolling

	private Renderer rend;

	void Start()
	{
		rend = GetComponent<Renderer>();
	}

	void Update()
	{
		//Keep looping between 0 and 1 => t % 1.0f
		float x = Mathf.Repeat(Time.time * speed, 1);

		//Create the offset
		Vector2 offset = new Vector2(x, 0);
		//Apply the offset to the material
		//		GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", offset);

		rend.material.mainTextureOffset = offset;

		//		float sx = Mathf.Repeat(Time.time * speed, 1.0f);
		//		float sy = Mathf.Repeat(Time.time * speed, 1.0f);
		//		rend.material.mainTextureScale = new Vector2(sx, sy);
	}
}
