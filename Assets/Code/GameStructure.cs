using UnityEngine;
using System.Collections;

public class GameStructure : MonoBehaviour {

	public GameObject desk;
	public GameObject door1;
	
	// Update is called once per frame
	void Update () {
		if (desk.GetComponent<MakeZoom> ().zoomOut == true) {
			door1.GetComponentInChildren<CapsuleCollider> ().enabled = true;
		}
			
	}
}
