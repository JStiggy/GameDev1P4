using UnityEngine;
using System.Collections;

public class SlaveDriver : MonoBehaviour
{
    private Animator animator;
    private PlayerController player;
    private bool canPush = true;

    public float sightRange = 10f;
    public float pushRange = 3f;

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        player = GameObject.FindObjectsOfType<PlayerController>()[0];
    }

    // Update is called once per frame
    void Update()
    {

        float playerDistance = Vector3.Distance(transform.position, player.transform.position);

        animator.SetFloat("Distance", playerDistance);

        Vector3 lookPos = player.transform.position - transform.position;
        lookPos.y = 0;

        //Vector3.Dot(transform.forward, obj.gameObject.transform.position - transform.position) > Mathf.Cos(30)

        if (playerDistance < pushRange && canPush && Vector3.Angle(transform.forward, lookPos) < 15f )
        {
            Vector3 f = transform.forward;
            player.StartCoroutine("KnockBack", f);
            StartCoroutine(ResetPush(4f));
        }
        else if (playerDistance < sightRange)
        {
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 1f);
        }

    }

    IEnumerator ResetPush(float time)
    {
        animator.SetTrigger("CanPush");
        canPush = false;
        yield return new WaitForSeconds(time);
        canPush = true;
    }
}
