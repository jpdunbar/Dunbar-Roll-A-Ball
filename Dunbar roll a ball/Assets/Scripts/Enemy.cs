using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //The variables used
    Vector3 movement;
    bool right = true;

    // Start is called before the first frame update
    void Start()
    {
        //The speed of the enemy
        movement = new Vector3(10.0f, 0, 0);
    }

    //Move the enemy right or left
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

    //Move the enemy if they trigger the invisible wall
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
