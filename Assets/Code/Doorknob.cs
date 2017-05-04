using UnityEngine;
using System.Collections;

public class Doorknob: MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			GetComponentInParent<DoorBehaviour> ().isReady = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			GetComponentInParent<DoorBehaviour> ().isReady = false;
		}
	}

	void Update(){
		
		if (Input.GetKey (KeyCode.Space)) {
			GetComponentInParent<DoorBehaviour> ().Use ();
		}
	}
}