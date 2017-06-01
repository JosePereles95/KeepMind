using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class Pausing : MonoBehaviour {

	[HideInInspector] public AudioSource audioMusic;

	[HideInInspector] public Image background;

	[HideInInspector] public Button resume;
	[HideInInspector] public Button exit;

	public GameObject deskMatt;
	public GameObject deskLisa;

	private bool paused = false;

	void Start(){
		background.enabled = false;
		resume.gameObject.SetActive (false);
		exit.gameObject.SetActive (false);
	}
		
	void Update () {
		if (!paused && Input.GetKeyDown (KeyCode.Escape)) {
			if (!deskMatt.GetComponent<MakeZoom> ().lookingPC &&
				!deskMatt.GetComponent<MakeZoom> ().zoomIn &&
				!deskMatt.GetComponent<MakeZoom> ().zoomOut &&
				!deskLisa.GetComponent<MakeZoom> ().lookingPC &&
				!deskLisa.GetComponent<MakeZoom> ().zoomIn &&
				!deskLisa.GetComponent<MakeZoom> ().zoomOut) {
				OnPause ();
			}
		}
		else if (paused && Input.GetKeyDown (KeyCode.Escape)) {
			OnUnPause ();
		}

		if (paused) {
			Cursor.visible = true;
		}

		resume.onClick.AddListener(delegate {
			if(paused)
				OnUnPause ();
		});

		exit.onClick.AddListener (delegate {
			if(paused)
				Exit ();
		});
	}

	void OnPause(){
		Cursor.lockState = CursorLockMode.None;
		this.GetComponentInParent<FirstPersonController> ().enabled = false;
		audioMusic.Pause ();
		background.enabled = true;
		resume.gameObject.SetActive (true);
		exit.gameObject.SetActive (true);
		paused = true;
		Time.timeScale = 0;
	}

	void OnUnPause(){
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		Time.timeScale = 1;
		this.GetComponentInParent<FirstPersonController> ().enabled = true;
		audioMusic.UnPause ();
		background.enabled = false;
		resume.gameObject.SetActive (false);
		exit.gameObject.SetActive (false);
		paused = false;
	}

	void Exit(){
		Application.Quit ();
		paused = false;
	}
}
