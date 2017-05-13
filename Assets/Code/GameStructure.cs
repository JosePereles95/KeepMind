using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameStructure : MonoBehaviour {

	public GameObject desk;
	public GameObject pcMatt;
	public GameObject door1;
	public GameObject wardrobe;
	public GameObject notebook;
	public Canvas pcScreen;
	public Image[] screensMatt1;
	public Image[] screensMatt2;
	public bool firstChange = false;

	public GameObject buzz1;
	private bool buzz1Called = false;

	public GameObject key;
	public bool keyFound = false;
	public GameObject door2;

	public GameObject buzz2;
	private bool buzz2Called = false;

	void Start (){
		pcMatt.GetComponent<PCNavigation> ().screens = screensMatt1;
	}

	// Update is called once per frame
	void Update () {
		if (pcMatt.GetComponent<PCNavigation> ().validated) {
			door1.GetComponentInChildren<CapsuleCollider> ().enabled = true;
		}

		if (wardrobe.GetComponent<WardrobeDoorBehaviour> ().open)
			notebook.GetComponent<GrabObject> ().canHold = true;
		else
			notebook.GetComponent<GrabObject> ().canHold = false;

		if (desk.GetComponent<MakeZoom> ().lookingPC)
			pcScreen.enabled = true;
		else
			pcScreen.enabled = false;

		if (!buzz1Called && buzz1.GetComponent<Collising> ().inside) {
			buzz1Called = true;
			this.GetComponent<Buzz> ().MakeBuzz (1);
		}

		if (!firstChange && buzz1.GetComponent<Collising> ().completed) {
			firstChange = true;
			pcMatt.GetComponent<PCNavigation> ().screens = screensMatt2;
		}

		if (!buzz2Called && buzz2.GetComponent<Collising> ().inside) {
			buzz2Called = true;
			this.GetComponent<Buzz> ().MakeBuzz (2);
		}

		if (key.GetComponent<GrabObject> ().isHolding)
			keyFound = true;
		if (keyFound && !key.GetComponent<GrabObject> ().isHolding) {
			key.SetActive (false);
			door2.GetComponentInChildren<CapsuleCollider> ().enabled = true;
		}
	}
}
