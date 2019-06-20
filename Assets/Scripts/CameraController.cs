using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform playerTrans;
    Vector3 cameraOffset;

    [SerializeField] bool lookAtPlayer = false;

    [SerializeField] float smoothFactor;

    private Vector3 vel = Vector3.zero;

    Vector3 yAxis;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        cameraOffset = this.transform.position - playerTrans.position;

        yAxis = new Vector3(0.0f, 1.0f, 0.0f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CameraControl();
    }

    void CameraControl()
    {
        //cameraOffset = this.transform.position - playerTrans.position;
        Vector3 newPos = playerTrans.position + cameraOffset;

        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref vel, smoothFactor);



        if(lookAtPlayer == true)
        {
            transform.LookAt(playerTrans);
        }
    }
}
