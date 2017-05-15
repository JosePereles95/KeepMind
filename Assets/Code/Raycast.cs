using UnityEngine;
using System.Collections;

public class Raycast : MonoBehaviour {

	void Update () {
		Vector3 forward = transform.TransformDirection (Vector3.forward);
		RaycastHit hit;
		if (Physics.Raycast(transform.position, forward, out hit)) {
			if (hit.distance <= 2.0 && hit.collider.gameObject.tag == "Object") {
				
				if (Input.GetMouseButtonDown (0)) {
					hit.collider.gameObject.GetComponent<GrabObject> ().isHolding = true;
				}
			}
			if (hit.distance <= 3.0 && hit.collider.gameObject.tag == "PC") {
				hit.collider.gameObject.GetComponentInParent<MakeZoom> ().raycasting = true;
			}
			else
				GameObject.FindGameObjectWithTag("Desk").GetComponent<MakeZoom> ().raycasting = false;
		}
	}
}