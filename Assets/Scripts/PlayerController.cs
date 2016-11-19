using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    [System.Serializable]
    public class MoveSettings
    {
        public float velocity = 12f;
        public float rotationVelocity = 150f;
        public float distanceToGround = 1f;
    }

    [System.Serializable]
    public class PhysSettings
    {
        public float downAccel = .75f;
    }

    public MoveSettings moveSettings = new MoveSettings();
    public PhysSettings physSettings = new PhysSettings();

    private Vector3 movementVector = Vector3.zero;
    [HideInInspector]
    public Quaternion targetRotation;
    Rigidbody rb;

    //Decrease player movement speed
    public float fatigue = 0f;
    //Character's ability to act against slavers, decreases over time
    public float resistance = 1f;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        targetRotation = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    bool Grounded()
    {
        return Physics.OverlapSphere(transform.position - new Vector3(0, moveSettings.distanceToGround, 0), .5f, 1<<8).Length > 0 ? true : false;
    }

    void FixedUpdate()
    {
        targetRotation *= Quaternion.AngleAxis(moveSettings.rotationVelocity * Input.GetAxis("Horizontal") * Time.deltaTime, Vector3.up);
        transform.rotation = targetRotation;
        movementVector.z = Input.GetAxis("Vertical") * moveSettings.velocity *  Mathf.Clamp(1-fatigue, .5f,1f);
        if (Grounded())
        {
            movementVector.y = 0;
        }
        else
        {
            movementVector.y -= physSettings.downAccel;
        }

        rb.velocity = transform.TransformDirection(movementVector);
    }
}
