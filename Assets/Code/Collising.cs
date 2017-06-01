using UnityEngine;
using System.Collections;

public class Collising : MonoBehaviour {

	[HideInInspector] public bool inside = false;
	[HideInInspector] public bool completed = false;

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			inside = true;
		}
	}
}