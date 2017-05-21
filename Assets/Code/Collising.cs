using UnityEngine;
using System.Collections;

public class Collising : MonoBehaviour {

	public bool inside = false;
	public bool completed = false;

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			inside = true;
		}
	}
}