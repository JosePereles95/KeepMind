using UnityEngine;
using System.Collections;

public class Interact: MonoBehaviour {

	bool inside = false;

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			this.transform.parent.SendMessage ("SetReady", true);
			inside = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			this.transform.parent.SendMessage ("SetReady", false);
			inside = false;
		}
	}

	void Update(){

		if (inside && Input.GetKey (KeyCode.Space)) {
			this.transform.parent.SendMessage ("Use");
		}
	}
}