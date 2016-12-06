using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class CameraController : MonoBehaviour {

    public Transform target;

    [System.Serializable]
    public class PositionSettings
    {
        public Vector3 targetPosOffset = new Vector3(0, 1.2f, 0);
        public float lookSmooth = 100f;
        public float zoomSmooth = 100f;
        public float distanceFromtarget = -8f;
        public float maxZoom = -2f;
        public float minZoom = -15f;
    }

    [System.Serializable]
    public class OrbitSettings
    {
        public float xRotation = -20;
        public float yRotation = -180;
        public float maxXRotation = 25;
        public float minXRotation = -85;
        public float vOrbitSmooth = 150;
        public float hOrbitSmooth = 150;
    }
    public PositionSettings position = new PositionSettings();
    public OrbitSettings orbit = new OrbitSettings();

    Vector3 targetPos = Vector3.zero;
    Vector3 destination = Vector3.zero;
    float vOrbitInput, hOrbitInput, zoomInput, hOrbitSnapInput;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SetCameraTarget(target);
        targetPos = target.position + position.targetPosOffset;
        destination = Quaternion.Euler(orbit.xRotation, orbit.yRotation, 0) * -Vector3.forward * position.distanceFromtarget;
        destination += targetPos;
        transform.position = destination;
    }

    public void SetCameraTarget(Transform t)
    {
        target = t;
    }

    void Update()
    {
		
        if (Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Horizontal") > 0) 
        {
            orbit.yRotation = Mathf.Lerp(orbit.yRotation, - 180, .1f );
            orbit.xRotation = Mathf.Lerp(orbit.xRotation, - 20, .1f);
        }
        else
        {
            orbit.yRotation += -Input.GetAxis("OrbitHorizontal") * orbit.hOrbitSmooth * Time.deltaTime * Input.GetAxisRaw("Fire1");
            orbit.xRotation += -Input.GetAxis("OrbitVertical") * orbit.vOrbitSmooth * Time.deltaTime * Input.GetAxisRaw("Fire1");
        }
        
        if(orbit.yRotation > 360 || orbit.yRotation < -360)
        {
            orbit.yRotation = orbit.yRotation % 360;
        }

        orbit.xRotation = Mathf.Clamp(orbit.xRotation, orbit.minXRotation, orbit.maxXRotation);

        position.distanceFromtarget += Input.GetAxis("Mouse ScrollWheel") * position.zoomSmooth * Time.deltaTime;

        position.distanceFromtarget = Mathf.Clamp(position.distanceFromtarget, position.minZoom, position.maxZoom);

    }

    void LateUpdate()
    {
        targetPos = target.position + position.targetPosOffset;
        destination = Quaternion.Euler(orbit.xRotation, orbit.yRotation + target.eulerAngles.y, 0) * - Vector3.forward * position.distanceFromtarget;
        destination += targetPos;
        transform.position = destination;

        Quaternion targetRot = Quaternion.LookRotation(targetPos - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, position.lookSmooth);
    }
	
	//coroutine to apply static, and shake the screen around
	//when the player has been pushed
	IEnumerator distortion(){
		
		Vector3 OGP = transform.localPosition;
		
		GetComponent<NoiseAndScratches>().enabled = true;
		float maxI = 0.5f;
		while(maxI>0){
			GetComponent<NoiseAndScratches>().grainIntensityMax = maxI;
			maxI-=0.01f;
			
			//shake camera
			shake();
			yield return null;
		}
		GetComponent<NoiseAndScratches>().grainIntensityMax = 0;
		GetComponent<NoiseAndScratches>().enabled = false;
		
		transform.localPosition = OGP;
		yield return null;
	}
	
	void shake(){
		transform.position = new Vector3(
			Random.Range(transform.localPosition.x-100, transform.localPosition.x+100), 
			Random.Range(transform.localPosition.y-100, transform.localPosition.y+100),
			Random.Range(transform.localPosition.z-100, transform.localPosition.z+100));
	}
}
