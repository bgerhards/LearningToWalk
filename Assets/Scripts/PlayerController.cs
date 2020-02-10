using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speed = 5;
    float rotSpeed = 160;

    Rigidbody rb;
    AudioSource audioSource;

    bool isGrounded = false;
    float timeLastJumped = 0;

    [SerializeField]
    private AudioClip jumpSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

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
        audioSource.PlayOneShot(jumpSound);
        isGrounded = false;
        rb.AddForce(new Vector3(0, 2, 0) * 2, ForceMode.Impulse);
        timeLastJumped = Time.realtimeSinceStartup;
    }
}
