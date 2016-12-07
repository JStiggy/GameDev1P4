using UnityEngine;
using System.Collections;

public class POV_1st : MonoBehaviour {

	//public Camera activeCamera;
   // public Camera nextCamera;

    public Collider trigger;
	
	public Light sun1;
	public Light sun2;
	public Light sun3;


    void OnTriggerEnter(Collider col)
    {
        if (col == trigger)
        {
            //nextCamera.enabled = !nextCamera.enabled;
            //activeCamera.enabled = !activeCamera.enabled;
			
			sun1.enabled = !sun1.enabled;
			sun2.enabled = !sun2.enabled;
			sun3.enabled = !sun3.enabled;
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
