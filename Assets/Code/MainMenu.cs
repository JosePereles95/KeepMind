using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	[HideInInspector] public Button play;
	[HideInInspector] public Button exit;

	private bool pressed = false;

	void Update () {
	
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;

		play.onClick.AddListener(delegate {
			if(!pressed)
				Play ();
		});

		exit.onClick.AddListener(delegate {
			if(!pressed)
				Exit ();
		});
	}

	void Play(){
		SceneManager.LoadScene("Game");
		pressed = true;
	}

	void Exit(){
		Application.Quit ();
		pressed = true;
	}
}