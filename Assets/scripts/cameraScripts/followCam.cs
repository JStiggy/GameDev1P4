using UnityEngine;
using System.Collections;

public class followCam : MonoBehaviour {
	
	public Transform target;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void LateUpdate(){
		transform.LookAt(target.transform);
	}
}
