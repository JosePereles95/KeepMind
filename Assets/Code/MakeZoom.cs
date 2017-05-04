﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class MakeZoom : MonoBehaviour {

	public bool isReady = false;
	public bool zoomIn = false;
	public bool zoomOut = false;
	public bool lookingPC = false;

	public Transform cameraTargetPos;
	public Transform cameraTargetLook;
	Vector3 actualPos;
	Quaternion actualRot;

	public GameObject player;
	public Transform cameraObject;
	public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
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

		if (isReady && !zoomIn && !zoomOut && !lookingPC) {
			player.GetComponent<FirstPersonController> ().enabled = false;
			actualPos = cameraObject.position;
			actualRot = cameraObject.rotation;
			zoomIn = true;
		}
		else if (lookingPC && !zoomOut) {
			zoomOut = true;
			lookingPC = false;
		}
	}

	IEnumerator zoomInToFalse() {
		yield return new WaitForSeconds (1);
		zoomIn = false;
	}
}
