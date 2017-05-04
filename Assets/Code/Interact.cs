using UnityEngine;
using System.Collections;

public class Interact: MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			this.transform.parent.SendMessage ("SetReady", true);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			this.transform.parent.SendMessage ("SetReady", false);
		}
	}

	void Update(){

		if (Input.GetKey (KeyCode.Space)) {
			this.transform.parent.SendMessage ("Use");
		}
	}
}