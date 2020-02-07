using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

	public GameControl GC;

	public int playerNumb;

	public Player[] p;

	public Button[] b;

	public int plyctr, winner;

	public int[] scoreAdded;

	float spintmp;

	public bool gotlose;

	public AudioSource whoosh;

	public void setPosition(){
		gotlose = false;
		if (playerNumb == 2)
			winner = 1;
		else if (playerNumb == 3)
			winner = 3;
		else if (playerNumb == 4)
			winner = 6;
		plyctr = playerNumb;
		for (int i = 0; i < playerNumb; i++) {
			p [i].poweredCD = 0;
			p [i].lose = false;
			p [i].transform.position = new Vector3 (p[i].startX, p[i].startY, 0);
			p [i].transform.rotation = Quaternion.Euler (0, 0, p[i].startZ);
			p [i].puStack = 0;
		}
	}

	public void movePlayer(int moveID){
		if (GC.isPlaying)
			whoosh.Play ();
		if(GC.start) p[moveID].isMoving = true;
	}

	public void stopPlayer(int moveID){
		p[moveID].isMoving = false;
	}

	public void playerOut(int id){
		scoreAdded[id] = playerNumb - plyctr;
		plyctr--;
		winner -= id;

	}
	void Start () {
		spintmp = p [0].spin;
	}

	void Update () {
		if (plyctr == 1 && !gotlose) {
			gotlose = true;
			GC.GameOver (winner, scoreAdded[0], scoreAdded[1], scoreAdded[2], scoreAdded[3]);
		}
		if (!GC.start) {
			for (int i = 0; i < playerNumb; i++) 
				p [i].spin = 0;
		} else {
			for (int i = 0; i < playerNumb; i++) 
				p [i].spin = spintmp;
		}
	}
}
