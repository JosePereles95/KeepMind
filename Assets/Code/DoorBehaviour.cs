using UnityEngine;
using System.Collections;

public class DoorBehaviour : MonoBehaviour {

	public float openAngle;
	public float defaultAngle;
	public Transform rotationPoint;
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

	void StartLerping()
	{
		isLerping = true;
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