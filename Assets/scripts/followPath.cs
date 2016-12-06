using UnityEngine;
using System.Collections;

public class followPath : MonoBehaviour {
	Animator anim;
	public Transform[] location;
	public float moveSpeed;
	public bool loop;
	float acceleration = 0.75f;
	
	
	private Vector3 movementVector = Vector3.zero;
	private  bool gravity = false;
	
	Rigidbody rb;
	
	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody>();
		StartCoroutine (route ());
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0, acceleration * Time.deltaTime, 0);

	}
	
	void FixedUpdate(){
		
		if(gravity)
			movementVector.y -=acceleration;
		
		else
			movementVector.y=0;
		
		//rb.velocity = transform.TransformDirection(movementVector);
	}

	IEnumerator route(){
		yield return new WaitForSeconds(3f);
		do {
			foreach(Transform t in location){
				//transform.LookAt(t);

				yield return StartCoroutine(movepos(t));
				//yield return new WaitForSeconds(0f);
			}

		} while(loop);
	}

	IEnumerator movepos(Transform target){
		while (transform.position!=target.position) {
			Vector3 lp = target.position - transform.position;
			transform.LookAt(target);
			//transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime*3);
			transform.position = Vector3.MoveTowards(transform.position, target.position,moveSpeed*Time.deltaTime);
			yield return null; //call me next frame, return here
		}
	}
	
	void OnCollisionEnter (Collision col)
    {
		gravity = false;
	}
	
	void OnCollisionExit(Collision col){
		gravity = true;
	}
}
