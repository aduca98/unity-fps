using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADS : MonoBehaviour {

	[SerializeField]
	private Animator mainCamera;
	[SerializeField]
	private Animator gunCamera;
	[SerializeField]
	private Animator gun; /*don't add anything here, start method gets the gun's animator*/
	[SerializeField]
	private Animator scopeOverlay;
	[SerializeField]
	private Animator crosshairsOverlay;
	[SerializeField]
	private GameObject parentOfGun;

	public bool isADS = false; /*are you ADSing or not—true or false*/

	/*for gun and main cam animator to know which animations to run*/
	public bool isM4Reflex = false;
	public bool isM4Holo = false;
	public bool isM4Hybrid = false;
	public bool isM4DMR = false;
	public bool isM4M16Irons = false;
	public bool isM4MP5Irons = false;

	/*for scoping variable zoom*/
	float zoomSpeed = -5f;
	float minFOV = 5;
	float maxFOV = 100;
	float currentFOV;

	void Awake (){

		switch (AlwaysExist.gameGun) {
		case "Reflex":
			gunCamera.SetBool ("isM4Reflex", true);
			mainCamera.SetBool ("isM4Relex", true);

			isM4Reflex = true;
			break;

		case "Holographic":
			gunCamera.SetBool ("isM4Holo", true);
			mainCamera.SetBool ("isM4Holo", true);

			isM4Holo = true;
			print (isM4Holo);
			break;

		case "Hybrid":
			gunCamera.SetBool ("isM4Hybrid", true);
			mainCamera.SetBool ("isM4Hybrid", true);

			isM4Hybrid = true;
			break;

		case "ScopedDMR":
			gunCamera.SetBool ("isM4DMR", true);
			mainCamera.SetBool ("isM4DMR", true);

			isM4DMR = true;
			break;

		case "MP5Irons":
			gunCamera.SetBool ("isM4MP5Irons", true);
			mainCamera.SetBool ("isM4MP5Irons", true);

			isM4MP5Irons = true;
			break;

		case "M16Irons":
			gunCamera.SetBool ("isM4M16Irons", true);
			mainCamera.SetBool ("isM4M16Irons", true);

			isM4M16Irons = true;
			break;

		case "BarrettM82":
			print("haven't done shit for this one sorry man");
			break;
		}
			
	}

	void Start(){
		gun = parentOfGun.GetComponentInChildren<Animator> (); 
		if (AlwaysExist.gameGun == "BarrettM82" || AlwaysExist.gameGun == "ScopedDMR") {
			currentFOV = Camera.main.fieldOfView;
			print ("Current FOV " +currentFOV);
			print ("Cam FOV " + Camera.main.fieldOfView);
		}
	}

	public void Update () {
		if (AlwaysExist.gameGun == "BarrettM82" || AlwaysExist.gameGun == "ScopedDMR") {
			HandleScope (true);
			/*variable zoom scope. Problems: not actually changing FOV (even though print statemetn says it is), 
			 * and it is only changing once in each direction, not updating the FOV*/
			if (isADS == true) {
				if (Input.GetKeyDown ("e")) {
					if (Camera.main.fieldOfView >= minFOV) {
						currentFOV += zoomSpeed;
						Camera.main.fieldOfView = currentFOV;
						print ("Cam FOV " + Camera.main.fieldOfView);
						print ("Current FOV " + currentFOV);
					}
				}

				if (Input.GetKeyDown ("q")) {
					if (Camera.main.fieldOfView <= maxFOV) {
						currentFOV -= zoomSpeed;
						Camera.main.fieldOfView = currentFOV;
						print ("Cam FOV " + Camera.main.fieldOfView);
						print ("Current FOV " + currentFOV);
					}
				}
			}
		}else{
			HandleScope (false);

		}
	}
			

	public void HandleScope(bool gunCamOff){


		if(Input.GetKeyDown("left shift") && !isADS){
			AnimateEverything("isADS",true,gunCamOff);
			isADS = true;
			crosshairsOverlay.SetBool ("isADS", true);

		}else if (Input.GetKeyDown("left shift") && isADS){
			AnimateEverything("isADS",false,gunCamOff);
			isADS = false;
			crosshairsOverlay.SetBool ("isADS", false);
		}

	}

	private void  AnimateEverything(string p, bool b, bool handleGun){
		gun.SetBool(p,b);
		gunCamera.SetBool(p,b);
		mainCamera.SetBool (p, b);
		if(handleGun) {
			StartCoroutine (GunCamera (p,b));
		}

	}

	private IEnumerator GunCamera(string p,bool b) {
		if (isADS) yield return new WaitForSeconds (.2f);
		else yield return new WaitForSeconds (.44f); 
		mainCamera.SetBool (p, b);
		gunCamera.gameObject.SetActive (!b);
		scopeOverlay.SetBool (p, b);
//		crosshairsOverlay.SetBool (p, b);
	
	}
	

}
