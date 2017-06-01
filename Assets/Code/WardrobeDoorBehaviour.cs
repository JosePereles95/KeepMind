using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WardrobeDoorBehaviour : MonoBehaviour {

	[HideInInspector] public float openAngleLeft;
	[HideInInspector] public float openAngleRight;
	[HideInInspector] public float defaultAngleLeft;
	[HideInInspector] public float defaultAngleRight;
	[HideInInspector] public Transform doorLeft;
	[HideInInspector] public Transform doorRight;
	[HideInInspector] public Transform rotationPointLeft;
	[HideInInspector] public Transform rotationPointRight;
	[HideInInspector] public AudioClip clipOpen;
	[HideInInspector] public AudioClip clipClose;
	public bool open = false;

	public bool isLerping = false;
	public bool isReady = false;

	public void Use() {
		if(!isLerping && isReady)
			StartLerping ();
	}

	void SetReady(bool value){
		isReady = value;
	}

	void SetBlock(){
		if(!open)
			isReady = false;
	}

	void StartLerping()
	{
		isLerping = true;

		if (open) {
			this.GetComponent<AudioSource> ().clip = clipClose;
			this.GetComponent<AudioSource> ().Play ();
		}
		else {
			this.GetComponent<AudioSource> ().clip = clipOpen;
			this.GetComponent<AudioSource> ().Play ();
		}
	}

	void FixedUpdate()
	{
		if (isLerping) {

			if (!open) {
				doorLeft.transform.RotateAround (rotationPointLeft.position, Vector3.up, 2f);
				doorRight.transform.RotateAround (rotationPointRight.position, Vector3.up, -2f);

				if (doorLeft.transform.eulerAngles.y >= openAngleLeft) {
					isLerping = false;
					open = true;
				}

			}
			else {
				doorLeft.transform.RotateAround (rotationPointLeft.position, Vector3.up, -2f);
				doorRight.transform.RotateAround (rotationPointRight.position, Vector3.up, 2f);

				if (doorLeft.transform.eulerAngles.y <= defaultAngleLeft || doorLeft.transform.eulerAngles.y > openAngleLeft) {
					isLerping = false;
					open = false;
				}
			}
		}
	}
}