using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    float horizontalInput, verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;
    [SerializeField] private float playerHeight;
    bool grounded;
    public float groundDrag;
    public float airMultiplier;

    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, LayerMask.GetMask("Ground"));

        PlayerInput();
        SpeedControl();

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    /// <summary>
    /// Gets the player's input and applies it to the moveDirection vector.
    /// </summary>
    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        print(grounded);

        if(grounded)
        rb.drag = groundDrag;
        else
        rb.drag = 0;
    }

    private void FixedUpdate() 
    {
        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        if(grounded)
        rb.AddForce(moveDirection.normalized * moveSpeed * 10, ForceMode.Force);
        else
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    /// <summary>
    /// Limits the player's speed to the moveSpeed variable.
    /// </summary>
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
