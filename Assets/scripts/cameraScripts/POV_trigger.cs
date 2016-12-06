using UnityEngine;
using System.Collections;
//script for changing the point of view of the camera, taking in two public
//cameras as arguments
//attach to a trigger
public class POV_trigger : MonoBehaviour {
	
	public Camera activeCamera;
	public Camera nextCamera;
	
	public Collider trigger;
	
	
	void OnTriggerEnter (Collider col){
		if(col== trigger){
			nextCamera.enabled = !nextCamera.enabled;
			activeCamera.enabled = !activeCamera.enabled;
		}
	}
	/*
	void OnTriggerExit (Collider col){
		if(col.gameObject.tag == "main"){
			nextCamera.enabled = !nextCamera.enabled;
			activeCamera.enabled = !activeCamera.enabled;
		}
	}
	*/
}
