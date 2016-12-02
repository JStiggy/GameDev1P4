using UnityEngine;
using System.Collections;

public class TorchScript : MonoBehaviour {
	
	public float intensity = 1.0f;
	public float minIntensity = 0;
	public float duration =  5;
	
	public Light light;
	
	float phi;
	float amplitude;

	// Use this for initialization
	void Start () {
		intensity = light.intensity;
	}
	
	// Update is called once per frame
	void Update () {
		
		phi = Time.time/duration*2*Mathf.PI;
		amplitude = Mathf.Cos(phi) * 0.5f + 1.5f;
		light.intensity = amplitude;
		//if(light.intensity>0)
		//	light.intensity -=0.01f;
	}
}
