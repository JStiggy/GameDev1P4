using UnityEngine;
using System.Collections;

public class StalkCamera : MonoBehaviour {
	
	public GameObject target;
	public float damping = 1f;
	Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position-target.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 newPosition = target.transform.position + offset;
		
		Vector3 position = Vector3.Lerp(transform.position, newPosition,
		damping*Time.deltaTime);
		
		transform.position = position;
		transform.LookAt(target.transform.position);
	}
}
