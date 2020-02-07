using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {
	public Sprite[] sp;
	public Sprite ko;
	public GameObject arena1, arena2;
	public Image load;
	public RoundControl RC;
	public float powerupCTR, cdStart;
	public PowerUps PU;
	public PlayerControl PC;
	public bool start, readystart;
	public bool[] ready;
	public Text info;
	public Text[] r,s;
	public int[] score;
	public int nPlayer;
	public float roundtime;

	public bool isPlaying;
	//****
	public float defaultLD;
	public bullet blt;

	public float bltCD;
	//**
	public void Replay(){
		for (int i = 0; i < PC.playerNumb; i++){
			score [i] = 0;
			ready [i] = false;
			r [i].text = "";
		}
		cdStart = -1;
		RC.reset ();
	}

	public void PlayerReady(int ID){
		if(cdStart==-1) r[ID].text = "Ready!";
		ready [ID] = true;
	}

	public void GameOver(int winner, int score1, int score2, int score3, int score4){
		score [0] += score1;
		score [1] += score2;
		if (PC.playerNumb > 2) {
			score [2] += score3;
			if (PC.playerNumb > 3)
				score [3] += score4;
		}
		score [winner] += PC.playerNumb - 1;
		for (int i = 0; i <= 3; i++)
			PC.scoreAdded [i] = 0;
		winner += 1;
		cdStart = -7;
		start = false;
		info.text = "Player " + winner + " Wins!";
		if (PC.GC.RC.roundID == 5)
			powerupCTR = 2 + Random.Range (0.0f, 1.0f);
		else
			powerupCTR = 6 + Random.Range (0.0f, 2.0f);
		RC.newRound();
	}

	void checkReady(){
		readystart = true;
		for (int i = 0; i < PC.playerNumb; i++) {
			if (!ready [i])
				readystart = false;
			if (PC.playerNumb - i == 1) {
				if (readystart)
					cdStart = 3.3f;
				else {
					readystart = true;
				}
			}
		}
		if (RC.roundID == 1) {
			arena1.GetComponentInChildren<Collider2D> ().enabled = false;
			arena1.GetComponentInChildren<SpriteRenderer> ().enabled = false;
			arena2.GetComponentInChildren<Collider2D> ().enabled = true;
			arena2.GetComponentInChildren<SpriteRenderer> ().enabled = true;
		} else {
			arena1.GetComponentInChildren<Collider2D> ().enabled = true;
			arena1.GetComponentInChildren<SpriteRenderer> ().enabled = true;
			arena2.GetComponentInChildren<Collider2D> ().enabled = false;
			arena2.GetComponentInChildren<SpriteRenderer> ().enabled = false;
		}
		if (RC.roundID == 2) {
			RC.slip.enabled = true;
			for (int i = 0; i < nPlayer; i++) {
				PC.p [i].GetComponent<Rigidbody2D> ().drag = 0.9f;
			}
		} else {
			RC.slip.enabled = false;
			for (int i = 0; i < nPlayer; i++) {
				PC.p [i].GetComponent<Rigidbody2D> ().drag = defaultLD;
			}
		}
		if (RC.roundID == 3) {
			info.color = new Vector4 (0.55f, 0.55f, 0.55f, 1);
			RC.hole.gameObject.SetActive (true);
		} else {
			info.color = new Vector4 (0.16f, 0.16f, 0.16f, 1);
			RC.hole.gameObject.SetActive (false);
		}

	}
	// ***********


	//********
	void Start () {
		readystart = true;
		cdStart = -1;
		powerupCTR = 5 + Random.Range (0.0f, 2.0f);
		roundtime = 0;
		isPlaying = false;
		defaultLD = PC.p [0].GetComponent<Rigidbody2D> ().drag;
		bltCD = 5;
	}

	void Update () {

		for (int i = 0; i < PC.playerNumb; i++) {
			if (!PC.p [i].lose)
				PC.b [i].image.sprite = sp [i];
			else
				PC.b [i].image.sprite = ko;
			s [i].text = "" + score [i];
		}
		if (cdStart == -1) {
			PC.setPosition ();
			info.text = "GET READY!";
			isPlaying = false;
		} else if (cdStart > 2.3f)
			info.text = "3";
		else if (cdStart > 1.3f)
			info.text = "2";
		else if (cdStart > 0.3f)
			info.text = "1";
		else if (cdStart >= 0) {
			info.text = "START!";
			isPlaying = true;
			for (int i = 0; i < PC.playerNumb; i++)
				r [i].text = "";
		}

		if (isPlaying) {
			roundtime += Time.deltaTime;
		}

		if (cdStart == -1) {
			checkReady ();
		}
		if (cdStart > 0)
			cdStart -= Time.deltaTime;
		else if(cdStart>=-0.5f){
			info.text = "";
			start = true;
		}
		if (cdStart < -2) {
			load.fillAmount = (-2 - cdStart)/ 5;
			cdStart += Time.deltaTime;
		}
		else if (cdStart < -1) {
			load.fillAmount = 0;
			for (int i = 0; i < PC.playerNumb; i++) {
				ready [i] = false;
			}
			cdStart = -1;
		} else load.fillAmount = 0;
		if (powerupCTR > 0) {
			if (start)
				powerupCTR -= Time.deltaTime;
		}
		else {
			PowerUps newPU = Instantiate (PU, new Vector3(Random.Range(-3.0f,3.0f),Random.Range(-3.0f,3.0f),0), transform.rotation);
			newPU.GC = this.GetComponent<GameControl> ();
			if (RC.roundID != 5)
				powerupCTR = 12 + Random.Range (0.2f, 2.0f);
			else
				powerupCTR = 2.5f + Random.Range (0.2f, 1.0f);

		}
		///////////////////////////////

		if (RC.roundID == 4 && start) {
			if (bltCD > 0) {
				bltCD -= Time.deltaTime;
			} else {
				blt.move ();
				bltCD = 10;
			}
		}
	}
}
