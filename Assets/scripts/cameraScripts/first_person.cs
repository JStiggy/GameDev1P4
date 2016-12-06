using UnityEngine;
using System.Collections;

public class first_person : MonoBehaviour {
	
	public GameObject target;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = target.transform.position;
		transform.position = position;
		transform.rotation = target.transform.rotation;
	}
}
