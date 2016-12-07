using UnityEngine;
using System.Collections;

public class sunCount : MonoBehaviour {
	public float timer = 25f;
	public FadeOutCamera camera;
	
	private float maxTime;
	private float maxI;
	private float percentage;
	
	private Light light;

	// Use this for initialization
	void Start () {
		light = GetComponent<Light>();
		maxI = light.intensity;
		maxTime = timer;
	}
	
	// Update is called once per frame
	//in this case, used to decrease the global light intensity until darkness
	void Update () {
		if(light.intensity>0){
			timer-=Time.deltaTime;
			percentage = timer/maxTime;
			
			light.intensity = maxI*percentage;
		}
		else{
			camera.fadeDir = 1;
		}
	}
}
