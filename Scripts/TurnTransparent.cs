using UnityEngine;
using System.Collections;

public class TurnTransparent : MonoBehaviour {

	Renderer render;
	public Material materialTransparent;
	private Material materialOriginal;

	void Start()
	{
		render = GetComponent<Renderer>();
		materialOriginal = render.materials[0];
	}

	public void GoTransparent()
	{
		render.material = materialTransparent;
	}

	public void Reappear()
	{
		render.material = materialOriginal;
	}
}
