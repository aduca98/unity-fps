using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class SaphDMG : MonoBehaviour {

	public GameObject[] clothes;
	Dictionary<string,GameObject> clothesDick = new Dictionary<string,GameObject> ();


	private static SaphDMG _instance = null;

	public static SaphDMG Instance {
		get {
			if(_instance == null) {
				_instance = new SaphDMG();
			} 
			return _instance;
		}

	}
		
	void Start(){
		clothesDick.Add("skirt",clothes[0]);
		clothesDick.Add("coattails",clothes[1]);
		clothesDick.Add("collar",clothes[2]);
		clothesDick.Add("skirt2",clothes[3]);
		clothesDick.Add("shirt",clothes[4]);
		clothesDick.Add("coattails2",clothes[5]);
	}

	public void DestroyClothes (){
		print ("Please please please");
		Destroy (clothesDick ["skirt"]);
		Destroy (clothesDick ["coattails"]);
		Destroy (clothesDick ["collar"]);
		Destroy (clothesDick ["skirt2"]);
		Destroy (clothesDick ["shirt"]);
		Destroy (clothesDick ["coattails2"]);
	}
}
