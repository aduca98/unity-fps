using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperADSOverlay : MonoBehaviour {

	[SerializeField]
	private Animator mainCamera;
	[SerializeField]
	private Animator gunCamera;
	[SerializeField]
	private Animator gun; 
	[SerializeField]
	private Animator scopeOverlay;
	[SerializeField]
	private Animator crosshairsOverlay;
//	[SerializeField]
//	private GameObject parentOfGun;

	public bool isADS = false; /*are you ADSing or not—true or false*/


	/*for scoping variable zoom*/
	float zoomSpeed = -.5f;
	float minFOV = 5;
	float maxFOV = 100;

	void Start(){

	}

	public void Update () {
		HandleScope (true);
		if (isADS == true) {
			if (Input.GetKey ("e")) {
				if (Camera.main.fieldOfView >= minFOV) {
					Camera.main.fieldOfView += zoomSpeed;	
				}
			}

			if (Input.GetKey ("q")) {
				if (Camera.main.fieldOfView <= maxFOV) {
					Camera.main.fieldOfView -= zoomSpeed;
				}
			}
		}

		if(gunCamera.gameObject.activeInHierarchy){
			gun.SetBool ("isRifle", true);
		}
	}
			

	public void HandleScope(bool gunCamOff){

		if(Input.GetKeyDown("left shift") && !isADS){
//			gun.SetBool ("isRifle", true);
			AnimateEverything("isADS",true,gunCamOff);
			isADS = true;
			crosshairsOverlay.SetBool ("isADS", true);

		}else if (Input.GetKeyDown("left shift") && isADS){
//			gun.SetBool ("isRifle", true);
			Camera.main.fieldOfView = 73;
			AnimateEverything("isADS",false,gunCamOff);
			isADS = false;
			crosshairsOverlay.SetBool ("isADS", false);
		}

	}

	private void  AnimateEverything(string p, bool b, bool handleGun){
//		gun.SetBool(p,b);
		gunCamera.SetBool(p,b);
		mainCamera.SetBool (p, b);
		if(handleGun) {
			StartCoroutine (GunCamera (p,b));
		}

	}

	private IEnumerator GunCamera(string p,bool b) {
		if (!isADS) yield return new WaitForSeconds (.4f);
		mainCamera.SetBool (p, b);
		gunCamera.gameObject.SetActive (!b); 
		scopeOverlay.SetBool (p, b);
//		crosshairsOverlay.SetBool (p, b);
	
	}
	
}
