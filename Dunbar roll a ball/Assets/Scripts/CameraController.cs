using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset, reverse, steadyCamera;
    private Rigidbody rb;
    private string direction = "up";
    private float cameraMovement = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        offset = transform.position - player.transform.position;
        reverse = new Vector3(0, 0, -16);
        steadyCamera = new Vector3(0, cameraMovement, 0);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(cameraMovement < 2 && direction == "up")
        {
            cameraMovement += Time.deltaTime*2.0f;
            steadyCamera = new Vector3(0, cameraMovement, 0);
        }
        else if (cameraMovement > -2 && direction == "down")
        {
            cameraMovement -= Time.deltaTime*2.0f;
            steadyCamera = new Vector3(0, cameraMovement, 0);
        }


        if (rb.useGravity)
        {
            transform.position = player.transform.position + offset + steadyCamera;
            transform.rotation = Quaternion.Euler(10, 0, 0);
            direction = "up";
        }
        else
        {
            transform.position = player.transform.position - offset + reverse + steadyCamera;
            transform.rotation = Quaternion.Euler(-10, 0, 0);
            direction = "down";
        }
    }
}
