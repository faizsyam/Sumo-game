using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundControl : MonoBehaviour {

	public int roundID;
	public int nRound;
	public Text roundText;
	public GameControl GC;

	public SpriteRenderer slip;
	public GameObject hole;

	int count;
	/* 	0: Normal
	   	1: small area
	 	2: slippery
	 	3: middle out
	 	4: random bullets
	 	5: stacking powerups
	 	6: traps
	 	7: fake powerups
	*/

	public void newRound(){
		nRound++;
		count++;
		roundID = Random.Range (0, 6);
	}

	public void reset(){
		nRound = 1;
		roundID = 0;
		count = 0;
	}

	void Start () {
		reset ();
	}

	void Update () {
		if (!GC.isPlaying) {
			string roundStr;
			roundStr = "Round: " + nRound + "\n";
			if (roundID == 0)
				roundStr += "Normal Match";
			if (roundID == 1)
				roundStr += "Small Arena";
			if (roundID == 2)
				roundStr += "Careful! Slippery!";
			if (roundID == 3)
				roundStr += "Hole in the Middle";
			if (roundID == 4)
				roundStr += "Random FireBalls";
			if (roundID == 5)
				roundStr += "Stacking PowerUps";
			roundText.text = roundStr;
		} else {
			roundText.text = "";
		}
	}
}
