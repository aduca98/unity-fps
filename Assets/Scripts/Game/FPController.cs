/*if game is not being played (just in scene/game view) take out/put away animations are fine, but when playing the rotations do weird things, probably because of sway shit
 * problem is we always want sway EXCEPT when the two animations are running and when we are ADSing 
 * can't set initialRot = gun.localRotation anymore because gun is being updated every frame
 * when i set initialRot to a constant value there is no sway, which is weird because it is set in start anyway it should be a constant value
 * in order to figure out when the animation is over i used an "indicator" state in the animator controller so i could use an indicator bool (isDone) 
 * I made an indictor script with a fxn that sets the indicator bool and that fxn is called by an animation event*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPController : MonoBehaviour {

	[SerializeField]
	private Animator coltAnim;
	[SerializeField]
	private Animator sniperAnim;

	public float mouseSensitivity = 3f;
	public float jumpHeight = 3f;
	public float moveSpeed = 2f;

	private float verticalPosition = 0f;
	private CharacterController cc;
	private Camera eyes;
	private float maxAngle = 60f;
	private float verticalVelocity = 0f;

	float playerSpeed = 2f;

	//Gun Sway
	Quaternion rotationSpeed;
	public float gunSwaySpeed = 15;
	Quaternion initialRot;
	public Transform rifle;
	public Transform pistol;
	Transform gun;

	bool isADS;
	bool isAnim;


	void Start (){
		cc = GetComponent<CharacterController> ();
		eyes = Camera.main;
		Cursor.lockState = CursorLockMode.Locked;

//		initialRot = gun.localRotation; /*woudl be ideal in start but changes depending on weapon*/

		initialRot = Quaternion.Euler (1, 1, 1);

	}

	void Update () {
		/*get WeaponSelection every frame*/
		WeaponSelection weaponSelectionScript = FindObjectOfType<WeaponSelection> ();
		string weaponSelection = weaponSelectionScript.weaponSelection;


		if (weaponSelection == "Rifle") {
			gun = rifle;

			/*get isADS value every frame because it will change in different situations*/
			SniperADSOverlay isADSScript = FindObjectOfType<SniperADSOverlay> ();
			bool isADSSniper = isADSScript.isADS;
			/*SniperADSDualRender isADSScript1 = FindObjectOfType<SniperADSDualRender> ();*/
			/*bool isADS1 = isADSScript1.isADS;*/

			bool isDone = sniperAnim.GetBool("isDone");

			isADS = isADSSniper;
			isAnim = isDone;
		}
		if(weaponSelection == "Pistol"){
			gun = pistol;

			/*get isADS value every frame because it will change in different situations*/
			ColtADS isADSScript = FindObjectOfType<ColtADS> ();
			bool isADSPistol = isADSScript;

			bool isDone = coltAnim.GetBool("isDone");

			isADS = isADSPistol;
			isAnim = isDone;
		}

		float fwdSpeed = Input.GetAxis ("Vertical") * moveSpeed;
		float sideSpeed = Input.GetAxis ("Horizontal") *moveSpeed;

		float mouseX = Input.GetAxis ("Mouse X") * mouseSensitivity;
		float mouseY = Input.GetAxis ("Mouse Y") * mouseSensitivity;

		transform.Rotate (0, mouseX, 0);

		//clamping look up/down
		verticalPosition -= mouseY;
		verticalPosition = Mathf.Clamp (verticalPosition, -maxAngle, maxAngle);
		eyes.transform.localRotation = Quaternion.Euler (verticalPosition, 0, 0);


		verticalVelocity = verticalVelocity + Physics.gravity.y * Time.deltaTime;

		if(cc.isGrounded && Input.GetButtonDown("Jump")){
			verticalVelocity = jumpHeight;

		}
			
		Vector3 playerSpeed = new Vector3 (sideSpeed, verticalVelocity, fwdSpeed);
		playerSpeed = transform.rotation * playerSpeed;
		cc.Move (playerSpeed * Time.deltaTime);

		if (!isADS && isAnim) { //Gun Sway
			rotationSpeed = Quaternion.Euler (-mouseY, mouseX * .5f, 0);
			gun.localRotation = Quaternion.Slerp (gun.localRotation, rotationSpeed * initialRot, gunSwaySpeed * Time.deltaTime);
		}

		if (isADS || !isAnim ){
			gun.localRotation = Quaternion.Euler (2, 0f, 0f); /*no sway*/
		}
	}

}

