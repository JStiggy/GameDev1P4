using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour
{

    public GameObject target;
    public float speed = 0.3f;
	public float time = 4;
    private float timer = 0;
	

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    void LateUpdate()
    {
        if (timer > time)
        {
            Quaternion old = transform.rotation;
            transform.LookAt(target.transform);
            Quaternion newL = transform.rotation;
            transform.rotation = Quaternion.Lerp(old, newL, speed * Time.deltaTime);
        }
    }

}