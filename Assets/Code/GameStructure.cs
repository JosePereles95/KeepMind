using UnityEngine;
using System.Collections;

public class GameStructure : MonoBehaviour {

	public GameObject desk;
	public GameObject door1;
	public GameObject wardrobe;
	public GameObject notebook;
	
	// Update is called once per frame
	void Update () {
		if (desk.GetComponent<MakeZoom> ().zoomOut) {
			door1.GetComponentInChildren<CapsuleCollider> ().enabled = true;
		}

		if (wardrobe.GetComponent<WardrobeDoorBehaviour> ().open)
			notebook.GetComponent<GrabObject> ().canHold = true;
		else
			notebook.GetComponent<GrabObject> ().canHold = false;
	}
}
