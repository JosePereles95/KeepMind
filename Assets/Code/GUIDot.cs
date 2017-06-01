using UnityEngine;
using System.Collections;

public class GUIDot: MonoBehaviour {

	[HideInInspector] public Texture2D dotTexture;
	[HideInInspector] public GameObject deskMatt;
	[HideInInspector] public GameObject deskLisa;

	private Rect position;
	private static bool OriginalOn = true;

	void Start() {
		position = new Rect((Screen.width - dotTexture.width) / 2, (Screen.height - 
			dotTexture.height) /2, dotTexture.width, dotTexture.height);
	}

	void Update() {
		if (deskMatt.GetComponent<MakeZoom> ().lookingPC ||
			deskLisa.GetComponent<MakeZoom> ().lookingPC) {
			OriginalOn = false;
		}
		else
			OriginalOn = true;
	}

	void OnGUI() {
		if(OriginalOn == true)
		{
			GUI.DrawTexture(position, dotTexture);
		}
	}
}