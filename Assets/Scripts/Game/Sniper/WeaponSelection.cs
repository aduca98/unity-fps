using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelection : MonoBehaviour {
	//1=rifle
	//2=pistol

	public string weaponSelection;

	public SniperShooting sniperShooting;
	public ColtShooting coltShooting;
	public SniperADSOverlay sniperADS;
	public ColtADS coltADS;

	[SerializeField]
	private Animator coltAnim;
	[SerializeField]
	private Animator sniperAnim;

	void Start () {
		weaponSelection = "Rifle";
		coltShooting.enabled = false;
		coltADS.enabled = false;
		coltAnim.SetBool("isPistol", false);
		sniperAnim.SetBool("isRifle", true); //isRifle is altered in the SniperADS script because we turn off the gun during ADS (fucks w/ the bool)
	}

	void Update () {
		if(Input.GetKeyDown("1") && weaponSelection != "Rifle"){
			coltShooting.enabled = false;
			coltADS.enabled = false;

			coltAnim.SetBool("isPistol", false);
			sniperAnim.SetBool("isRifle", true);

			sniperShooting.enabled = true; /*AFTER take out anim plays*/
			sniperADS.enabled = true; /*AFTER take out anim plays*/

			weaponSelection = "Rifle";
		}

		if(Input.GetKeyDown("2") && weaponSelection != "Pistol"){
			coltShooting.enabled = true;
			coltADS.enabled = true;

			coltAnim.SetBool("isPistol", true);
			sniperAnim.SetBool("isRifle", false);
			sniperAnim.SetBool ("ReallyShouldntRun", false);
			sniperAnim.SetBool ("ReallyReallyShouldNotRun", true);


			sniperShooting.enabled = false; /*AFTER take out anim plays*/
			sniperADS.enabled = false; /*AFTER take out anim plays*/

			weaponSelection = "Pistol";
		}

	}
}
