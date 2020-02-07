using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour {

	public GameControl GC;
	public int ID;
	public float amount,duration, dur;
	public SpriteRenderer spr;

	float durtmp;

	void OnTriggerStay2D(Collider2D obj){
		if (obj.gameObject.tag == "Player" && durtmp-dur > 0.3f) {
			if (ID == 1) {
				obj.gameObject.GetComponent<Player> ().powered (amount, duration);
				Destroy (this.gameObject);
			}
		}
	}

	void Start () {
		durtmp = dur;
		spr.enabled = false;
	}

	void Update () {
		if(GC.cdStart==-1) 
			Destroy (this.gameObject);
		if(durtmp-dur > 0.3f)
			spr.enabled = true;
		if (dur > 0)
			dur -= Time.deltaTime;
		else 
			Destroy (this.gameObject);
		
	}
}
