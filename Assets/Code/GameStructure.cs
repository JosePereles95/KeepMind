using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameStructure : MonoBehaviour {

	[HideInInspector] public GameObject deskMatt;
	[HideInInspector] public GameObject deskLisa;

	[HideInInspector] public GameObject pcMatt;
	[HideInInspector] public GameObject pcLisa;

	[HideInInspector] public GameObject door1;
	[HideInInspector] public GameObject door2;
	[HideInInspector] public GameObject door3;
	[HideInInspector] public GameObject door5;

	[HideInInspector] public GameObject wardrobe;
	[HideInInspector] public GameObject notebook;
	[HideInInspector] public GameObject key;
	[HideInInspector] public GameObject diary;
	[HideInInspector] public GameObject diaryMatt;
	[HideInInspector] public GameObject pills;

	[HideInInspector] public Canvas pcScreenMatt;
	[HideInInspector] public Canvas pcScreenLisa;

	[HideInInspector] public Image[] screensMatt1;
	[HideInInspector] public Image[] screensMatt2;
	[HideInInspector] public Image[] screensMatt3;
	[HideInInspector] public Image[] screensMatt4;

	[HideInInspector] public Image[] screensLisa1;

	[HideInInspector] public GameObject buzz1;
	[HideInInspector] public GameObject buzz2;
	[HideInInspector] public GameObject buzz3;

	[HideInInspector] public Image textMouse;//0
	[HideInInspector] public Image textWasd;//1
	[HideInInspector] public Image textSpace;//2
	[HideInInspector] public Image textLeftClick;//3
	[HideInInspector] public Image textRightClick;//4
	[HideInInspector] public Image textShift;//5
	[HideInInspector] public Image textMessage;//6
	[HideInInspector] public Image textEmily;//7
	[HideInInspector] public Image textLisa;//8
	[HideInInspector] public Image textArrows;//9

	[HideInInspector] public AudioSource audioSource;
	[HideInInspector] public AudioClip clipPop;
	[HideInInspector] public AudioClip clipMessage;

	private bool buzz1Called = false;
	private bool buzz2Called = false;
	private bool buzz3Called = false;

	private bool buzz2Ready = false;
	private bool buzz3Ready = false;

	private bool firstChange = false;
	private bool secondChange = false;
	private bool thirdChange = false;

	private bool keyFound = false;
	private bool diaryFound = false;

	private bool text0 = false;
	private bool text1 = false;
	private bool text2 = false;
	private bool text3 = false;
	private bool text4 = false;
	private bool text5 = false;
	private bool text6 = false;
	private bool text7 = false;
	private bool text8 = false;
	private bool text9 = false;

	private Image[] texts;
	private bool[] boolTexts;

	private bool soundDone = false;

	void Start (){
		pcMatt.GetComponent<PCNavigation> ().screens = screensMatt1;
		door1.GetComponentInChildren<CapsuleCollider> ().enabled = false;
		door2.GetComponentInChildren<CapsuleCollider> ().enabled = false;
		door3.GetComponentInChildren<CapsuleCollider> ().enabled = false;
		door5.GetComponentInChildren<CapsuleCollider> ().enabled = false;
		buzz2.GetComponent<Collider> ().enabled = false;
		pcLisa.GetComponent<PCNavigation>().screens = screensLisa1;
		pills.GetComponent<GrabObject> ().canHold = false;
		texts = new Image[] {textMouse, textWasd, textSpace, textLeftClick,
			textRightClick, textShift, textMessage, textEmily, textLisa, textArrows
		};
		boolTexts = new bool[] {text0, text1, text2, text3, text4,
			text5, text6, text7, text8, text9
		};
	}

	void Update () {
		if (pcMatt.GetComponent<PCNavigation> ().validated) {
			door1.GetComponentInChildren<CapsuleCollider> ().enabled = true;
		}

		if (wardrobe.GetComponent<WardrobeDoorBehaviour> ().open) {
			notebook.GetComponent<GrabObject> ().canHold = true;
		}
		else {
			notebook.GetComponent<GrabObject> ().canHold = false;
		}

		if (deskMatt.GetComponent<MakeZoom> ().lookingPC) {
			pcScreenMatt.enabled = true;
		}
		else {
			pcScreenMatt.enabled = false;
		}

		if (deskLisa.GetComponent<MakeZoom> ().lookingPC) {
			pcScreenLisa.enabled = true;
		}
		else {
			pcScreenLisa.enabled = false;
		}

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

		if (key.GetComponent<GrabObject> ().isHolding) {
			keyFound = true;
		}

		if (keyFound && !key.GetComponent<GrabObject> ().isHolding) {
			key.SetActive (false);
			door2.GetComponentInChildren<CapsuleCollider> ().enabled = true;
		}

		if (!diaryFound && diary.GetComponent<GrabObject> ().isHolding) {
			diaryFound = true;
			boolTexts [6] = false;
			pcMatt.GetComponent<PCNavigation> ().screens = screensMatt3;
		}

		if (deskMatt.GetComponent<MakeZoom> ().lookingPC && diaryFound) {
			buzz2Ready = true;
		}
		
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
			StartCoroutine("End");
		}

		//Texts
		if (!soundDone && !boolTexts [0]) {//Mouse
			ShowText (0);
		}
		
		if (!soundDone && !boolTexts [1] && boolTexts [0]) {//Wasd
			ShowText (1);
		}

		if (!soundDone && !boolTexts [2] && boolTexts [1]) {//Space
			ShowText (2);
		}

		if (!soundDone && !boolTexts [3] && boolTexts [2] &&
			wardrobe.GetComponent<WardrobeDoorBehaviour>().open) {//Left Click
			ShowText (3);
		}

		if (!soundDone && !boolTexts [4] && boolTexts [3] &&
			notebook.GetComponent<GrabObject>().isHolding) {//Right click
			ShowText (4);
		}

		if (!soundDone && !boolTexts [9] && pcMatt.GetComponent<PCNavigation>().validated) {//Arrows
			ShowText (9);
		}

		if (!soundDone && !boolTexts [5] && door1.GetComponent<DoorBehaviour>().open) {//Shift
			ShowText (5);
		}

		if (firstChange) {
			
			if (!soundDone && !boolTexts [6]) {//Message 1
				ShowText (6);
			}
		}

		if (keyFound) {
			if (!soundDone && !boolTexts [7] && boolTexts[6]) {//Emily
				ShowText (7);
			}
		}

		if (diaryFound) {
			if (!soundDone && !boolTexts [6]) {//Message 2
				ShowText (6);
			}
		}

		if (secondChange) {
			if (!soundDone && !boolTexts [6]) {//Message 3
				ShowText (6);
			}

			if (!soundDone && !boolTexts [8] && boolTexts [6]) {//Lisa
				ShowText (8);
			}
		}
	}

	void ShowText(int i){
		if (!soundDone && i == 6) {
			audioSource.clip = clipMessage;
			audioSource.Play ();
			soundDone = true;
		}
		else if(!soundDone && i !=6) {
			audioSource.clip = clipPop;
			audioSource.Play ();
			soundDone = true;
		}

		texts [i].enabled = true;
		StartCoroutine ("HideText", i);
	}

	IEnumerator HideText(int i){
		yield return new WaitForSeconds(4);
		texts [i].enabled = false;
		boolTexts [i] = true;
		soundDone = false;
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