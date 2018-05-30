using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
	private int gemCounter = 0;
	public int lifeCounter;
	public StoryManager sm;
	private Text gemText;
	private Text lifeText;
	private GameObject gameEnd;



	void Start(){
		gemText = GameObject.Find("GemText").GetComponent<Text>();
		lifeText = GameObject.Find ("LifeText").GetComponent<Text> ();
		gameEnd = GameObject.Find ("GameEnd");
		gameEnd.SetActive (false);
		gemText.text = gemCounter.ToString ();
		lifeText.text = lifeCounter.ToString ();
	}

	public void enable(){
		GetComponent<Canvas> ().enabled = true;	
	}

	public void disable(){
		GetComponent<Canvas> ().enabled = false;
	}

	public void increaseGemCount(){
		gemCounter++;
		gemText.text = gemCounter.ToString ();
		if (gemCounter == 7) {
			sm.allDiamondsCollected ();
		}
	}

	public void endGame(string[] script){
		gameEnd.SetActive (true);
		gameEnd.GetComponent<GameEndText> ().endGame (script);
	}

	public void decreaseLifeCount(){
		lifeCounter--;
		lifeText.text = lifeCounter.ToString ();
		if (lifeCounter == 0) {
			sm.died ();
		}
	}

}
