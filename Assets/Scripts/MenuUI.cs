using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class CustomButton {
	public Image CButton;
	public Sprite Default;
	public Sprite Selected;
	[HideInInspector]
	public string Action;
}


//Use keyboard to select and press button
//buttonlist:
//New Game
//Exit
//About this Game?
public class MenuUI : MonoBehaviour {
	
	public CustomButton StartGame;
    public GameObject cam;

	//public CustomButton About;
	//public CustomButton Exit;

	IList<CustomButton> button_list;
	int button_index;

	void Awake() {
		Cursor.visible = false;
		button_index = 0;
	}

	void Start() {
		//Draw the buttons
		//Draw the frame of the selected button
		button_list = new List<CustomButton>();
		StartGame.Action = "START";
		button_list.Add (StartGame);
		//About.Action = "ABOUT";
		//button_list.Add (About);
		//Exit.Action = "EXIT";
		//button_list.Add (Exit);
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			button_index++;
		}
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			button_index--;
		}
		if (button_index < 0) {
			button_index = 0;
		}
		if (button_index >= button_list.Count) {
			button_index = button_list.Count - 1;
		}

		//Debug.Log ("Button " + button_index + " is on");
		//Draw frame out of the box
		for (int i = 0; i < button_list.Count; i++) {
			if (i == button_index) {
				if (button_list [i].Selected) {
					button_list [i].CButton.sprite = button_list [i].Selected;
				}
			} else {
				if (button_list [i].Default) {
					button_list [i].CButton.sprite = button_list [i].Default;
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			//perform button action
			if(button_list[button_index].Action == "START") {
                //load scene
                //Debug.Log("Start pressed!");
                cam.GetComponent<FadeOutCamera>().enabled = true;
			}
			if(button_list[button_index].Action == "ABOUT") {

				//Debug.Log("About pressed!");
			}
			if(button_list[button_index].Action == "EXIT") {

				//Debug.Log("Exit pressed!");
			}

		}


	}


}
