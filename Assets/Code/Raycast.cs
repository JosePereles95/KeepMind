using UnityEngine;
using System.Collections;

public class Raycast : MonoBehaviour {

	private bool showText = false;
	private bool holding = false;
	private string nameObject = "";

	void Update () {
		Vector3 forward = transform.TransformDirection (Vector3.forward);
		RaycastHit hit;
		if (Physics.Raycast(transform.position, forward, out hit)) {
			if (hit.distance <= 2.0 && hit.collider.gameObject.tag == "Object" &&
			    hit.collider.gameObject.GetComponent<GrabObject> ().canHold) {

				showText = true;
				holding = hit.collider.gameObject.GetComponent<GrabObject> ().isHolding;
				nameObject = hit.collider.gameObject.name;

				if (Input.GetMouseButtonDown (0)) {
					hit.collider.gameObject.GetComponent<GrabObject> ().isHolding = true;
					this.GetComponentInChildren<AudioSource> ().Play ();
				}
			}
			else {
				showText = false;
				nameObject = "";
			}
			
			if (hit.distance <= 3.0 && hit.collider.gameObject.tag == "PC") {
				hit.collider.gameObject.GetComponentInParent<MakeZoom> ().raycasting = true;
			}
			else
				GameObject.FindGameObjectWithTag("Desk").GetComponent<MakeZoom> ().raycasting = false;
		}
	}

	void OnGUI(){
		if (showText && !holding) {
			GUI.Box (new Rect (Screen.width / 2 - 75, Screen.height / 2 + 75, 150, 30), nameObject);
		}
	}
}