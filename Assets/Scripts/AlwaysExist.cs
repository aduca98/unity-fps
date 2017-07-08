using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysExist : MonoBehaviour {

	// State of the gamegun selected
	public static string gameGun;

	// Make it so persists throughout scenes
	void Awake () {

		DontDestroyOnLoad (transform.gameObject);
	}

}
