using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Rigidbody rb;
    [SerializeField] private Transform trans;

    [SerializeField] Animator anim;

    private float axisH;
    private float axisV;

    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;

    [SerializeField] private float rotSpeed;
    [SerializeField] private float force;

    private Vector3 vel;
    private Vector3 dir;

    public Vector3 forwardRay;
    public Vector3 playerPos;

    private bool canAttack = true;
    private bool isBlocking = false;

    void Start()
    {
        playerPos = rb.transform.position;
        speed = 0;
    }


    void Update()
    {
        PlayerMovement();
        PlayAnimations();

        ForwardRayCast();
        //Debug.DrawRay(new Vector3(rb.transform.position.x, .5f, rb.transform.position.z), forwardRay, Color.green);
    }

    // Do anything Physics related here
    private void FixedUpdate()
    {
        vel.y = rb.velocity.y;

        rb.velocity = vel;       
    }

    void PlayerMovement()
    {
        vel = Vector3.zero;

        axisH = Input.GetAxis("Horizontal");
        axisV = Input.GetAxis("Vertical");
        //Accelerate 
        if (axisH > 0 || axisV > 0)
        {
            speed = Mathf.Clamp(speed += .02f, 0, maxSpeed);
        }
        else if (axisH < 0 || axisV < 0) {
            speed = Mathf.Clamp(speed += .02f, 0, maxSpeed);
        }
        else
        {
            speed = 0;
        }

        vel = new Vector3(axisH, 0, axisV);
        vel.Normalize();
        vel *= speed;

        Quaternion lookRotation = Quaternion.LookRotation(rb.transform.forward + vel);

        trans.rotation = Quaternion.Slerp(rb.transform.rotation, lookRotation, rotSpeed);
    }

    void ForwardRayCast()
    {
        //player position for raycast info
        playerPos = rb.transform.position;
        playerPos.y = rb.transform.position.y + 0.25f;

        forwardRay = rb.transform.forward * 2.0f;
        forwardRay.y = rb.transform.position.y + 0.25f;
    }


    void AttackOne()
    {
        anim.SetTrigger("isAttackingOne");
    }

    void Block()
    {
        anim.SetBool("isBlocking", isBlocking);
    }

    void PlayAnimations()
    {
        anim.SetFloat("speed", speed);

        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack == true)
        {
            AttackOne();
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            isBlocking = true;

        }
        else
        {
            isBlocking = false;
        }

        Block();
    }
}
