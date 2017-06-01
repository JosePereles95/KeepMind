using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour {

	private bool waiting = false;

	void Update () {
		if (!waiting && Input.GetMouseButtonDown (0)){
			StartCoroutine ("Menu");
		}
	}

	IEnumerator Menu(){
		waiting = true;	
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene("MainMenu");
	}
}