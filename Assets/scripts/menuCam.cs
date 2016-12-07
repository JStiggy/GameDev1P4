using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class menuCam : MonoBehaviour {
	float intensity = 0;
	float time = 0;
	// Use this for initialization
	void Start () {
		intensity = GetComponent<BloomOptimized>().intensity;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(intensity>0.17 && time>1.5){
			GetComponent<BloomOptimized>().intensity = intensity;
			intensity-=0.05f;
		}
		time+=Time.deltaTime;
	}
}
