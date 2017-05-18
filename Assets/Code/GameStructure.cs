using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameStructure : MonoBehaviour {

	public GameObject deskMatt;
	public GameObject pcMatt;
	public GameObject door1;
	public GameObject wardrobe;
	public GameObject notebook;
	public Canvas pcScreenMatt;
	public Image[] screensMatt1;
	public Image[] screensMatt2;
	public Image[] screensMatt3;
	public Image[] screensMatt4;
	public bool firstChange = false;

	public GameObject buzz1;
	private bool buzz1Called = false;

	public GameObject key;
	public bool keyFound = false;
	public GameObject door2;

	public GameObject buzz2;
	private bool buzz2Called = false;

	public GameObject diary;
	public bool diaryFound = false;
	public bool buzz2Ready = false;

	public GameObject door3;
	public bool secondChange = false;
	public Canvas pcScreenLisa;
	public GameObject deskLisa;
	public GameObject pcLisa;
	public Image[] screensLisa1;

	public GameObject door5;

	void Start (){
		pcMatt.GetComponent<PCNavigation> ().screens = screensMatt1;
		door1.GetComponentInChildren<CapsuleCollider> ().enabled = false;
		door2.GetComponentInChildren<CapsuleCollider> ().enabled = false;
		door3.GetComponentInChildren<CapsuleCollider> ().enabled = false;
		door5.GetComponentInChildren<CapsuleCollider> ().enabled = false;
		buzz2.GetComponent<Collider> ().enabled = false;
		pcLisa.GetComponent<PCNavigation>().screens = screensLisa1;
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

		if (deskMatt.GetComponent<MakeZoom> ().lookingPC)
			pcScreenMatt.enabled = true;
		else
			pcScreenMatt.enabled = false;

		if (deskLisa.GetComponent<MakeZoom> ().lookingPC)
			pcScreenLisa.enabled = true;
		else
			pcScreenLisa.enabled = false;

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

		if (!diaryFound && diary.GetComponent<GrabObject> ().isHolding) {
			diaryFound = true;
			pcMatt.GetComponent<PCNavigation> ().screens = screensMatt3;
		}

		if (deskMatt.GetComponent<MakeZoom> ().lookingPC && diaryFound)
			buzz2Ready = true;
		
		if (buzz2Ready) {
			buzz2.GetComponent<Collider> ().enabled = true;
			buzz2Ready = false;
		}

		if (!secondChange && buzz2.GetComponent<Collising> ().completed) {
			secondChange = true;
			pcMatt.GetComponent<PCNavigation> ().screens = screensMatt4;
			door3.GetComponentInChildren<CapsuleCollider> ().enabled = true;
			//door3.GetComponent<DoorBehaviour> ().Use ();
		}

		if (pcLisa.GetComponent<PCNavigation> ().validated) {
			door5.GetComponentInChildren<CapsuleCollider> ().enabled = true;
		}
	}
}