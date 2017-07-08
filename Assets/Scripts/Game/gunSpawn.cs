using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunSpawn : MonoBehaviour {

	public GameObject[] gunsToInstantiate;
	public GameObject emptyGun;

	Dictionary<string,GameObject> gunsToInstantiateDick = new Dictionary<string,GameObject> ();

	string gunChoice = AlwaysExist.gameGun;

	void Awake () {
		gunsToInstantiateDick.Add("Holographic",gunsToInstantiate[0]);
		gunsToInstantiateDick.Add("Reflex",gunsToInstantiate[1]);
		gunsToInstantiateDick.Add("Hybrid",gunsToInstantiate[2]);
		gunsToInstantiateDick.Add("ScopedDMR",gunsToInstantiate[3]);
		gunsToInstantiateDick.Add("MP5Irons",gunsToInstantiate[4]);
		gunsToInstantiateDick.Add("M16Irons",gunsToInstantiate[5]);
		gunsToInstantiateDick.Add("BarrettM82",gunsToInstantiate[6]);

		InstantiateYoGunBitch();
	}
	
	void InstantiateYoGunBitch(){
		switch (gunChoice) {
		case "Reflex":
			GameObject Reflex = Instantiate (gunsToInstantiateDick ["Reflex"]);
			Reflex.transform.parent = transform;
			Reflex.transform.localPosition = new Vector3 (0f, 0f, 0f);
			break;

		case "Holographic":
			GameObject Holographic = Instantiate(gunsToInstantiateDick["Holographic"]);
			Holographic.transform.parent = transform;
			Holographic.transform.localPosition = new Vector3 (0f, 0f, 0f);
			break;

		case "Hybrid":
			GameObject Hybrid = Instantiate(gunsToInstantiateDick["Hybrid"]);
			Hybrid.transform.parent = transform;
			Hybrid.transform.localPosition = new Vector3 (0f, 0f, 0f);
			break;

		case "ScopedDMR":
			GameObject ScopedDMR = Instantiate(gunsToInstantiateDick["ScopedDMR"]);
			ScopedDMR.transform.parent = transform;
			ScopedDMR.transform.localPosition = new Vector3 (0f, 0f, 0f);
			break;

		case "MP5Irons":
			GameObject MP5Irons = Instantiate(gunsToInstantiateDick["MP5Irons"]);
			MP5Irons.transform.parent = transform;
			MP5Irons.transform.localPosition = new Vector3 (0f, 0f, 0f);
			break;

		case "M16Irons":
			GameObject M16Irons = Instantiate(gunsToInstantiateDick["M16Irons"]);
			M16Irons.transform.parent = transform;
			M16Irons.transform.localPosition = new Vector3 (0f, 0f, 0f);
			break;

		case "BarrettM82":
			GameObject BarrettM82 = Instantiate(gunsToInstantiateDick["BarrettM82"]);
			BarrettM82.transform.parent = transform;
			BarrettM82.transform.localPosition = new Vector3 (0f, 0f, 0f);
			break;

		}


	}
}
