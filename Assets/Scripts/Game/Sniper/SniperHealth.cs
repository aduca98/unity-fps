using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//get ray hit info from SniperShooting script and carry out animations, effects, HUD updating...relating to health and getting hurt

public class SniperHealth : MonoBehaviour {

	public GameObject stanTheMan;

	int maxHealth = 100;
	int currentHealth;
	float timerForHealthRegen = 0;

	public ParticleSystem explosion;
	public ParticleSystem blood;
	public ParticleSystem fire;
	public ParticleSystem magicShit;

	public Material[] materials;
	public Renderer rend;

	//gotHitHere cases
	/*string miscBelowHead;
	string heart;
	string lung;
	string brain;
	string eye;*/

	//for object reference static issue lol
	int hurtThisFuckinMuch;
	string gotHitHere;

	public Text stansHealth;


	void Start() {
		currentHealth = maxHealth;
		stansHealth.text = currentHealth.ToString();
	}

	public void Update(){
		//REGENERATION...
		if (currentHealth < maxHealth) {
			rend.sharedMaterial = materials[1];
			if (currentHealth > 0) {
				if (timerForHealthRegen < .2) {
					timerForHealthRegen += Time.deltaTime;
				} else {
					timerForHealthRegen = 0;
				}
				if (timerForHealthRegen == 0) {
					++currentHealth;
					stansHealth.text = currentHealth.ToString ();
				}
			}

			if(currentHealth <= 0 && !magicShit.isPlaying){
				magicShit.Play ();
				Destroy (stanTheMan,1f);
				print ("StanDestroyed");
			}
		}else {
			rend.sharedMaterial = materials [0];
		}
		//...REGENERATION
				
	}

	//fxn that will be called by SniperShooting Script
	public void FuuuuuckIDoneGotHit(object[] o){
		string gotHitHere = (string)o [0]; 
		int hurtThisFuckinMuch = (int)o [1];
		Vector3 gotHitHereEXACTLYHere = (Vector3)o [2];
		Vector3 gotHitLikeThis = (Vector3)o [3];

		currentHealth -= hurtThisFuckinMuch;
		stansHealth.text = currentHealth.ToString();

		switch (gotHitHere) {
		case "MiscBody":
			
			Effect (hurtThisFuckinMuch, gotHitHereEXACTLYHere, gotHitLikeThis, false);
			break;

		case "Heart":

			Effect (hurtThisFuckinMuch, gotHitHereEXACTLYHere, gotHitLikeThis, true);
			break;

		case "Brain":
			
			Effect (hurtThisFuckinMuch, gotHitHereEXACTLYHere, gotHitLikeThis, false);
			break;

		case "Lung":
			
			Effect (hurtThisFuckinMuch, gotHitHereEXACTLYHere, gotHitLikeThis, false);
			break;

		case "Eye":
			
			Effect (hurtThisFuckinMuch, gotHitHereEXACTLYHere, gotHitLikeThis, true);
			break;

		case "Tiara":
			
			Effect (hurtThisFuckinMuch, gotHitHereEXACTLYHere, gotHitLikeThis, false);
			break;
			
		}
	}

	//fxn that will determine the visible effect of being hit
	void Effect(int hurtThisFuckinMuch, Vector3 gotHitHereEXACTLYhere, Vector3 gotHitLikeThis, bool isExplosive){
		if (hurtThisFuckinMuch <= 20){
			print ("blood play please");
			blood.transform.position = gotHitHereEXACTLYhere;
			blood.transform.Rotate(gotHitLikeThis);
			blood.transform.localScale = new Vector3 (1,1,1);
			blood.Play ();
			print ("blood played");
			if(isExplosive){
				explosion.transform.position = gotHitHereEXACTLYhere;
				explosion.transform.localScale = new Vector3 (1,1,1);
				explosion.Play ();
			}
				
		}

		if (hurtThisFuckinMuch > 20 && hurtThisFuckinMuch <= 40){
			blood.transform.position = gotHitHereEXACTLYhere;
			blood.transform.Rotate(gotHitLikeThis);
			blood.transform.localScale = new Vector3 (1,1,1);
			blood.Play ();
			if(isExplosive){
				explosion.transform.position = gotHitHereEXACTLYhere;
				explosion.transform.localScale = new Vector3 (1,1,1);
				explosion.Play ();
			}
		}
	
		if (hurtThisFuckinMuch > 40 && hurtThisFuckinMuch <= 60){
			blood.transform.position = gotHitHereEXACTLYhere;
			blood.transform.Rotate(gotHitLikeThis);
			blood.transform.localScale = new Vector3 (1,1,1);
			blood.Play ();
			if(isExplosive){
				explosion.transform.position = gotHitHereEXACTLYhere;
				explosion.transform.localScale = new Vector3 (1,1,1);
				explosion.Play ();
			}

		}

		if (hurtThisFuckinMuch > 60 && hurtThisFuckinMuch <= 80){
			blood.transform.position = gotHitHereEXACTLYhere;
			blood.transform.Rotate(gotHitLikeThis);
			blood.transform.localScale = new Vector3 (1,1,1);
			blood.Play ();
			if(isExplosive){
				explosion.transform.position = gotHitHereEXACTLYhere;
				explosion.transform.localScale = new Vector3 (1,1,1);
				explosion.Play ();
			}

		}
			
		if (hurtThisFuckinMuch > 80 && hurtThisFuckinMuch < 100){
			blood.transform.position = gotHitHereEXACTLYhere;
			blood.transform.Rotate(gotHitLikeThis);
			blood.transform.localScale = new Vector3 (1,1,1);
			blood.Play ();
			if(isExplosive){
				explosion.transform.position = gotHitHereEXACTLYhere;
				explosion.transform.localScale = new Vector3 (1,1,1);
				explosion.Play ();
			}
	
		}

		if (hurtThisFuckinMuch >= 100) {
			blood.transform.position = gotHitHereEXACTLYhere;
			blood.transform.Rotate(gotHitLikeThis);
			blood.transform.localScale = new Vector3 (1,1,1);
			blood.Play ();
			if (isExplosive) {
				explosion.transform.position = gotHitHereEXACTLYhere;
				explosion.transform.localScale = new Vector3 (1,1,1);
				explosion.Play ();
			}
		}
	}

}
