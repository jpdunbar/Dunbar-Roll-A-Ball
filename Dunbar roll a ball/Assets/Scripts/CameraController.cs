using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    private Rigidbody rb;
    private Vector3 reverse;

    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        offset = transform.position - player.transform.position;
        reverse = new Vector3(0, 0, -8);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (rb.useGravity)
        {
            transform.position = player.transform.position + offset;
            transform.rotation = Quaternion.Euler(2, 0, 0);
        }
        else
        {
            transform.position = player.transform.position - offset + reverse;
            transform.rotation = Quaternion.Euler(-2, 0, 0);
        }
    }
}
