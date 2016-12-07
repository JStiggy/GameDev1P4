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
        public float knockDownForce = 5f;
    }

    [System.Serializable]
    public class PhysSettings
    {
        public float downAccel = .75f;
    }

    public MoveSettings moveSettings = new MoveSettings();
    public PhysSettings physSettings = new PhysSettings();
    private Animator animator;
    public AudioSource miningNoise;
    public AudioSource movementNoise;
	public sunCount global;

    public float interactionHeight = 3.0f;

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
        StartCoroutine("MiningNoise");
    }

    IEnumerator MiningNoise()
    {
        int count = 1;
        while(true)
        {
            if((count % 4 == 0 || count % 9 == 0) && !miningNoise.isPlaying)
            {
                miningNoise.volume = Random.Range(0f, 1f);
                miningNoise.Play();
            }
            count++;
            yield return new WaitForSeconds(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Submit"))
        //{
        //    knockDown();
        //}

        animator.SetFloat("Speed", (movementVector.z + Mathf.Abs(Input.GetAxisRaw("Horizontal"))) * inEvent);
        if(movementVector.z + Mathf.Abs(Input.GetAxisRaw("Horizontal")) * inEvent > .5f && !movementNoise.isPlaying)
        {
            movementNoise.Play();
        }
        else if((movementVector.z + Mathf.Abs(Input.GetAxisRaw("Horizontal"))) * inEvent < .5f)
        {
            movementNoise.Stop();
        }
        animator.SetFloat("Resistance", resistance);
        if (Input.GetButtonDown("Submit"))
        {
            Collider[] col = Physics.OverlapSphere(transform.position + transform.forward + transform.up * interactionHeight, .7f, 1 << 9);
            if (col.Length > 0)
            {
                inEvent = 0;
                print("mining");
                inEvent = 0;
                animator.SetBool("Mining", true);
                StartCoroutine("Mining", col[0].gameObject);
            }
        }
    }

    bool Grounded()
    {
        return Physics.OverlapSphere(transform.position - new Vector3(0, moveSettings.distanceToGround, 0), .5f, 1 << 8).Length > 0 ? true : false;
    }

    IEnumerator Mining(GameObject node)
    {
        bool noisePlayed = false;
        float animTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;
        //float rotVel = Vector3.Angle((node.transform.position - this.transform.position), this.transform.forward) + targetRotation.eulerAngles.y;
        while (!(animTime > .80f || animTime < .10f) || !animator.GetCurrentAnimatorStateInfo(0).IsName("Mine") || !Input.GetButtonDown("Submit"))
        {

            if (animTime < .40f && animTime > .38f && !noisePlayed)
            {
                miningNoise.volume = 1;
                noisePlayed = true;
                miningNoise.Play();
            }
            else if (animTime > .40f)
            {
                noisePlayed = false;
            }
            animTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;
            // if (Mathf.Abs(targetRotation.eulerAngles.y - rotVel) > 10)
            // {
            //     targetRotation *= Quaternion.AngleAxis(Mathf.Lerp(targetRotation.eulerAngles.y, rotVel, .3f) * Time.deltaTime, Vector3.up);
            // }

            yield return null;
        }
        while(animTime < .30f || animTime > .50f)
        {
            animTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;
            yield return null;
        }
        endEvent();
        animator.SetBool("Mining", false);
        this.resistance = resistance * .8f;
		//add intensity boost
		global.timer+=5;
        node.GetComponent<MineralNode>().consumeNode();
        yield return null;
    }

    IEnumerator KnockBack(Vector3 force)
    {
        animator.SetTrigger("Knocked Down");
        inEvent = 0;
        for (int i = 0; i < 30;++i)
        {
           //transform.Translate(force * Time.deltaTime * moveSettings.knockDownForce);
           // yield return null;
		   rb.AddForce(force*moveSettings.knockDownForce*100);
		   yield return null;
        }
        yield return new WaitForSeconds(1);
        endEvent();
        yield return null;
    }

    public void endEvent()
    {
        print("Can Move");
        inEvent = 1;
    }

    void FixedUpdate()
    {
        targetRotation *= Quaternion.AngleAxis(moveSettings.rotationVelocity * Input.GetAxis("Horizontal") * inEvent * Time.deltaTime, Vector3.up);
        transform.rotation = targetRotation;
        movementVector.z = Mathf.Max(Input.GetAxis("Vertical") * moveSettings.velocity * inEvent * Mathf.Clamp(resistance, .25f, 1f), 0);

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
