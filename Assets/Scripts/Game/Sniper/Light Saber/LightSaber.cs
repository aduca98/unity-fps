using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaber : MonoBehaviour {

	[SerializeField]
	private Animator saberAnim;

	public ParticleSystem fireBig;
	public ParticleSystem fireSmall;

	bool canFire; //limit the amount of fires you can do per second
	bool isFired; //only want to be able to do damage if you slash (not just walking into stuff with it)
	bool isHitSomeShit; //fireBig or fireSmall

	public GameObject saber;

	void Start () {
		canFire = true;
		isFired = false;
		isHitSomeShit = false;
		fireBig.Play ();
	}

	void Update () {
		if (Input.GetButtonDown ("Fire1") && canFire) {
			saberAnim.SetTrigger ("isFire");
			isFired = true;
			/*canFire = false; //might be able to do this implicitly with timer fxn*/
			if(!isHitSomeShit){
				fireSmall.Play();
			}else{
				fireBig.Play ();
			}
		}
	}

	void OnCollisionEnter(Collision collision){
		isHitSomeShit = true;
		print ("hit something");
		if(collision.gameObject.tag == "Target" ){
			print ("hit tarrget");
			TargetAnim animScript = collision.transform.GetComponent<TargetAnim> ();
			animScript.TargetAnimation();
		}

		if(collision.gameObject.tag == "MiscBody"){

			Vector3 gotHitLikeThis = (collision.gameObject.transform.position - saber.transform.position)*100;

			object[] tempStorage = new object[4]{"MiscBody", 100,collision.contacts[0].point, gotHitLikeThis.normalized};
	
			collision.transform.gameObject.SendMessageUpwards("FuuuuuckIDoneGotHit", tempStorage);

		}
	}
}
