using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Buzz : MonoBehaviour {

	[HideInInspector] public GameObject player;
	[HideInInspector] public GameObject fade;

	[HideInInspector] public GameObject recol1;
	[HideInInspector] public GameObject recol2;
	[HideInInspector] public GameObject recol3;

	[HideInInspector] public GameObject buzz1;
	[HideInInspector] public GameObject buzz2;
	[HideInInspector] public GameObject buzz3;

	private int phase;

	private bool raising = false;

	private float min = 0.0f;
	private float max = 0.4f;
	private static float t = 0.0f;

	private bool changed = false;

	void Start () {
		this.GetComponent<AudioSource> ().volume = min;
	}

	void Update () {
		if (raising) {
			this.GetComponent<AudioSource> ().volume = Mathf.Lerp (min, max, t);
			t += 0.5f * Time.deltaTime;
		}

		if (!changed && this.GetComponent<AudioSource> ().volume >= max) {
			changed = true;
			StartCoroutine ("Decreasing");
		}

		if (fade.GetComponent<Fade> ().fadeFinished) {
			Recolocate ();
		}

		if (fade.GetComponent<Fade> ().allFinished) {
			Restart ();
		}
	}

	public void MakeBuzz(int p){
		phase = p;

		player.GetComponent<FirstPersonController> ().enabled = false;

		this.GetComponent<AudioSource> ().Play ();
		raising = true;

		fade.GetComponent<Fade> ().FadeOut ();
	}

	void Recolocate (){
		if (phase == 1) {	
			player.transform.position = recol1.transform.position;
		}

		if (phase == 2) {
			player.transform.position = recol2.transform.position;
		}

		if (phase == 3) {
			player.transform.position = recol3.transform.position;
		}
	}

	void Restart (){

		fade.GetComponent<Fade> ().fadeFinished = false;
		fade.GetComponent<Fade> ().allFinished = false;
		fade.GetComponent<Fade> ().fade = "";

		min = 0.0f;
		max = 0.4f;
		t = 0.0f;

		changed = false;
		raising = false;

		this.GetComponent<AudioSource> ().volume = 0.0f;
		player.GetComponent<FirstPersonController> ().enabled = true;

		if (phase == 1) {
			buzz1.GetComponent<Collider> ().enabled = false;
			buzz1.GetComponent<Collising> ().inside = false;
			buzz1.GetComponent<Collising> ().completed = true;
		}

		if (phase == 2) {
			buzz2.GetComponent<Collider> ().enabled = false;
			buzz2.GetComponent<Collising> ().inside = false;
			buzz2.GetComponent<Collising> ().completed = true;
		}

		if (phase == 3) {
			buzz3.GetComponent<Collider> ().enabled = false;
			buzz3.GetComponent<Collising> ().inside = false;
			buzz3.GetComponent<Collising> ().completed = true;
		}
	}

	IEnumerator Decreasing(){
		yield return new WaitForSeconds(1);
		float temp = max;
		max = min;
		min = temp;
		t = 0.0f;
	}
}