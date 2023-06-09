using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;                //Stores the value of how fast the camera moves


    private Transform parent;

    private void Start()
    {
        parent = transform.parent;                          //The parent of the object is the one we want to rotate so is assigned to player
        Cursor.lockState = CursorLockMode.Locked;           //Locks mouse to middle of screen so it doesnt move around everywhere
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;        //Moves camera in horizontal direction by the mouse sensitivity by time every update
        parent.Rotate(Vector3.up, mouseX);                                                  //Rotates the mouse on the X plane
    }
}
