using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

	public string fade = "";

	public Texture2D fadeTexture;
	public float fadeSpeed = 0.6f;
	public int drawDepth = -1000;
	public bool fadeFinished = false;
	public bool allFinished = false;

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

		if (alpha == 0.0f && fade == "IN")
			allFinished = true;
	}

	public void FadeIn(){
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