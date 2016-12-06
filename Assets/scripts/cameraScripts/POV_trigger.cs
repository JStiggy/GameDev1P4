using UnityEngine;
using System.Collections;
//script for changing the point of view of the camera, taking in two public
//cameras as arguments
//attach to a trigger
public class POV_trigger : MonoBehaviour {
	
	public Camera camera1;
	public Camera camera2;
	
	
	void OnTriggerEnter (Collider col){
		if(col.gameObject.tag == "main"){
			camera2.enabled = !camera2.enabled;
			camera1.enabled = !camera1.enabled;
		}
	}
	
	void OnTriggerExit (Collider col){
		if(col.gameObject.tag == "main"){
			camera2.enabled = !camera2.enabled;
			camera1.enabled = !camera1.enabled;
		}
	}
}
