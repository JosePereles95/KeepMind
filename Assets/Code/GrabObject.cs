using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class GrabObject: MonoBehaviour {

	public GameObject player;
	public Transform SpawnTo;
	public bool canHold = true;
	private bool isHolding = false;
	private Vector3 defaultPos;
	private int dist = 2;

	void Start() {
		defaultPos = this.transform.position;
	}

	void Update () {

		if(Input.GetMouseButtonDown(0)){
			if(canHold && Vector3.Distance(player.transform.position, this.transform.position) < dist) {
				isHolding = true;
				player.GetComponent<FirstPersonController> ().enabled = false;
			}
		}

		if (isHolding) {
			this.transform.position = SpawnTo.transform.position;


			if(Input.GetMouseButtonDown(1)){
				isHolding = false;
				player.GetComponent<FirstPersonController> ().enabled = true;
			}
		}
		else
			this.transform.position = defaultPos;
	}
}