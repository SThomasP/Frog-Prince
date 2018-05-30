using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talker : MonoBehaviour {

	private bool talking = true;
	public float distance;

	private Conversation talkingTo;
	private characterController movementController;

	// Use this for initialization
	void Start () {
		movementController = GetComponent<characterController> ();
	}

	private void stopTalking(){
		movementController.enabled = true;
		talking = false;
		
	}

	private void continueTalking(){
		if (!talkingTo.advanceConversation ()) {
			stopTalking ();
		};
	}

	public void lookForTalkee(){
		int charactersMask = 1 << 8;
		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out hit, distance, charactersMask)) {
			// set the character the player is talking to
			talkingTo = hit.collider.gameObject.transform.Find ("SpeechBubble").gameObject.GetComponent<Conversation>();
			if (talkingTo.talkTo (transform)) {
				talking = true;
				if (movementController != null)
					movementController.enabled = false; 
			}
		}
	}

	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("action")) {
			if (!talking) {
				lookForTalkee ();
			} else {
				continueTalking ();
			}
		}
	}
}
