using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PCNavigation : MonoBehaviour {

	[HideInInspector] public GameObject desk;
	[HideInInspector] public InputField usernameField;
	[HideInInspector] public InputField passwordField;
	[HideInInspector] public Image background;
	[HideInInspector] public Image[] screens;
	[HideInInspector] public AudioClip arrowClip;

	public string username;
	public string password;
	public bool validated = false;

	private bool credentialsChecked = false;
	private bool reset = false;
	private bool cleaned = true;
	private bool loggedIn = false;

	private int i = 0;

	void Update () {
		if (desk.GetComponent<MakeZoom> ().lookingPC) {
			if(!reset)
				Reset ();
			Cursor.lockState = CursorLockMode.None;
			if (!validated) {
				Cursor.visible = true;
				background.enabled = true;
				usernameField.enabled = true;
				usernameField.image.enabled = true;
				passwordField.enabled = true;
				passwordField.image.enabled = true;
			}
			else {
				Cursor.visible = false;
				screens [i].enabled = true;
			}

			if (Input.GetKeyDown (KeyCode.Return)) {
				credentialsChecked = false;
			}

			passwordField.onEndEdit.AddListener (delegate {
				if (!credentialsChecked)	
					CheckCredentials ();
			});

			if (validated) {
				if (!loggedIn) {
					loggedIn = true;
					LogIn ();
					this.GetComponent<AudioSource> ().Play ();
				}

				if(Input.GetKeyDown(KeyCode.RightArrow)){
					this.GetComponent<AudioSource> ().clip = arrowClip;
					this.GetComponent<AudioSource> ().Play ();
					ChangeInfo ("Right");
				}
				if(Input.GetKeyDown(KeyCode.LeftArrow)){
					this.GetComponent<AudioSource> ().clip = arrowClip;
					this.GetComponent<AudioSource> ().Play ();
					ChangeInfo ("Left");
				}
			}
		}
		else if (!cleaned){
			Clean ();
		}
			

	}

	void CheckCredentials(){
		if (usernameField.text == username) {
			if (passwordField.text == password) {
				validated = true;
			}
			else {
				usernameField.text = "";
				passwordField.text = "";
			}
		}
		else {
			usernameField.text = "";
			passwordField.text = "";
		}
		credentialsChecked = true;
	}

	void LogIn(){
		background.enabled = false;
		usernameField.enabled = false;
		usernameField.image.enabled = false;
		passwordField.enabled = false;
		passwordField.image.enabled = false;
		loggedIn = true;
		screens [i].enabled = true;
	}

	void ChangeInfo(string next){
		int length = screens.Length;

		screens [i].enabled = false;

		if (next == "Right") {
			if (i >= length - 1)
				i = 0;
			else
				i++;
		}
		else if (next == "Left") {
			if (i <= 0)
				i = length - 1;
			else
				i--;
		}

		screens [i].enabled = true;
	}

	void Reset(){
		usernameField.text = "";
		passwordField.text = "";
		reset = true;
		cleaned = false;
	}

	void Clean(){
		background.enabled = false;
		screens [i].enabled = false;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		cleaned = true;
		reset = false;
	}
}