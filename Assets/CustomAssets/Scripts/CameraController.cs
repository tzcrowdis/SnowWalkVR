using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float x;
    private float y;
    private Vector3 rotateValue;

    public float sensitivity = 1f;
    public float speed = 1f;

    private float maxX = 90f;
    private float minX = -90f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        //basic camera rotation
        y = Input.GetAxis("Mouse X") * sensitivity;
        x = Input.GetAxis("Mouse Y") * sensitivity;
        rotateValue = new Vector3(x, -y, 0);
        transform.eulerAngles = transform.eulerAngles - rotateValue;

        //prevent unnatural vertical rotation
        if (transform.eulerAngles.x > maxX)
            rotateValue = new Vector3(0, -y, 0);
        if (transform.eulerAngles.x < minX)
            rotateValue = new Vector3(0, -y, 0);
        transform.eulerAngles = transform.eulerAngles - rotateValue;

        //camera motion
        transform.Translate(speed * Input.GetAxisRaw("Horizontal"), 0, speed * Input.GetAxisRaw("Vertical"));
        transform.position = new Vector3(transform.position.x, 2.5f, transform.position.z);
    }
}
