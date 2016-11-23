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
    private Animator animator;

    private Vector3 movementVector = Vector3.zero;
    [HideInInspector]
    public Quaternion targetRotation;
    Rigidbody rb;

    //Decrease player movement speed
    public float fatigue = 0f;
    //Character's ability to act against slavers, decreases over time
    public float resistance = 1f;
    //1 if not in an event, 0 if in an event. Used to control player movement.
    public int inEvent = 1;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        targetRotation = this.transform.rotation;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", movementVector.z);
        animator.SetFloat("Resistance", resistance);
        if (Input.GetButtonDown("Submit"))
        {
            Collider[] col = Physics.OverlapSphere(transform.position + transform.forward, .3f, 1 << 9);
            if (col.Length > 0)
            {
                inEvent = 0;
                print("mining");
                inEvent = 0;
                animator.SetBool("Mining", false);
                StartCoroutine("Mining", col[0].gameObject);
            }
        }
    }

    bool Grounded()
    {
        return Physics.OverlapSphere(transform.position - new Vector3(0, moveSettings.distanceToGround, 0), .5f, 1 << 8).Length > 0 ? true : false;
    }

    public void knockDown()
    {
        animator.SetTrigger("Knockdown");
        inEvent = 0;
        Invoke("endEvent", 1f);
    }

    IEnumerator Mining(GameObject node)
    {
        //float rotVel = Vector3.Angle((node.transform.position - this.transform.position), this.transform.forward) + targetRotation.eulerAngles.y;
        while (!(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .90) || !animator.GetCurrentAnimatorStateInfo(0).IsName("Mine") || !Input.GetButtonDown("Submit"))
        {
           // if (Mathf.Abs(targetRotation.eulerAngles.y - rotVel) > 10)
           // {
           //     targetRotation *= Quaternion.AngleAxis(Mathf.Lerp(targetRotation.eulerAngles.y, rotVel, .3f) * Time.deltaTime, Vector3.up);
           // }

            yield return null;
        }
        Invoke("endEvent", animator.GetCurrentAnimatorStateInfo(0).length * (1 - animator.GetCurrentAnimatorStateInfo(0).normalizedTime));
        animator.SetBool("Mining", false);
        yield return null;
    }

    public void endEvent()
    {
        inEvent = 1;
    }

    void FixedUpdate()
    {
        targetRotation *= Quaternion.AngleAxis(moveSettings.rotationVelocity * Input.GetAxis("Horizontal") * inEvent * Time.deltaTime, Vector3.up);
        transform.rotation = targetRotation;
        movementVector.z = Input.GetAxis("Vertical") * moveSettings.velocity * inEvent * Mathf.Clamp(1 - fatigue, .5f, 1f);

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
