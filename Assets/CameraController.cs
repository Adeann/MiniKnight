using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform playerTrans;
    Vector3 cameraOffset;

    [SerializeField] bool lookAtPlayer = false;

    [SerializeField] float smoothFactor;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        cameraOffset = this.transform.position - playerTrans.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CameraControl();
    }

    void CameraControl()
    {
        Vector3 newPos = playerTrans.position + cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);

        if(lookAtPlayer == true)
        {
            transform.LookAt(playerTrans);
        }
    }
}
