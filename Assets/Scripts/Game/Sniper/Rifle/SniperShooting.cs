using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Shit to fix:
		Recoil rotation shit
		ShellEjection lerping
		Reloading Lerping
*/


public class SniperShooting : MonoBehaviour {

	[SerializeField]
	private Animator bolt;
	bool isFired = false; /*for the timer to know when to run*/
	bool canFire = true; /* canFire = !isFired...ALWAYS */
	bool hasBullets = true;
	bool reloading = false;
	bool isEjected = false;

	public GameObject gun;
	public GameObject round;
	public GameObject shell;

	int bulletCount = 5;

	public float range = 1000.00f;
	public GameObject bulletHole;
	private int shitToShootLayerMask;
	private int insidesLayerMask;
	private int outsidesLayerMask;

	public float[] secondShootPointsZ;
	int i = 0;
	public GameObject secondShootPoint;
	public GameObject reticle;

	public AudioClip shootSound; 
	private AudioSource audio;

	public Text rangeHUDText;
	public Text distanceSettingHUDText;
	public Text bulletCountHUDText;

	public GameObject recoilEmptyShit;

	bool isTooClose = false;

	float timerForReload = 0f;
	float timerForCock = 0f;
	float timerForReloading = 0f;

	public ParticleSystem gunMuzzleFlash;
	public ParticleSystem explosion;
	public ParticleSystem blood;

	public int integer;

	public GameObject targetReal;
	bool isElsewhereInside = false;
	bool isElsewhereOutside = false;

	Recoil recoil;

	void Awake(){
		audio = GetComponent<AudioSource>();
		recoil = recoilEmptyShit.GetComponent<Recoil>();
		bulletCountHUDText.text = bulletCount.ToString();
	}

	void Start(){
		shitToShootLayerMask = LayerMask.GetMask("ShitToShoot");
		insidesLayerMask = LayerMask.GetMask("Insides");
		outsidesLayerMask = LayerMask.GetMask("Outsides");

		distanceSettingHUDText.text = "Distance Setting: " + secondShootPointsZ [i];
	}

	void Update () {
		Reload ();
		if ((timerForCock > .88f) && !isEjected) {
			ShellEjection ();
			isEjected = true;
		}
		if(reloading){
			timerForReloading += Time.deltaTime;
			if (timerForReloading > 1f) timerForReloading = 0;
		}


		if (isFired && (timerForCock < 1.3f || timerForReload < 5f)) {
			TimerForReloadingAndCocking ();
		}
		if(!reloading && timerForCock >= 1.8f){
			print ("slerp my balls");
			isFired = false;
			canFire = true;
			isEjected = false;
			timerForCock = 0f;
			timerForReload = 0f;
		}
		if (Input.GetKeyDown ("x") && i<(secondShootPointsZ.Length-1)) {
			/*changing distance setting + */
			i++;
			distanceSettingHUDText.text = "Distance Setting: " + secondShootPointsZ[i];
		}

		if (Input.GetKeyDown ("z") && i>0) {
			/*changing distance setting - */
			i--;
			distanceSettingHUDText.text = "Distance Setting: " + secondShootPointsZ[i];
		}

		/*get isADS value every frame because it will change in different situations*/
		SniperADSOverlay isADSScript = FindObjectOfType<SniperADSOverlay> ();
		bool isADS = isADSScript.isADS;
		/*calculating and displaying range*/
		if (isADS){
		/*Range shit*/
			rangeHUDText.color = new Color(1f,1f,1f,1f);
			distanceSettingHUDText.color = new Color(1f,1f,1f,1f);
			bulletCountHUDText.color = new Color (1f,1f,1f,0f);
//			
			RaycastHit hit;
			Ray rangeRay = Camera.main.ScreenPointToRay((new Vector3(Screen.width/2,Screen.height/2,0f)));
			if (Physics.Raycast (rangeRay,out hit,1000.00f)) {
				rangeHUDText.text = "Range: " + hit.distance;
				if (hit.distance < 10){
					isTooClose = true;
				}else{ 
					isTooClose = false;
				}
			}
		}else{
			rangeHUDText.color = new Color(1f,1f,1f,0f);
			distanceSettingHUDText.color = new Color(1f,1f,1f,0f);
			bulletCountHUDText.color = new Color (.5f,.2f,1f,1f);
				
		}

		if (Input.GetButtonDown("Fire1") && reloading && bulletCount>0){ /*stop reload at any time, but finish insrting the current round before canFire*/
			reloading = false;
			bolt.SetBool ("isReloading", false);
			canFire = true;
			timerForReloading = 0;
			return;
		}

		if (Input.GetButtonDown("Fire1") && canFire){
			isFired = true;
			canFire = false;

			if (bulletCount > 0) {
				bolt.SetTrigger ("isFired");
				gunMuzzleFlash.Play();
				recoil.StartRecoil (1f,20f,10f);
				bulletCount--;
//				bolt.SetBool ("hasBullets",true);
				if (isADS) {
					FireScoped ();
				} else {
					FireNoScope (); 
				}
			}
			/*if (bulletCount == 0){
//				bolt.SetBool ("hasBullets",false);
//				hasBullets = false;
			}*/
		}
	}
		

	private void FireScoped(){
/*by increasing the translation on the firstShootPoint you are increasing the effect of shooting at the wrong range setting—
  no matter what the translation is, at the correct range the scope will be zeroed; keep secondShootPoint (0,0,distance setting) */		

		audio.PlayOneShot (shootSound, .7f);

		if(!isTooClose){
			secondShootPoint.transform.localPosition = new Vector3(0,0,secondShootPointsZ[i]);
		
			float randomX = Random.Range(-1.2f,1.2f); 
			Vector3 firstShootPoint = new Vector3(Camera.main.transform.position.x+randomX,Camera.main.transform.position.y+1f, Camera.main.transform.position.z);
			RaycastHit hit;
			Ray ray = new Ray ();
			ray.origin = firstShootPoint;
			ray.direction = secondShootPoint.transform.position-firstShootPoint;
		
			RayCastHitShit(ray);
			
		}else{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay((new Vector3(Screen.width/2,Screen.height/2,0f)));

			RayCastHitShit(ray);
			
		}
			
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
		
	void TimerForReloadingAndCocking(){
		if (/*hasBullets*/ bulletCount <= 4) {
			timerForCock += Time.deltaTime;
		}else
		/*if (!hasBullets)*/ {
			timerForReload += Time.deltaTime;
		}
	}

	void Reload(){
		if (bulletCount == 0){
			bolt.SetBool ("isReloading", true);
			GameObject reloadedRound = Instantiate (round);
			reloadedRound.transform.parent = gun.transform;
//			stick localPosition/Rotation/Scale here
//			move round into gun (new localPosition)
//			move round down into integrated magazine (new localPosition)
			Destroy (reloadedRound, 1f);
			++bulletCount;
//			hasBullets = true;
			reloading = true;
		}
		if((/*bulletCount == 0 ||*/ Input.GetKeyDown("r") || reloading) && bulletCount < 5 && timerForReloading == 0){
			bolt.SetBool ("isReloading", true);
			GameObject reloadedRound = Instantiate (round);
			reloadedRound.transform.parent = gun.transform;
//			stick localPosition/Rotation/Scale here
//			move round into gun (new localPosition)
//			move round down into integrated magazine (new localPosition)
			Destroy (reloadedRound, 1f);
			++bulletCount;
//			hasBullets = true;
			reloading = true;
			if(bulletCount == 5){
				StartCoroutine (waitForReload());
			}
		}
		bulletCountHUDText.text = "Bullets: " + bulletCount;
	}


	IEnumerator waitForReload(){
		yield return new WaitForSeconds (.8f);
		reloading = false;
		bolt.SetBool ("isReloading", false);
		timerForReload = 0;
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

				object[] tempStorage = new object[4]{"Heart", 100, hit.point, hit.normal.normalized};

				hit.transform.gameObject.SendMessageUpwards("FuuuuuckIDoneGotHit", tempStorage);

				//pass: Heart,100
				isElsewhereInside = true;
			}else{
				isElsewhereOutside = false;
			}

			if ((hit.transform.tag == "Lung") && !isElsewhereOutside){

				object[] tempStorage = new object[4]{"Lung", 50, hit.point,hit.normal.normalized};

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


