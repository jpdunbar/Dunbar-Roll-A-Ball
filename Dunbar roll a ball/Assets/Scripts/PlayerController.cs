using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //Variables used
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
    private float direction = -1.0f;
    private bool lose = false;

    // Start is called before the first frame update
    void Start()
    {
        //Setting up variables
        rb = GetComponent<Rigidbody>();
        lives = 3;
        input = Keyboard.current;

        //Setting the lives text
        SetLivesText();
        
        //Setting the canvases to be disabled
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        welcomeTextObject.SetActive(true);

        //The initial spawn of the player
        initialSpawn = new Vector3(-1.0f, -12.5f, 2.0f);
    }

    //Changing the lives counter
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
        //Restarting the player if they fall
        if(transform.position.y < -20 || transform.position.y > 25)
        {
            transform.position = initialSpawn;
            transform.position = initialSpawn;
            rb.useGravity = true;
            direction = -1.0f;
            lives -= 1;
            SetLivesText();
        }

        Vector3 movement = new Vector3(movementX, direction, movementY);
        rb.AddForce(movement * speed);
        
        timer += Time.deltaTime;

        //Limiting button presses
        if(timer >= 1)
        {
            //Changing gravity
            if (input.spaceKey.isPressed && rb.useGravity == true)
            {
                direction = 1.0f;
                rb.useGravity = false;
                timer = 0;
            }
            else if (input.spaceKey.isPressed && rb.useGravity == false)
            {
                direction = -0.5f;
                rb.useGravity = true;
                timer = 0;
            }
        }

        //In the into portion
        if (transform.position.z < 10 && transform.position.y < -10 && lose == false)
        {
            welcomeTextObject.SetActive(true);
        }
        else
        {
            welcomeTextObject.SetActive(false);
        }

        //When the player runs out of lives
        if(lives <= 0)
        {
            loseTextObject.SetActive(true);
            lose = true;
        }

        //Puts up the win text
        if (transform.position.z > 148 && transform.position.y < -10 && lose == false)
        {
            winTextObject.SetActive(true);
        }
        else
        {
            winTextObject.SetActive(false);
        }
    }

    //Pickups are a prize
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
