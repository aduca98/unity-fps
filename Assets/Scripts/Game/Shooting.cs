using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour {

	public float range = 500f;
	public GameObject bulletHole;
	private int shitToShootLayerMask;

//	public Transform emptyOnBarrel;
	public Transform crosshairs;

	private Animator anim;

	public AudioClip shootSound; 
	private AudioSource audio;

	public Image crosshairsImage;

	public float crosshairWidth;
	public float crosshairHeight;

	Recoil recoil;
	public GameObject shell;
	public GameObject emptyGunSpawn;

	void Awake(){
		SaphDMG SaphDMG = FindObjectOfType<SaphDMG>();
		audio = GetComponent<AudioSource>();
		recoil = GetComponent<Recoil>();
	}

	void Start(){
		shitToShootLayerMask = LayerMask.GetMask("ShitToShoot");
		anim = GetComponent<Animator>();

	}

	void Update () {
		/*get isADS value every frame because it will change in different situations*/
		ADS isADSScript = FindObjectOfType<ADS> ();
		bool isADS = isADSScript.isADS;

		if (isADS && Input.GetButtonDown("Fire1")) {
			FireScoped(); 
		}

		if (!isADS && Input.GetButtonDown("Fire1")) {
			FireNoScope(); 
		}
			
	}

	private void FireScoped(){
		recoil.StartRecoil (1f,20f,10f);

		anim.SetTrigger ("isFiring");

		audio.clip = shootSound;
		audio.Play ();

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0f));


		if(Physics.Raycast(ray,out hit,range,shitToShootLayerMask)){
//			Bullethole
//			if(hit.transform.tag == "Target"){
//				Instantiate(bulletHole,hit.positon,hit.Rotation);
//			}
		

			if (hit.transform.tag == "Anime Chick"){
				SaphDMG.Instance.DestroyClothes();

			}

			if (hit.transform.tag == "Target"){
				TargetAnim animScript = hit.transform.GetComponent<TargetAnim> ();
				animScript.TargetAnimation();

				BulletDecal (hit);
			}

			if (hit.transform.tag == "Terrain"){
				BulletDecal (hit);
			}
		}
		Color color = Color.cyan;
		Debug.DrawLine (ray.origin, ray.direction*500f,color);
	}

	private void FireNoScope(){
		recoil.StartRecoil (1f,20f,10f);

		ShellEjection ();

		anim.SetTrigger ("isFiring");

		audio.clip = shootSound;
		audio.Play ();

		Vector3 randomVector = new Vector3 (Random.Range(-12f,12f), Random.Range(-17f,17f), 0f);

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay((new Vector3(Screen.width/2,Screen.height/2,0f))+randomVector);


		if(Physics.Raycast(ray,out hit,range,shitToShootLayerMask)){

			if (hit.transform.tag == "Anime Chick"){
				SaphDMG.Instance.DestroyClothes();

			}

			if (hit.transform.tag == "Target"){
				TargetAnim animScript = hit.transform.GetComponent<TargetAnim> ();
				animScript.TargetAnimation();

				BulletDecal (hit);
			}

			if (hit.transform.tag == "Terrain"){
				BulletDecal (hit);
			}
		}
		Color color = Color.green;
		Debug.DrawLine (ray.origin, ray.direction*500f,color);
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

	void ShellEjection(){
		GameObject shellEjection = Instantiate (shell);
		shellEjection.transform.parent = emptyGunSpawn.transform;
		shellEjection.transform.localPosition = new Vector3 (0.1121173f, 21.3251f, -18.84448f);
		shellEjection.transform.localRotation = Quaternion.Euler (89.98f, 0f, 0f);
		shellEjection.transform.localScale = new Vector3 (0.1564895f, 0.1564895f, 0.1564895f);
		Rigidbody rb = shellEjection.GetComponent<Rigidbody> ();
		rb.velocity = emptyGunSpawn.transform.right * 2 + new Vector3 (0, .8f, 0);
		shellEjection.transform.parent = null;
//		Destroy (shellEjection, 30f);
	}
}
