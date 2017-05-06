using UnityEngine;
using System.Collections;

public class GUIDot: MonoBehaviour {

	public Texture2D dotTexture;
	public Rect position;
	static bool OriginalOn = true;

	void Start()
	{
		position = new Rect((Screen.width - dotTexture.width) / 2, (Screen.height - 
			dotTexture.height) /2, dotTexture.width, dotTexture.height);
	}

	void OnGUI()
	{
		if(OriginalOn == true)
		{
			GUI.DrawTexture(position, dotTexture);
		}
	}
}