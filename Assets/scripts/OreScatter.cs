using UnityEngine;
using System.Collections;

public class OreScatter : MonoBehaviour {
	public float thrust = 200;
	// Use this for initialization
	void Start () {
		Vector3 direction = new Vector3(
		Random.Range( 0.1f, 1.0f ),
		Random.Range( 0.1f, 1.0f ),
		Random.Range( 0.1f, 1.0f )
		);
		print(direction*thrust);
		GetComponent<Rigidbody>().AddForce(direction*thrust);
	}
}
