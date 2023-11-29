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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        Debug.Log(verticalInput);

        rb.AddForce(moveDirection.normalized * moveSpeed * 10, ForceMode.Force);
        SpeedControl();
    }

    private void FixedUpdate() {
        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
