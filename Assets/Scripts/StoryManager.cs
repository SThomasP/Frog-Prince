using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour {

	private const int START_OF_GAME = 0;
	private const int MEET_THE_PRINCESS = 1;
	private const int GET_THE_DIAMONDS = 2;
	private const int ALL_DIAMONDS_GOT = 3;
	private const int GAME_END_GOOD = 4;
	private const int GAME_END_DEAD = 5;
	private const int GAME_END_FROG = 6;

	private int gameState;
	private GameObject player;
	private GameObject princess;
	private GameObject witch;
	private GameObject ibok;
	private GameObject gem;

	public UIController ui;

	void Start(){
		player = GameObject.Find ("frog");
		witch = GameObject.Find ("Witch");
		princess = GameObject.Find ("Princess");
		gem = GameObject.Find ("Gem");
		ibok = GameObject.Find ("Ibok");
		gem.SetActive (false);
		ibok.SetActive (false);
		setState (START_OF_GAME);
	}

	private string[][] conversations = new string[][] {
		// conversation 0 (Inital Conversation With Witch)
		new string[] {
			"Take that",
			"You're a frog now",
			"Why???",
			"Because I felt like it",
			"You'll need a princess to change you back",
			"There's probably one at the castle",
			"Have fun",
			"Ha! Ha! Ha!"
		},
		// conversation 1 (The First Conversation With the Princess)
		new string[] {
			"Kiss you",
			"No F**king way",
			"Fine, but first you must bring me seven magic gems",
			"They're all somewhere in the forest",
			"And don't talk to me again until you have them all",
			"Watch out for the ibok though,",
			"They tend to charge at frogs."
		},
		// conversation 2 (Speaking to the Princess while collection the diamonds)
		new string[] {
			"You're back?",
			"You haven't got all seven yet",
			"I said I wanted seven",
			"Forget it,",
			"I'll find them myself"
		},
		// conversation 3 (to the princess with all the diamonds)
		new string[] {
			"Ooh...",
			"Shiny magic gems",
			"And seven of them",
			"Well, come here then"
		},
		// conversation 4 ( Prince speaking after the transformation)
		new string[] {
			"And so,",
			"I returned to being human",
			"And burned the witch at the stake",
			"The End"
		},
		// conversation 5 (Died While Collecting)
		new string[] {
			"With that, I died",
			"And was eaten by ibok",
			"I have no idea why there were ibok in Europe",
			"The End"
		},
		// converstation 6 (Stuck as Frog)
		new string[] {
			"I never saw the princess again",
			"She got rich and moved to America",
			"And I'm still a Frog",
			"The End"
		}
	};

	public void endCoversation(string character){
		if (character.ToLower().Equals ("witch")) {
			switch (gameState) {
			case START_OF_GAME:
				setState (MEET_THE_PRINCESS);
				break;
			}
		} else if (character.ToLower().Equals("princess")) {
			switch (gameState) {
			case MEET_THE_PRINCESS:
				setState (GET_THE_DIAMONDS);
				break;
			case GET_THE_DIAMONDS:
				setState (GAME_END_FROG);
				break;
			case ALL_DIAMONDS_GOT:
				setState (GAME_END_GOOD);
				break;
			}
		}
	}

	public void endGame(int scriptNo){
		player.GetComponent<characterController> ().enabled = false;
		player.GetComponent<Talker> ().enabled = false;
		ibok.SetActive (false);
		ui.endGame(conversations [scriptNo]);
	}

	public void allDiamondsCollected(){
		if (gameState == GET_THE_DIAMONDS) {
			setState (ALL_DIAMONDS_GOT);
		}
	}

	public void died(){
		if (gameState == GET_THE_DIAMONDS || gameState == ALL_DIAMONDS_GOT) { 
			setState (GAME_END_DEAD);
		}
	}

	private void setState(int state){
		gameState = state;
		switch (state) {
		case START_OF_GAME:
			witch.GetComponentInChildren<Conversation> ().setConversation (true, conversations [0]);
			player.GetComponent<Talker> ().lookForTalkee ();
			break;
		case MEET_THE_PRINCESS:
			witch.SetActive (false);
			princess.GetComponentInChildren<Conversation> ().setConversation (true, conversations [1]);
			break;
		case GET_THE_DIAMONDS:
			gem.SetActive (true);
			ibok.SetActive (true);
			princess.GetComponentInChildren<Conversation> ().setConversation (true, conversations [2]);
			ui.enable ();
			break;
		case ALL_DIAMONDS_GOT:
			gem.SetActive (false);
			princess.GetComponentInChildren<Conversation> ().setConversation (true, conversations [3]);
			break;
		case GAME_END_DEAD:
		case GAME_END_FROG:
		case GAME_END_GOOD:
			endGame (gameState);
			break;
		}
		
	}

}
