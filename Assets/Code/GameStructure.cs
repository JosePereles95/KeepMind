using UnityEngine;
using UnityEngine.SceneManagement;
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
	public GameObject diaryMatt;
	public GameObject pills;

	public GameObject buzz3;
	private bool buzz3Called = false;
	public bool buzz3Ready = false;
	public bool thirdChange = false;

	public Image textMouse;//0
	private bool text0 = false;
	public Image textWasd;//1
	private bool text1 = false;
	public Image textSpace;//2
	private bool text2 = false;
	public Image textRightClick;//3
	private bool text3 = false;
	public Image textLeftClick;//4
	private bool text4 = false;
	public Image textShift;//5
	private bool text5 = false;
	public Image textMessage;//6
	private bool text6 = false;
	public Image textEmily;//7
	private bool text7 = false;
	public Image textLisa;//8
	private bool text8 = false;

	private Image[] texts;
	private bool[] boolTexts;

	void Start (){
		pcMatt.GetComponent<PCNavigation> ().screens = screensMatt1;
		door1.GetComponentInChildren<CapsuleCollider> ().enabled = false;
		door2.GetComponentInChildren<CapsuleCollider> ().enabled = false;
		door3.GetComponentInChildren<CapsuleCollider> ().enabled = false;
		door5.GetComponentInChildren<CapsuleCollider> ().enabled = false;
		buzz2.GetComponent<Collider> ().enabled = false;
		pcLisa.GetComponent<PCNavigation>().screens = screensLisa1;
		pills.GetComponent<GrabObject> ().canHold = false;
		texts = new Image[] {textMouse, textWasd, textSpace, textRightClick,
			textLeftClick, textShift, textMessage, textEmily, textLisa
		};
		boolTexts = new bool[] {text0, text1, text2, text3, text4,
			text5, text6, text7, text8
		};
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
			boolTexts [6] = false;
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
			boolTexts [6] = false;
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
			boolTexts [6] = false;
			pcMatt.GetComponent<PCNavigation> ().screens = screensMatt4;
			door3.GetComponentInChildren<CapsuleCollider> ().enabled = true;
			door3.GetComponent<DoorBehaviour> ().Use ();
		}

		if (pcLisa.GetComponent<PCNavigation> ().validated) {
			door5.GetComponentInChildren<CapsuleCollider> ().enabled = true;
		}

		if (diaryMatt.GetComponent<GrabObject> ().isHolding) {
			pills.GetComponent<GrabObject> ().canHold = true;
		}

		if (pills.GetComponent<GrabObject> ().isHolding) {
			buzz3Ready = true;
		}

		if (buzz3Ready) {
			buzz3.GetComponent<Collider> ().enabled = true;
			buzz3Ready = false;
		}

		if (!buzz3Called && buzz3.GetComponent<Collising> ().inside) {
			buzz3Called = true;
			this.GetComponent<Buzz> ().MakeBuzz (3);
			StartCoroutine("Pills");
		}

		if (!thirdChange && buzz3.GetComponent<Collising> ().completed) {
			thirdChange = true;
			//End
			StartCoroutine("End");
		}

		//Texts
		if (!boolTexts [0]) {//Mouse
			ShowText (0);
		}
		
		if (!boolTexts [1] && boolTexts [0]) {//Wasd
			ShowText (1);
		}

		if (!boolTexts [2] && boolTexts [1]) {//Space
			ShowText (2);
		}

		if (!boolTexts [3] && boolTexts [2] &&
			wardrobe.GetComponent<WardrobeDoorBehaviour>().open) {//Right Click
			ShowText (3);
		}

		if (!boolTexts [4] && boolTexts [3] &&
			notebook.GetComponent<GrabObject>().isHolding) {//Left click
			ShowText (4);
		}

		if (!boolTexts [5] && door1.GetComponent<DoorBehaviour>().open) {//Shift
			ShowText (5);
		}

		if (firstChange) {
			
			if (!boolTexts [6]) {//Message 1
				ShowText (6);
			}
		}

		if (keyFound) {
			if (!boolTexts [7] && boolTexts[6]) {//Emily
				ShowText (7);
			}
		}

		if (diaryFound) {
			if (!boolTexts [6]) {//Message 2
				ShowText (6);
			}
		}

		if (secondChange) {
			if (!boolTexts [6]) {//Message 3
				ShowText (6);
			}

			if (!boolTexts [8] && boolTexts [6]) {//Lisa
				ShowText (8);
			}
		}
	}

	void ShowText(int i){
		texts [i].enabled = true;
		StartCoroutine ("HideText", i);
	}

	IEnumerator HideText(int i){
		yield return new WaitForSeconds(4);
		texts [i].enabled = false;
		boolTexts [i] = true;
	}

	IEnumerator End(){
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene("End");
	}

	IEnumerator Pills(){
		yield return new WaitForSeconds(1);
		pills.GetComponent<GrabObject> ().isHolding = false;
		pills.SetActive (false);
	}

}