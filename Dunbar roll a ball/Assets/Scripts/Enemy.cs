using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Vector3 movement;
    bool right = true;

    // Start is called before the first frame update
    void Start()
    {
        movement = new Vector3(10.0f, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (right == true)
        {
            transform.position += movement * Time.deltaTime;
        }
        else if (right == false)
        {
            transform.position -= movement * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MoveEnemy") && right == true)
        {
            right = false;
        }
        else if(other.gameObject.CompareTag("MoveEnemy") && right == false)
        {
            right = true;
        }
    }
}
