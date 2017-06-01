using UnityEngine;
using System.Collections;

public class DoorBehaviour : MonoBehaviour {

	[HideInInspector] public float openAngle;
	[HideInInspector] public float defaultAngle;

	[HideInInspector] public Transform rotationPoint;

	[HideInInspector] public bool open = false;

	[HideInInspector] public AudioClip clipOpen;
	[HideInInspector] public AudioClip clipClose;

	private bool isReady = false;
	private bool isLerping = false;

	public void Use() {
		if(!isLerping && isReady)
			StartLerping ();
	}

	void SetReady(bool value) {
		isReady = value;
	}

	void StartLerping() {
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

	void FixedUpdate() {
		if (isLerping) {
			if (!open) {
				transform.RotateAround (rotationPoint.position, Vector3.up, 2f);
				if (transform.eulerAngles.y >= openAngle) {
					isLerping = false;
					open = true;
				}
			}
			else {
				transform.RotateAround (rotationPoint.position, Vector3.up, -2f);
				if (transform.eulerAngles.y <= defaultAngle || transform.eulerAngles.y > openAngle) {
					isLerping = false;
					open = false;
				}
			}
		}
	}
}