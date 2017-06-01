using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

	[HideInInspector] public string fade = "";

	[HideInInspector] public bool fadeFinished = false;
	[HideInInspector] public bool allFinished = false;

	[HideInInspector] public Texture2D fadeTexture;

	private float fadeSpeed = 0.6f;
	private int drawDepth = -1000;
	private float alpha = 0.0f; 
	private int fadeDir = -1;

	void OnGUI(){
		if (fade == "OUT") {
			alpha -= fadeDir * fadeSpeed * Time.deltaTime;
			alpha = Mathf.Clamp01 (alpha);
			Color color = new Color (255, 255, 255, alpha);
			GUI.color = color;
			GUI.depth = drawDepth;
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeTexture);
		}

		if (fade == "IN") {
			alpha += fadeDir * fadeSpeed * Time.deltaTime;  
			alpha = Mathf.Clamp01 (alpha);   
			Color color = new Color (255, 255, 255, alpha);
			GUI.color = color;
			GUI.depth = drawDepth;
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeTexture);
		}

		if (alpha == 1.0f && fade == "OUT") {
			fadeFinished = true;
			StartCoroutine ("FadeToIn");
		}

		if (alpha == 0.0f && fade == "IN") {
			allFinished = true;
		}
	}

	void FadeIn(){
		fade = "IN";
	}

	public void FadeOut(){
		allFinished = false;
		fadeFinished = false;
		fade = "OUT";
	}

	IEnumerator FadeToIn(){
		yield return new WaitForSeconds(1);
		FadeIn ();
	}
}