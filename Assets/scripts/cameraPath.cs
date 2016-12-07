using UnityEngine;
using System.Collections;

public class cameraPath : MonoBehaviour {

public Transform[] location;
	public float moveSpeed;
	public bool loop;

	// Use this for initialization
	void Start () {
		StartCoroutine (route ());
	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator route(){
		do {
			foreach(Transform t in location){
				yield return StartCoroutine(movepos(t));
				//yield return new WaitForSeconds(0f);
			}

		} while(loop);
	}

	IEnumerator movepos(Transform target){
		while (transform.position!=target.position) {
			transform.position = Vector3.MoveTowards(transform.position, target.position,moveSpeed*Time.deltaTime);
			yield return null; //call me next frame, return here
		}
	}
}
