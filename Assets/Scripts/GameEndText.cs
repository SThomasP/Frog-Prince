using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEndText : MonoBehaviour {

	private string[] script;
	private bool endgame = false;
	private bool over = false;
	private int position;
	private Text text;


	// Use this for initialization
	void Start () {
		
	}

	public void endGame(string[] script){
		endgame = true;
		this.script = script;
		text = GetComponentInChildren<Text> ();
		text.text = script [0];
		position = 1;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("action")){
			if (endgame) {
				if (!over) {
					if (position < script.Length) {
						text.text = script [position];
						position++;
					} else {
						over = true;
						text.text = "Press E to play again.";
					}
				} else {
					SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
				}
			}
		}

	}
}
