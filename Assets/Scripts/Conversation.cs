using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conversation : MonoBehaviour {

	public string character;
	private int stage = 0;
	public bool canBeTalkedTo;
	private string[] script;
	StoryManager sm;

	// Use this for initialization
	void Start () {
		sm = GameObject.Find ("Controller").GetComponent<StoryManager> ();
	}

	public void setConversation(bool avaliable, string[] newScript){
		canBeTalkedTo = avaliable;
		script = newScript;
		
	}

	private void stopTalking(){
		GetComponent<Canvas>().enabled = false;
		canBeTalkedTo = false;
		sm.endCoversation (character);
	}

	private void setText(string line){
		Text text = GetComponentInChildren<Text> ();
		text.text = line;

	}

	public bool advanceConversation(){
		if (stage == script.Length) {
			stopTalking ();
			return false;
		} else {
			setText (script [stage]);
			stage++;
			return true;
		}
	}

	public bool talkTo(Transform player){
		if (canBeTalkedTo) {
			GetComponent<Canvas> ().enabled = true;
			transform.parent.LookAt (player);

			setText (script [0]);
			stage = 1;
			return true;
		} else {
			return false; 
		}
		
	}
}
