using UnityEngine;
using System.Collections;

public class guard : MonoBehaviour {

	Animator anim;
	public Transform[] location;
	public float moveSpeed;
	public bool loop;

	// Use this for initialization
	void Start () {
		StartCoroutine (do_lol ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator do_lol(){
		do {
			foreach(Transform t in location){
				yield return StartCoroutine(movepos(t));
				yield return new WaitForSeconds(3f);
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
