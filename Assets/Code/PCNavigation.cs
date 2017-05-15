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

	public int i = 0;
	
	// Update is called once per frame
	void Update () {
		if (desk.GetComponent<MakeZoom> ().lookingPC) {
			if(!reset)
				Reset ();
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			background.enabled = true;
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
			LogIn ();

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
		passwordField.enabled = false;

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
		Debug.Log ("mostrando: " + i + " de " + length);
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