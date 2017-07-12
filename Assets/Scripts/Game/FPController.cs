/*some serious issues most likely because initialRot is no longer just initial; 
 * i need some way of getting initialRot of a weapon only once (right when the weapon switch occurs)*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPController : MonoBehaviour {

//	[SerializeField]
//	private Animator coltAnim;
//	[SerializeField]
//	private Animator sniperAnim;

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
	public Transform saber;
	Transform gun;


	void Start (){
		cc = GetComponent<CharacterController> ();
		eyes = Camera.main;
		Cursor.lockState = CursorLockMode.Locked;

		gun = rifle;
		initialRot = gun.localRotation; /*woudl be ideal in start but changes depending on weapon*/



	}

	void Update () {
//		initialRot = gun.localRotation;

		/*get WeaponSelection every frame*/
		WeaponSelection weaponSelectionScript = FindObjectOfType<WeaponSelection> ();
		string weaponSelection = weaponSelectionScript.weaponSelection;


		if (weaponSelection == "Rifle") {
			gun = rifle;

		}

		if(weaponSelection == "Pistol"){
			gun = pistol;

		}

		if(weaponSelection == "Saber"){
			gun = saber;

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

		//Gun Sway
		rotationSpeed = Quaternion.Euler (-mouseY, mouseX * .5f, 0);
		gun.localRotation = Quaternion.Slerp (gun.localRotation, rotationSpeed * initialRot, gunSwaySpeed * Time.deltaTime);

	}

}

