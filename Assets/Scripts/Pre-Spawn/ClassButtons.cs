using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClassButtons : MonoBehaviour {

	public Image emptyClassImages;

	public Sprite[] classImages;

	public Text firstButtonText;

	Dictionary<string,Sprite> classChoiceDick = new Dictionary<string,Sprite> ();

	void Start(){
		classChoiceDick.Add("Holographic",classImages[0]);
		classChoiceDick.Add("Reflex",classImages[1]);
		classChoiceDick.Add("Hybrid",classImages[2]);
		classChoiceDick.Add("ScopedDMR",classImages[3]);
		classChoiceDick.Add("MP5Irons",classImages[4]);
		classChoiceDick.Add("M16Irons",classImages[5]);
		classChoiceDick.Add("BarrettM82",classImages[6]);

		ClassChoice("Reflex");
		firstButtonText.color = Color.red;

//		emptyClassImages.sprite = classImages [1];

		/*start with the CQB (topmost) button already pushed*/

	}


	public void ClassChoice(string button){
		//in a Game scene script, call the classChoice and instantiate the weapon+attachment image accordingly
//		DestroyChildrenHehe ();
		firstButtonText.color = Color.white;
		AlwaysExist.gameGun = button;
		InstTheShit ();
	}

	public void SpawnButton() {
		SceneManager.LoadScene ("Game");
	}
		

	private void InstTheShit(){
		switch (AlwaysExist.gameGun) {
		case "Reflex":
			emptyClassImages.sprite = classImages [1];

			break;

		case "Holographic":
			emptyClassImages.sprite = classImages [0];

			break;

		case "Hybrid":
			emptyClassImages.sprite = classImages [2];

			break;

		case "ScopedDMR":
			emptyClassImages.sprite = classImages [3];

			break;

		case "MP5Irons":
			emptyClassImages.sprite = classImages [4];

			break;

		case "M16Irons":
			emptyClassImages.sprite = classImages [5];

			break;

		case "BarrettM82":
			emptyClassImages.sprite = classImages [6];

			break;
		}
	}
		
	private void DestroyChildren() {
		// Only run code if there are children
		if(transform.childCount > 0) {

			// Destroy all children of attachment
			foreach(Transform child in emptyClassImages.transform) {
				Destroy(child.gameObject);
			}
		}
	}
}