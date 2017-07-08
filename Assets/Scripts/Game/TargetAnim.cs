using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAnim : MonoBehaviour {

	private Animator target;
//	int isHit = Animator.StringToHash("isHit");

	void Awake(){

	}

	void Start(){
		target = GetComponent<Animator>();
	}


	public void TargetAnimation(){
		target.SetTrigger ("isHit");
		print ("Hell yeah good shit");
	}
}
