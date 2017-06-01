using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class MakeZoom : MonoBehaviour {

	[HideInInspector] public GameObject player;
	[HideInInspector] public Transform cameraObject;

	[HideInInspector] public Transform cameraTargetPos;
	[HideInInspector] public Transform cameraTargetLook;

	[HideInInspector] public float speed;

	public bool lookingPC = false;
	[HideInInspector] public bool raycasting = false;

	private bool isReady = false;

	public bool zoomIn = false;
	public bool zoomOut = false;

	private Vector3 actualPos;
	private Quaternion actualRot;

	void Update () {
		if (zoomIn) {
			float step = speed * Time.deltaTime;
			cameraObject.position =	Vector3.MoveTowards (cameraObject.position, cameraTargetPos.position, step);

			var targetRotation = Quaternion.LookRotation (cameraTargetLook.position - cameraObject.position, Vector3.up);
			cameraObject.rotation =	Quaternion.Slerp (cameraObject.rotation, targetRotation, Time.deltaTime * 7.0f);
		}
		else if (zoomOut) {
			float step = speed * Time.deltaTime;
			cameraObject.position =	Vector3.MoveTowards (cameraObject.position, actualPos, step);

			cameraObject.rotation =	actualRot;
		}

		if (!zoomOut && cameraObject.position == cameraTargetPos.position) {
			lookingPC = true;
			StartCoroutine("zoomInToFalse");
		}
		if (cameraObject.position == actualPos) {
			zoomOut = false;
			player.GetComponent<FirstPersonController> ().enabled = true;
		}
	}

	void SetReady(bool value){
		isReady = value;
	}

	void Use (){
		if (raycasting) {
			if (isReady && !zoomIn && !zoomOut && !lookingPC) {
				player.GetComponent<FirstPersonController> ().enabled = false;
				actualPos = cameraObject.position;
				actualRot = cameraObject.rotation;
				zoomIn = true;
			} else if (lookingPC && !zoomOut) {
				zoomOut = true;
				lookingPC = false;
			}
		}
	}

	IEnumerator zoomInToFalse() {
		yield return new WaitForSeconds (1);
		zoomIn = false;
	}
}