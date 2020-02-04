using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speed = 5;
    float rotSpeed = 160;

    Rigidbody rb;

    bool isGrounded = false;
    float timeLastJumped = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void FixedUpdate()
    {
        MoveForwardAndBackward();
        Rotate();

        if (IsAbleToJump())
            Jump();
    }

    void MoveForwardAndBackward()
    {
        var movement = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(0, 0, movement);
    }

    void Rotate()
    {
        var rotation = Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);
    }

    bool IsAbleToJump()
    {
        return Input.GetButtonDown("Jump") && isGrounded;
        //&& Time.realtimeSinceStartup >= timeLastJumped + 1;
    }

    void Jump()
    {
        rb.AddForce(new Vector3(0, 2, 0) * 2, ForceMode.Impulse);
        isGrounded = false;
        timeLastJumped = Time.realtimeSinceStartup;
    }
}
