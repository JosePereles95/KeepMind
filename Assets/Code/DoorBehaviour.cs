using UnityEngine;
using System.Collections;

public class DoorBehaviour : MonoBehaviour {

	public float openAngle;
	public float defaultAngle;
	public Transform rotationPoint;
	public bool open = false;
	public bool isLerping = false;

	public AudioClip clipOpen;
	public AudioClip clipClose;

	public bool isReady = false;

	public void Use() {
		if(!isLerping && isReady)
			StartLerping ();
	}

	void SetReady(bool value){
		isReady = value;
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
				transform.RotateAround (rotationPoint.position, Vector3.up, 2f);
				if (transform.eulerAngles.y >= openAngle) {
					isLerping = false;
					open = true;
				}
			}
			else {
				transform.RotateAround (rotationPoint.position, Vector3.up, -2f);
				if (transform.eulerAngles.y <= defaultAngle) {
					isLerping = false;
					open = false;
				}
			}
		}
	}
}