using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColtShooting : MonoBehaviour {

	[SerializeField]
	private Animator gunBlowTrigger;
	[SerializeField]
	private Animator gunParent;


	bool canFire = true; 
	bool reloading = false;
	bool isEjected = false;

	public GameObject gun;
	public GameObject round;
	public GameObject shell;

	int bulletCount = 8;

	public GameObject bulletHole;
	private int shitToShootLayerMask;
	private int insidesLayerMask;
	private int outsidesLayerMask;

	public AudioClip shootSound; 
	private AudioSource audio;

	public Text bulletCountHUDText;

	public GameObject recoilEmptyShit;
	public GameObject recoilEmtpyPistol;

	float timerForReload = 0f;

	public ParticleSystem gunMuzzleFlash;

	public int integer;

	public GameObject targetReal;
	bool isElsewhereInside = false;
	bool isElsewhereOutside = false;

	public Transform rearShootPoint;
	public Transform frontShootPoint;

	Recoil recoil;
	PistolRecoil pistolRecoil;

	void Awake(){
		audio = GetComponent<AudioSource>();
		recoil = recoilEmptyShit.GetComponent<Recoil>();
		pistolRecoil = recoilEmtpyPistol.GetComponent<PistolRecoil>();
	}

	void Start(){
		shitToShootLayerMask = LayerMask.GetMask("ShitToShoot");
		insidesLayerMask = LayerMask.GetMask("Insides");
		outsidesLayerMask = LayerMask.GetMask("Outsides");
		bulletCountHUDText.text = bulletCount.ToString();

	}

	void Update () {
		Reload ();
		CanFireDictate ();
	
		/*get isADS value every frame because it will change in different situations*/
		ColtADS isADSScript = FindObjectOfType<ColtADS> ();
		bool isADS = isADSScript.isADS;

		if (Input.GetButtonDown("Fire1") && canFire){
			ShellEjection ();
			gunBlowTrigger.SetTrigger ("isFired");
			gunMuzzleFlash.Play();
			recoil.StartRecoil (1f,20f,10f);
			pistolRecoil.StartRecoil (1f, 2f, 10f);
			bulletCount--;
			bulletCountHUDText.text = bulletCount.ToString ();
			if (isADS) {
				FireScoped ();
			} else {
				FireNoScope (); 
			}
		}
	}


	private void FireScoped(){
		audio.PlayOneShot (shootSound, .7f);

		/*RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay((new Vector3(Screen.width/2,Screen.height/2,0f)));

		RayCastHitShit(ray);*/
		RaycastHit hit;
		Ray ray = new Ray ();
		ray.origin = rearShootPoint.position;
		ray.direction = frontShootPoint.position - rearShootPoint.position;
		RayCastHitShit (ray);


	}

	private void FireNoScope(){
		audio.PlayOneShot (shootSound, .7f);

		Vector3 randomVector = new Vector3 (Random.Range(-12f,12f), Random.Range(-17f,17f), 0f);

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay((new Vector3(Screen.width/2,Screen.height/2,0f))+randomVector);

		RayCastHitShit(ray);

	}

	public void BulletDecal(RaycastHit hit){
		Vector3 normal = hit.normal.normalized;
		Vector3 hitPoint = hit.point+(normal*.01f);
		GameObject target = hit.transform.gameObject;
		Quaternion hitRotation = Quaternion.FromToRotation (Vector3.up, normal);

		GameObject bullet = Instantiate (bulletHole, hitPoint, hitRotation);
		bullet.transform.parent = target.transform;

		Destroy (bullet, 10f);

	}
		

	void Reload(){
		if (bulletCount == 0){
			reloading = true;
			gunParent.SetBool ("isReloading", true);
			GameObject reloadedRound = Instantiate (round);
			reloadedRound.transform.parent = gun.transform;
			//			stick localPosition/Rotation/Scale here
			//			move round into gun (new localPosition)
			//			move round down into integrated magazine (new localPosition)
			Destroy (reloadedRound, 1f);
			bulletCount = 8;
		}
		if (Input.GetKeyDown ("r") && bulletCount <= 7 && bulletCount != 0) {
			reloading = true;
			gunParent.SetBool ("isReloading", true);
			GameObject reloadedRound = Instantiate (round);
			reloadedRound.transform.parent = gun.transform;
			//			stick localPosition/Rotation/Scale here
			//			move round into gun (new localPosition)
			//			move round down into integrated magazine (new localPosition)
			Destroy (reloadedRound, 1f);
			bulletCount = 9;
		}

//		bulletCountHUDText.text = bulletCount;
	}

	void CanFireDictate (){
		if (reloading) {
			canFire = false;
			timerForReload += Time.deltaTime;
			if(bulletCount == 9){
				if (timerForReload >= 2f) {
					bulletCountHUDText.text = bulletCount.ToString ();
					canFire = true;
					timerForReload = 0;
					reloading = false;
				}
			}
			if(bulletCount == 8){
				if (timerForReload >= 2.2f) { /*account for chambering round*/
					bulletCountHUDText.text = bulletCount.ToString ();
					canFire = true;
					timerForReload = 0;
					reloading = false;
				}
			}
		}
	}
		

	void ShellEjection(){
		GameObject shellEjection = Instantiate (shell);
		shellEjection.transform.parent = gun.transform;
		shellEjection.transform.localPosition = new Vector3 (0.1121173f, 21.3251f, -18.84448f);
		shellEjection.transform.localRotation = Quaternion.Euler (89.98f, 0f, 0f);
		shellEjection.transform.localScale = new Vector3 (0.1564895f, 0.1564895f, 0.1564895f);
		Rigidbody rb = shellEjection.GetComponent<Rigidbody> ();
		rb.velocity = gun.transform.right * 2 + new Vector3(0,.8f,0);
		shellEjection.transform.parent = null;
		Destroy (shellEjection, 30f);

		if (bulletCount > 1) {
			GameObject newRoundChambered = Instantiate (round);
			newRoundChambered.transform.parent = gun.transform;
			newRoundChambered.transform.localPosition = new Vector3 (6.3083f, 17.71f, -14.18f); /*start position*/
			/*would like to lerp between the point on line 276 and line 278 (just a change in y coordinates)*/
			/* newRoundChambered.transform.localPosition = new Vector3 (6.3083f, 19.78f, -14.18f); mid position*/
			/*would like to lerp between the point on line 278 and line 280 (NEEDS to line up with bolt animation)*/
			/* newRoundChambered.transform.localPosition = new Vector3 (6.3083f, 20.15f, -3.42f); end position*/
			newRoundChambered.transform.localRotation = Quaternion.Euler (90f, 0f, 0f);
			newRoundChambered.transform.localScale = new Vector3 (0.1564895f, 0.1564895f, 0.1564895f);
			Destroy (newRoundChambered, 30f);
		}

	}

	void RayCastHitShit(Ray ray){

		RaycastHit hit;

		if(Physics.Raycast(ray,out hit,1000f,shitToShootLayerMask)){

			if (hit.transform.tag == "Target"){
				TargetAnim animScript = hit.transform.GetComponent<TargetAnim> ();
				animScript.TargetAnimation();
				BulletDecal (hit);
			}

			if (hit.transform.tag == "Terrain"){
				BulletDecal (hit);
			}
		}

		if(Physics.Raycast(ray,out hit,1000f,insidesLayerMask)){
			/*isElsewhere stops you from getting hit in the heart/eye/brain/lung and having it count as __ + miscbody*/
			if ((hit.transform.tag == "Heart") && !isElsewhereOutside){

				object[] tempStorage = new object[4]{"Heart", 85, hit.point, hit.normal.normalized};

				hit.transform.gameObject.SendMessageUpwards("FuuuuuckIDoneGotHit", tempStorage);

				//pass: Heart,100
				isElsewhereInside = true;
			}else{
				isElsewhereOutside = false;
			}

			if ((hit.transform.tag == "Lung") && !isElsewhereOutside){

				object[] tempStorage = new object[4]{"Lung", 40, hit.point,hit.normal.normalized};

				hit.transform.gameObject.SendMessageUpwards("FuuuuuckIDoneGotHit", tempStorage);

				//pass: Lung,50
				isElsewhereInside = true;
			}else{
				isElsewhereOutside = false;
			}

			if ((hit.transform.tag == "Brain") && !isElsewhereOutside){

				object[] tempStorage = new object[4]{"Brain", 80,hit.point,hit.normal.normalized};

				hit.transform.gameObject.SendMessageUpwards("FuuuuuckIDoneGotHit", tempStorage);

				//pass: Brain,80
				print("Brain");
				isElsewhereInside = true;
			}else{
				isElsewhereOutside = false;
			}


		}

		if(Physics.Raycast(ray,out hit,1000f,outsidesLayerMask)){

			if (hit.transform.tag == "Eye"){

				object[] tempStorage = new object[4]{"Eye", 10,hit.point,hit.normal.normalized};

				hit.transform.gameObject.SendMessageUpwards("FuuuuuckIDoneGotHit", tempStorage);

				//pass: Eye,10
			}

			if (hit.transform.tag == "Tiara"){

				object[] tempStorage = new object[4]{"Tiara", 0,hit.point,hit.normal.normalized};

				hit.transform.gameObject.SendMessageUpwards("FuuuuuckIDoneGotHit", tempStorage);

				//pass: Tiara,0
				isElsewhereOutside = true;
			}

			if ((hit.transform.tag == "MiscBody") && !isElsewhereInside){

				object[] tempStorage = new object[4]{"MiscBody", 20,hit.point,hit.normal.normalized};

				hit.transform.gameObject.SendMessageUpwards("FuuuuuckIDoneGotHit", tempStorage);

				//pass: MiscBody,20
			}else{
				isElsewhereInside = false;
			}

		}	

		Color color = Color.cyan;
		Debug.DrawLine (ray.origin, ray.direction*1000f,color);

	} 

}