using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColtADS : MonoBehaviour {

	[SerializeField]
	private Animator gun; 
	[SerializeField]
	private Animator crosshairsOverlay;

	public bool isADS; /*are you ADSing or not—true or false*/

	void Start(){
		isADS = false;
	}

	public void Update () {
		if(Input.GetKeyDown("left shift") && !isADS){
			isADS = true;
			crosshairsOverlay.SetBool ("isADS", true);
			gun.SetBool ("isADS", true);

		}else if (Input.GetKeyDown("left shift") && isADS){
			isADS = false;
			crosshairsOverlay.SetBool ("isADS", false);
			gun.SetBool ("isADS", false);
		}
	}
}