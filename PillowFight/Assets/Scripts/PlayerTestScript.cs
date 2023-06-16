using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestScript : MonoBehaviour
{
    /*public float speed = 3.0f;
    public float rotationSpeed = 100.0f;

    private Vector3 moveDirection = Vector3.zero;

    private void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Horizontal") * speed;
        float rotation = Input.GetAxis("Vertical") * rotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 1.5f, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;

        gameObject.transform.position = new Vector3( moveDirection.x * Time.deltaTime , 1.5f, moveDirection.z * Time.deltaTime);
    }*/

    /*public float speed = 6.0f;

    float horizontal;
    float vertical;

    private Rigidbody controller;

    void Start()
    {
        // Store reference to attached component
        controller = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        Debug.Log(horizontal);

    }

    void FixedUpdate()
    {
        controller.velocity = new Vector3(horizontal * speed, 0.0f, vertical * speed);
    }*/

    public float speed = 6.0F;
    public float gravity = 20.0F;

    private Vector3 moveDirection = Vector3.zero;
    public CharacterController controller;

    void Start()
    {
        // Store reference to attached component
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Character is on ground (built-in functionality of Character Controller)
        if (controller.isGrounded)
        {
            
            // Use input up and down for direction, multiplied by speed
            moveDirection = new Vector3(Input.GetAxis("Horizontal1"), 0, -Input.GetAxis("Vertical1"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }
        // Apply gravity manually.
        moveDirection.y -= gravity * Time.deltaTime;
        // Move Character Controller
        controller.Move(moveDirection * Time.deltaTime);
    }
}
