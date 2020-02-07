using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

	public float cd;
	Vector2 dir;
	bool push;
	public AudioSource whoosh;

	public void move(){
		cd = 3;
		push = true;
	}

	void OnCollisionEnter2D(Collision2D obj){
		if (obj.gameObject.tag == "Player") {
			cd = 0;
			push = false;
		}
	}

	void reset(){
		cd = -6;
		int dirID = Random.Range (0, 8);
		Vector2 pos = new Vector2 (0,0);
		switch (dirID) {
		case 0:
			dir = new Vector2 (1, 0);
			pos = new Vector2 (-5, 0);
			break;
		case 1:
			dir = new Vector2 (1, 1);
			pos = new Vector2 (-4, -4);
			break;
		case 2:
			dir = new Vector2 (0, 1);
			pos = new Vector2 (0, -5);
			break;
		case 3:
			dir = new Vector2 (-1, 1);
			pos = new Vector2 (4, -4);
			break;
		case 4:
			dir = new Vector2 (-1, 0);
			pos = new Vector2 (5, 0);
			break;
		case 5:
			dir = new Vector2 (-1, -1);
			pos = new Vector2 (4, 4);
			break;
		case 6:
			dir = new Vector2 (0, -1);
			pos = new Vector2 (0, 5);
			break;
		case 7:
			dir = new Vector2 (1, -1);
			pos = new Vector2 (-4, 4);
			break;
		}

		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		GetComponent<Transform> ().position = pos;
		GetComponent<SpriteRenderer> ().color = new Vector4 (1, 1, 1, 0);
		GetComponent<CircleCollider2D> ().enabled = false;
	}
	void Start () {
		reset ();
	}

	void Update () {
		if (cd > 0) {
			GetComponent<SpriteRenderer> ().color = new Vector4 (1, 1, 1, (3 - cd) / 3);
			cd -= Time.deltaTime;
		} else if (cd <= -5 && cd != -6) {
			reset ();
			whoosh.Stop ();
			cd = -6;
		} else if (cd != -6) {
			if (push) {
				float strength = 8;
				whoosh.Play ();
				GetComponent<Rigidbody2D> ().velocity = dir * strength;
				GetComponent<CircleCollider2D> ().enabled = true;
				cd -= Time.deltaTime;
			} else {
				cd -= Time.deltaTime;
			}
		}
	}
}
