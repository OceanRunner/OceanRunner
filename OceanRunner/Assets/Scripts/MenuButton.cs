using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour {
	public void PushPlay() {
		SceneManager.LoadScene(1);
		//		Application.LoadLevel("Main");
	}

	public void Settings() {
		SceneManager.LoadScene(2);
		//		Application.LoadLevel("Main");
	}
	public void Exit() {
		Application.Quit();
	}
}
	
