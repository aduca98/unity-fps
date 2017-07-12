using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//problems: 
// 			saberCollider reads enabled in console (printed it) but box isn't checked in inspector
//			Even when saberCollider is enabled, its not doing shit to the targets	

public class WeaponSelection : MonoBehaviour {
	//1=rifle
	//2=pistol

	public string weaponSelection;

	public SniperShooting sniperShooting;
	public SniperADSOverlay sniperADS;
	public ColtShooting coltShooting;
	public ColtADS coltADS;
	public LightSaber lightSaberScript;

	[SerializeField]
	private Animator coltAnim;
	[SerializeField]
	private Animator sniperAnim;
	[SerializeField]
	private Animator saberAnim;

	int randomNumber;

	public Collider saberCollider;

	void Start () {
		//initial settings
		weaponSelection = "Rifle";

		coltShooting.enabled = false;
		coltADS.enabled = false;
		coltAnim.SetBool("isPistol", false);

		lightSaberScript.enabled = false;
		saberAnim.SetBool ("isSaberUnder", false);
		saberCollider.enabled = false;

		sniperShooting.enabled = true;
		sniperADS.enabled = true;
		sniperAnim.SetBool("isRifle", true); //isRifle is altered in the SniperADS script because we turn off the gun during ADS (fucks w/ the bool)
	}

	void Update () {
		if(Input.GetKeyDown("1") && weaponSelection != "Rifle"){
			//disable other scripts
			lightSaberScript.enabled = false;
			coltShooting.enabled = false; 
			coltADS.enabled = false; 

			//figure out what the current (soon to be previous) weapon was and run that weapon's put away animation
			if (weaponSelection == "Saber"){
				saberAnim.SetBool("isSaberUnder", false);
				saberAnim.SetBool("isSaberOver", false);
				saberCollider.enabled = false;
			}
			if (weaponSelection == "Pistol"){
				coltAnim.SetBool("isPistol", false);
			}

			sniperAnim.SetBool("isRifle", true);

			sniperShooting.enabled = true; /*AFTER take out anim plays*/
			sniperADS.enabled = true; /*AFTER take out anim plays*/

			weaponSelection = "Rifle";
		}

		if(Input.GetKeyDown("2") && weaponSelection != "Pistol"){
			//disable other scripts
			lightSaberScript.enabled = false;
			sniperShooting.enabled = false; 
			sniperADS.enabled = false; 

			//figure out what the current (soon to be previous) weapon was and run that weapon's put away animation
			if (weaponSelection == "Saber"){
				saberAnim.SetBool("isSaberUnder", false);
				saberAnim.SetBool("isSaberOver", false);
				saberCollider.enabled = false;
			}
			if (weaponSelection == "Rifle"){
				sniperAnim.SetBool("isRifle", false);
			}

			coltAnim.SetBool("isPistol", true);

			coltShooting.enabled = true; /*AFTER take out anim plays*/
			coltADS.enabled = true; /*AFTER take out anim plays*/

			weaponSelection = "Pistol";
		}

		if(Input.GetKeyDown("3") && weaponSelection != "Saber"){
			print ("before true:" + saberCollider.enabled);
			saberCollider.enabled = true;
			print ("after true:" + saberCollider.enabled);

			/*randomNumber = Random.Range (0, 4); //Random.Range returns an int between min (inclusive) and max (EXCLUSIVE), hence [0,whatever)*/
			randomNumber = 0;

			//disable other scripts
			coltShooting.enabled = false;
			coltADS.enabled = false;
			sniperShooting.enabled = false; 
			sniperADS.enabled = false; 

			//figure out what the current (soon to be previous) weapon was and run that weapon's put away animation
			if (weaponSelection == "Pistol"){
				coltAnim.SetBool("isPistol", false);
			}
			if (weaponSelection == "Rifle"){
				sniperAnim.SetBool("isRifle", false);
			}

			if(randomNumber == 0){
				saberAnim.SetBool("isSaberUnder", true);
			}else{
				saberAnim.SetBool ("isSaberOver", true);
			}


			lightSaberScript.enabled = true; /*AFTER take out anim plays*/

			weaponSelection = "Saber";
		}

	}
}
