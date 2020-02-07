using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public SpriteRenderer powerFX;
	public PlayerControl PC;
	public Sprite dead;
	Sprite normal;

	public int id;
	public bool isMoving, lose;
	public float power, spin, speed, revpower;
	public float startX, startY, startZ;

	public float poweredCD;

	public AudioSource bump, puSound;

	float powerTMP;
	public int puStack;
	public void powered(float amount, float duration){
		puSound.Play ();
		puStack++;
		powerFX.enabled = true;
		power += amount;
		revpower = amount * 2 / 3;
		poweredCD = duration;
	}

	void OnTriggerExit2D(Collider2D obj){
		if (obj.gameObject.tag == "Arena" ) {
			PC.playerOut (id);
			lose = true;
			powerFX.enabled = false;
		}
	}
	void OnTriggerEnter2D(Collider2D obj){
		if (obj.gameObject.tag == "Fall") {
			PC.playerOut (id);
			lose = true;
			powerFX.enabled = false;
		}
	}

	void OnCollisionEnter2D(Collision2D obj){
		if (obj.gameObject.tag == "Player") {
			bump.Play ();
			foreach (ContactPoint2D contact in obj.contacts) {
				contact.otherCollider.GetComponent<Rigidbody2D>().AddForce(  contact.normal * (obj.gameObject.GetComponent<Player>().power - revpower),  ForceMode2D.Impulse);
			}
		} 
	}

	void OnCollisionStay2D(Collision2D obj){
		if(obj.gameObject.tag == "Wall"){
			bump.Play ();
			Vector2 dir = GetComponent<Transform> ().position.normalized ;
			GetComponent<Rigidbody2D> ().AddForce (dir * power, ForceMode2D.Impulse);
			//foreach (ContactPoint2D contact in this.GetComponent<Collision2D>().contacts) {
			//	contact.otherCollider.GetComponent<Rigidbody2D>().AddForce(  contact.normal * (power - revpower),  ForceMode2D.Impulse);
			//}
		}
	}

	void Start () {
		normal = this.GetComponent<SpriteRenderer> ().sprite;
		startX = transform.position.x;
		startY = transform.position.y;
		powerTMP = power;
		isMoving = false;
	}

	void Update () {
		if (poweredCD > 0)
			poweredCD -= Time.deltaTime;
		else {
			powerFX.enabled = false;
			power = powerTMP;
			revpower = 0;
			puStack = 0;
		}
		if (!lose) {
			this.GetComponent<SpriteRenderer> ().sprite = normal;
			GetComponent<Collider2D> ().isTrigger = false;
			if (!isMoving) {
				transform.Rotate (Vector3.forward * Time.deltaTime * spin, Space.World);
			} else {
				GetComponent<Rigidbody2D> ().AddForce (transform.right * speed);
			}
		} else {
			this.GetComponent<SpriteRenderer> ().sprite = dead;
			GetComponent<Collider2D> ().isTrigger = true;
		}

		if (PC.GC.RC.roundID == 5) {
			powerFX.color = new Vector4 (1, 1, 1, puStack/6.0f);
		} else {
			powerFX.color = new Vector4 (1, 1, 1, 88 / 255.0f);
		}
	}
}
