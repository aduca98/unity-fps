using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttachmentsEnum {
	Holographic,
	Reflex,
	Hybrid,
	Scoped,
	MP5Irons,
	M16Irons,
}

public class Attachments : MonoBehaviour {
	

	public GameObject[] attachments;
	public AttachmentsEnum currentEnum;

	Dictionary<string,GameObject> attachmentsDick = new Dictionary<string,GameObject> ();


	void Start () {
		attachmentsDick.Add("Holographic",attachments[0]);
		attachmentsDick.Add("Reflex",attachments[1]);
		attachmentsDick.Add("Hybrid",attachments[2]);
		attachmentsDick.Add("Scoped",attachments[3]);
		attachmentsDick.Add("MP5Irons",attachments[4]);
		attachmentsDick.Add("M16Irons",attachments[5]);
//		attachmentsDick.Add("Foregrip",attachments[6]);
//		attachmentsDick.Add("Suppressed",attachments[7]);
//		attachmentsDick.Add("Laser",attachments[8]);
	}
	

	void Update () {
		if(Input.GetKeyDown("1")) {
			DestroyChildren ();
//			CreateSight (AttachmentsEnum.Holographic, "Holographic");
			currentEnum = AttachmentsEnum.Holographic;
			GameObject sight = Instantiate(attachmentsDick["Holographic"],transform.position,transform.rotation);
			sight.transform.parent = transform;
			sight.transform.localPosition = new Vector3 (-.3f,-.04f,0f);
		}
			
		if(Input.GetKeyDown("2")){
			DestroyChildren ();
//			CreateSight (AttachmentsEnum.Reflex, "Reflex");
			currentEnum = AttachmentsEnum.Reflex;
			GameObject sight = Instantiate(attachmentsDick["Reflex"],transform.position + new Vector3(-.108f,-.01f,-.1f),transform.rotation);
			sight.transform.parent = transform;
			sight.transform.localPosition = new Vector3 (.28f,-.06f,.4f);
			sight.transform.localRotation = Quaternion.Euler (0f, 0f, -.5f);
		}
			
		if (Input.GetKeyDown ("3")) {
			DestroyChildren ();
//			CreateSight(AttachmentsEnum.Hybrid, "Hybrid");
			currentEnum = AttachmentsEnum.Hybrid;
			GameObject sight = Instantiate(attachmentsDick["Hybrid"],transform.position + new Vector3(0f,0f,0f),transform.rotation);
			sight.transform.parent = transform;
			sight.transform.localPosition = new Vector3 (0,0,0);
		}

		if (Input.GetKeyDown ("4")) {
			DestroyChildren ();
//			CreateSight(AttachmentsEnum.Scoped, "Scoped");
			currentEnum = AttachmentsEnum.Scoped;
			GameObject sight = Instantiate(attachmentsDick["Scoped"],transform.position + new Vector3(0f,0f,0f),transform.rotation);
			sight.transform.parent = transform;
			sight.transform.localPosition = new Vector3 (-.6f,0f,.06f);
			sight.transform.localRotation = Quaternion.Euler (0f, 90f, 0f);
		}

		if (Input.GetKeyDown ("5")) {
			DestroyChildren ();
//			CreateSight(AttachmentsEnum.MP5Irons, "MP5Irons");
			currentEnum = AttachmentsEnum.MP5Irons;
			GameObject sight = Instantiate(attachmentsDick["MP5Irons"],transform.position + new Vector3(0f,0f,0f),transform.rotation);
			sight.transform.parent = transform;
			sight.transform.localPosition = new Vector3 (-11f,.9f,-.72f);
			sight.transform.localRotation = Quaternion.Euler (0f, 179f, 0f);
		}

		if (Input.GetKeyDown ("6")) {
			DestroyChildren ();
			//CreateSight(AttachmentsEnum.M16Irons, "M16Irons");
			currentEnum = AttachmentsEnum.M16Irons;
			GameObject sight = Instantiate(attachmentsDick["M16Irons"],transform.position + new Vector3(0f,0f,0f),transform.rotation);
			sight.transform.parent = transform;
			sight.transform.localPosition = new Vector3 (2f,.9f,.4f);
			sight.transform.localRotation = Quaternion.Euler (-2f, 90f, 0f);
		}
	}

	/// <summary>
	/// Destroys the children of the attachments component.
	/// </summary>
	private void DestroyChildren() {
		// Only run code if there are children
		if(transform.childCount > 0) {

			// Destroy all children of attachment
			foreach(Transform child in this.transform) {
				Destroy(child.gameObject);
			}
		}
	}

	private void MakeScope(AttachmentsEnum currentEnum, string sightName, Vector3 worldV, Vector3 localV, Quaternion localQ) {
		DestroyChildren ();
		currentEnum = currentEnum;
		GameObject sight = Instantiate (attachmentsDick [sightName], transform.position + worldV, transform.rotation);
		sight.transform.parent = transform;
		sight.transform.localPosition = localV;
		sight.transform.localRotation = localQ;
	}


	/// <summary>
	/// Instantiate Sight and make child of attachment game object
	/// </summary>
	/// <param name="attach">AttachmentEnum to keep state</param>
	/// <param name="key">Key for dictionary</param>

//	private void CreateSight(AttachmentsEnum attach, string key) {
//		currentEnum = attach;
//		GameObject sight = Instantiate<GameObject> (attachmentsDick [key]);
//		sight.transform.parent = transform;
//		sight.transform.localPosition = Vector3.zero;
//	}

	/// <summary>
	/// Get the current sight state
	/// </summary>
	
//public AttachmentsEnum State() {
//		return currentEnum;
//	}

}
