using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    //public Transform cameraBody;
    public float touchSensitivity = 10f;
    public float mouseSensitivity = 100f;

    float xRotation = 0f;
    float yRotation = 0f;

    private bool moveCameraEnabled = true;

    void Start()
    {
        UpdateLastRotation();
    }

    void Update()
    {
        MoveCamera();

    }

    private void MoveCamera()
    {
        if (Input.touchCount > 0 && moveCameraEnabled)
        {
            Touch touch = Input.GetTouch(0);

            float touchX = 0f;
            float touchY = 0f;

            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    // Record initial touch position.
                    touchX = 0f;
                    touchY = 0f;
                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    // Determine direction by comparing the current touch position with the initial one
                    Debug.Log("touch.deltaPosition.x: " + touch.deltaPosition.x + " touch.deltaPosition.y: " + touch.deltaPosition.y);
                    Debug.Log("deltaTime: " + Time.deltaTime);

                    touchX = touch.deltaPosition.y * touchSensitivity * Time.deltaTime;
                    touchY = touch.deltaPosition.x * touchSensitivity * Time.deltaTime;
                    //Debug.Log("touchX: " + touchX + " touchY: " + touchY);

                    if (Mathf.Abs(touchX) < 1f)
                    {
                        touchX = 0f;
                    }

                    if (Mathf.Abs(touchY) < 1f)
                    {
                        touchY = 0f;
                    }
                    Debug.Log("post diff -- touchX: " + touchX + " touchY: " + touchY);

                    break;

                case TouchPhase.Ended:
                    // Report that the touch has ended when it ends
                    touchX = 0f;
                    touchY = 0f;
                    break;
            }


            xRotation += touchX;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            yRotation += touchY;
            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        }

        if (Input.GetMouseButton(0) && moveCameraEnabled)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            Debug.Log("mouseX: " + mouseX + " mouseY: " + mouseY);

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            yRotation += mouseX;

            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

        }
        
        if (!moveCameraEnabled)
        {
            ResetRotation();
            UpdateLastRotation();
        }

    }

    private void ResetRotation()
    {
        xRotation = 0f;
        yRotation = 0f;
    }


    private void UpdateLastRotation()
    {
        Quaternion lastRotation = transform.localRotation;
        xRotation += lastRotation.eulerAngles.x;
        yRotation += lastRotation.eulerAngles.y;
    }

    public void EnableCameraMovement()
    {
        moveCameraEnabled = true;
    }

    public void DisableCameraMovement()
    {
        moveCameraEnabled = false;
    }

}
