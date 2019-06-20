﻿using System.Collections;
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

    private bool canAttack = true;
    private bool isBlocking = false;

    void Start()
    {
        speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayAnimations();
    }

    private void FixedUpdate()
    {
        rb.velocity = vel;       
    }

    void PlayerMovement()
    {
        //vel = Vector3.zero;

        axisH = Input.GetAxis("Horizontal");
        axisV = Input.GetAxis("Vertical");
        //Accelerate 
        if (axisH > 0 || axisV > 0)
        {
            speed = Mathf.Clamp(speed += .02f, 0, maxSpeed);
        } else if (axisH < 0 || axisV < 0) {
            speed = Mathf.Clamp(speed += .02f, 0, maxSpeed);
        } else
        {
            speed = 0;
        }

        axisH *= speed;
        axisV *= speed;

        vel = new Vector3(axisH, 0, axisV);

        dir = rb.transform.forward;

        Quaternion lookRotation = Quaternion.LookRotation(dir + vel);

        trans.rotation = Quaternion.Slerp(rb.transform.rotation, lookRotation, rotSpeed);


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
