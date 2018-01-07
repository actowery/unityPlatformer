using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//for menu methods
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

	// Use this for initialization
	bool clicked;
	void Start () {
		clicked = false;
	}
	
	// Update is called once per frame
	void Update () {
		//click start button to begin
		if (clicked) {
			if (this.name == "Start") {
				SceneManager.LoadScene ("Scene1");
			} else {
				Debug.Log ("quit");
				clicked = !clicked;
			}
		}
	}

	//when clicked
	private void OnMouseDown() {
		clicked = true;
	}

}