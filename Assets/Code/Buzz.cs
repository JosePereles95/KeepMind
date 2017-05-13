using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Buzz : MonoBehaviour {

	public GameObject player;
	public GameObject fade;
	private bool raising = false;
	private int phase;

	public GameObject recol1;
	public GameObject recol2;

	public GameObject buzz1;
	public GameObject buzz2;

	private float min = 0.0f;
	private float max = 0.4f;
	static float t = 0.0f;
	private bool changed = false;

	// Use this for initialization
	void Start () {
		this.GetComponent<AudioSource> ().volume = min;
	}
	
	// Update is called once per frame
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
			Debug.Log ("Recolocate 1");		
			player.transform.position = recol1.transform.position;
		}
		if (phase == 2) {
			Debug.Log ("Recolocate 2");	
			player.transform.position = recol2.transform.position;
		}
	}

	void Restart (){

		fade.GetComponent<Fade> ().fadeFinished = false;
		fade.GetComponent<Fade> ().allFinished = false;
		fade.GetComponent<Fade> ().fade = "";
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
	}

	IEnumerator Decreasing(){
		yield return new WaitForSeconds(1);
		float temp = max;
		max = min;
		min = temp;
		t = 0.0f;
	}
}