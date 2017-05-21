using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PCNavigation : MonoBehaviour {

	public GameObject desk;
	public InputField usernameField;
	public InputField passwordField;
	public Image background;
	public Image[] screens;
	public string username;
	public string password;

	public bool validated = false;
	private bool credentialsChecked = false;
	private bool reset = false;
	private bool cleaned = true;
	private bool loggedIn = false;

	public int i = 0;

	void Update () {
		if (desk.GetComponent<MakeZoom> ().lookingPC) {
			if(!reset)
				Reset ();
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			if (!validated) {
				background.enabled = true;
				usernameField.enabled = true;
				usernameField.image.enabled = true;
				passwordField.enabled = true;
				passwordField.image.enabled = true;
			}
			else
				screens [i].enabled = true;
		}
		else if (!cleaned){
			Clean ();
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
			}

			if(Input.GetKeyDown(KeyCode.RightArrow)){
				ChangeInfo ("Right");
			}
			if(Input.GetKeyDown(KeyCode.LeftArrow)){
				ChangeInfo ("Left");
			}
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
		Cursor.visible = false;
		cleaned = true;
		reset = false;
	}
}