using UnityEngine;
using System.Collections;

public class WardrobeDoorBehaviour : MonoBehaviour {

	public float openAngleLeft;
	public float openAngleRight;
	public float defaultAngleLeft;
	public float defaultAngleRight;
	public Transform doorLeft;
	public Transform doorRight;
	public Transform rotationPointLeft;
	public Transform rotationPointRight;
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

				if (doorLeft.transform.eulerAngles.y <= defaultAngleLeft) {
					isLerping = false;
					open = false;
				}
			}
		}
	}
}