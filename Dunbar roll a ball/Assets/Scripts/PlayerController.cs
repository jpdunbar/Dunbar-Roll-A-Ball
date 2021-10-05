using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI livesText;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public GameObject welcomeTextObject;
    private Rigidbody rb;
    private Vector3 initialSpawn;
    private Keyboard input;
    private int lives;
    private float movementX, movementY;
    private float timer = 1;
    private float speed = 20;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lives = 5;
        input = Keyboard.current;

        SetLivesText();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        welcomeTextObject.SetActive(true);

        initialSpawn = new Vector3(-1.0f, -12.5f, 2.0f);
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if(lives <= 0)
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
            lives -= 1;
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

        if(transform.position.z < 10 && transform.position.y < -10)
        {
            welcomeTextObject.SetActive(true);
        }
        else
        {
            welcomeTextObject.SetActive(false);
        }

        if(lives <= 0)
        {
            loseTextObject.SetActive(true);
        }

        //
        if (transform.position.z > 148 && transform.position.y < -10)
        {
            winTextObject.SetActive(true);
        }
        else
        {
            winTextObject.SetActive(false);
        }
    }

    //Not using pickups for this game
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            lives = lives + 1;

            SetLivesText();
        }
    }
    */
}
