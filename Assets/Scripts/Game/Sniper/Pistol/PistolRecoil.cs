using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolRecoil : MonoBehaviour {

	private float recoil;
	    private float maxRecoil_x;
	    private float maxRecoil_y;
	    private float recoilSpeed;
	public void StartRecoil (float recoilParam, float maxRecoil_xParam, float recoilSpeedParam)
	    {
	        recoil = recoilParam;
	        maxRecoil_x = -maxRecoil_xParam;
	        recoilSpeed = recoilSpeedParam;
//		    maxRecoil_y = Random.Range(-maxRecoil_xParam, maxRecoil_xParam);
			maxRecoil_y = maxRecoil_xParam;
	    }

	    private void Recoiling ()
	    {
		        if (recoil > 0f) {
			            Quaternion maxRecoil = Quaternion.Euler (maxRecoil_x, maxRecoil_y, 0f);
			            // Dampen towards the target rotation
			            transform.localRotation = Quaternion.Slerp (transform.localRotation, maxRecoil, Time.deltaTime * recoilSpeed);
			            recoil -= Time.deltaTime * 10;
		        } else {
			            recoil = 0f;
			            // Dampen towards the target rotation
			            transform.localRotation = Quaternion.Slerp (transform.localRotation, Quaternion.identity, Time.deltaTime * recoilSpeed / 2);
			        }
		    }

	    void Update ()
	    {
		        Recoiling ();
		    }

}
