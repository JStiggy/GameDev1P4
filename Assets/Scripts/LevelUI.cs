using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelUI : MonoBehaviour {
	//Template for making ui elemnets;
	//Including:
	//Text
	//Image
	//Button

	public Text def;
	public Button but;
	public Image img;

	public ParticleSystem prt;

	void Start() {
		def.alignment = TextAnchor.UpperLeft;
		def = (Text)Instantiate (def);
		def.transform.SetParent (this.transform, false);
		def.rectTransform.position = new Vector3(160f, 0f, 0f);
		//def.transform.position = new Vector3 (200f, 150f, 0f);

		//but.OnPointerClick(
		//SceneManager.LoadScene("scene1");


		Debug.Log (def.alignment);

		Debug.Log ("Level: " + SceneManager.sceneCount);
		Debug.Log ("Position: " + def.transform.position);

	}


}

