using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour {

	public AudioSource click;

	public void Quit(){
		click.Play();
		Application.Quit();
	}

	public void home(){
		click.Play();

		SceneManager.LoadScene ("Main");
	}

	public void selectLevel(int i){
		click.Play();

		if (i == 2)
			SceneManager.LoadScene ("play1");
		else if (i == 3)
			SceneManager.LoadScene ("play2");
		else if (i == 4)
			SceneManager.LoadScene ("play3");
	}

	void Start () {
		
	}

	void Update () {
		
	}
}
