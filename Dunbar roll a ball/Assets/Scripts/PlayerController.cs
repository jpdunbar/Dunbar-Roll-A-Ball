using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private Rigidbody rb;
    private Vector3 initialSpawn;
    private Keyboard input;
    private int count;
    private float movementX, movementY;
    private float timer = 1;
    private float speed = 20;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        input = Keyboard.current;

        SetCountText();
        winTextObject.SetActive(false);
        initialSpawn = new Vector3(-1.0f, -12.5f, 2.0f);
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }

    //Move the player
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    //Apply force to the player
    void FixedUpdate()
    {

        if(transform.position.y < -20 || transform.position.y > 20)
        {
            transform.position = initialSpawn;
            transform.position = initialSpawn;
            rb.useGravity = true;
        }

        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
        
        timer += Time.deltaTime;

        if(timer >= 1)
        {
            if (input.spaceKey.isPressed && rb.useGravity == true)
            {
                movement = new Vector3(movementX, 20, movementY);
                rb.AddForce(movement * speed);
                rb.useGravity = false;
                timer = 0;
            }
            else if (input.spaceKey.isPressed && rb.useGravity == false)
            {
                movement = new Vector3(movementX, 20, movementY);
                rb.AddForce(movement * speed);
                rb.useGravity = true;
                timer = 0;
            }
        }
    }

    //When the player touches a pickup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
    }
}
