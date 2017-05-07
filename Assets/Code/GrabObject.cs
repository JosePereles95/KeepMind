using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class GrabObject: MonoBehaviour {

	public GameObject player;
	public Transform SpawnTo;
	public bool canHold = true;
	public bool isHolding = false;
	private Vector3 defaultPos;
	private Quaternion defaultRot;

	void Start() {
		defaultPos = this.transform.position;
		defaultRot = this.transform.rotation;
	}

	void Update () {

		if (isHolding) {
			this.transform.position = SpawnTo.transform.position;
			player.GetComponent<FirstPersonController> ().enabled = false;

			if (Input.GetMouseButtonDown (1)) {
				isHolding = false;
				player.GetComponent<FirstPersonController> ().enabled = true;
			}
		}
		else {
			this.transform.position = defaultPos;
			this.transform.rotation = defaultRot;
		}
	}
}