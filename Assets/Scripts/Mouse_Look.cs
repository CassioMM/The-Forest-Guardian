using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mouse_Look : MonoBehaviour
{
    public float mouseSesitivity = 350f;
    public Transform playerBody;

    public float xRotation;
    bool canMouseMove;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        if (canMouseMove == true)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSesitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSesitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90);


            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    public void CancelCamera(bool value)
    {
        canMouseMove = value;
    }

    public void SensiControl(float value)
    {
        mouseSesitivity = value;


    }

}


