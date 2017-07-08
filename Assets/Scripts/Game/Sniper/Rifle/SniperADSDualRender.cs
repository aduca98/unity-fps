using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// (local) scope coordinates: (-0.1391694,27.60374,-33.36742)

public class SniperADSDualRender : MonoBehaviour {

	[SerializeField]
	private Animator mainCamera;
	[SerializeField]
	private Animator gunCamera;
	[SerializeField]
	private Animator gun; 
	[SerializeField]
	private Animator crosshairsOverlay;
//	[SerializeField]
//	private GameObject parentOfGun;

	public bool isADS = false; /*are you ADSing or not—true or false*/


	/*for scoping variable zoom*/
	float zoomSpeed = .1f;
	float minZoom = 3f;
	float maxZoom = 20f;
	public GameObject magnifiedLens;
	float zoom = 3f;
	Renderer lensRenderer;


	void Start(){
		lensRenderer = magnifiedLens.GetComponent<Renderer> ();
		lensRenderer.material.SetFloat ("_Magnification", zoom);
	}

	public void Update () {
		HandleScope (true);
		if (isADS == true) {
			lensRenderer.material.SetFloat ("_Magnification", zoom);
			if (Input.GetKey ("e")) {
				print (zoom);
				if (zoom <= maxZoom) {
					zoom += zoomSpeed;	
				}
			}

			if (Input.GetKey ("q")) {
				print (zoom);
				if (zoom >= minZoom) {
					zoom -= zoomSpeed;
				}
			}
		}
	}
			

	public void HandleScope(bool gunCamOff){

		if(Input.GetKeyDown("left shift") && !isADS){
			AnimateEverything("isADS",true,gunCamOff);
			isADS = true;
			crosshairsOverlay.SetBool ("isADS", true);

		}else if (Input.GetKeyDown("left shift") && isADS){
//			Camera.main.fieldOfView = 73;
			AnimateEverything("isADS",false,gunCamOff);
			isADS = false;
			crosshairsOverlay.SetBool ("isADS", false);
		}

	}

	private void  AnimateEverything(string p, bool b, bool handleGun){
		gun.SetBool(p,b);
		mainCamera.SetBool (p, b);
		if(handleGun) {
			StartCoroutine (GunCamera (p,b));
		}

	}

	private IEnumerator GunCamera(string p,bool b) {
		if (!isADS) yield return new WaitForSeconds (.4f);
		mainCamera.SetBool (p, b);
//		scopeOverlay.SetBool (p, b);
	
	}
	
}
