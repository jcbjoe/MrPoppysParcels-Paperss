using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _gravity = 1.5f;
    public float _yVelocity = 0.0f;
    public float _moveSpeed = 4.0f;
    public float _runSpeed = 6.0f;
    public float _jumpSpeed = 40.0f;
    public CharacterController _controller;
    private Animator _animator;

    private bool _onGround;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();

        _animator = GetComponent<Animator>();

        // let the gameObject fall down
        gameObject.transform.position = new Vector3(0, 5, 0);
    }

    // Update is called once per frame
    void Update()
    {

        _onGround = _controller.isGrounded; //Gets if the controller is grounded
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //Gets the direction
        if (direction.sqrMagnitude > 1f) direction = direction.normalized; //This prevents going faster when running diagonically
        Vector3 velocity;
        if (Input.GetKey(KeyCode.LeftShift)) 
            velocity = direction * _runSpeed; //Multiplies the movement speed
        else
            velocity = direction * _moveSpeed; //Multiplies the movement speed

        if (_onGround)
        {
            //JUMPING
            if (Input.GetButtonDown("Jump"))
            {
                _yVelocity = _jumpSpeed;            //Gets the jump height on the Y Axis
            }
        }
        else
            _yVelocity -= _gravity;

        if (Input.GetKey(KeyCode.LeftShift))
            _animator.SetFloat("Speed", (velocity.magnitude / _runSpeed) * 2);
        else
            _animator.SetFloat("Speed", velocity.magnitude / _moveSpeed);


        _controller.Move(velocity * Time.deltaTime);
        Vector3 facingrotation = Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")));
        if (facingrotation != Vector3.zero)         //This condition prevents from spamming "Look rotation viewing vector is zero" when not moving.
            transform.forward = facingrotation;


    }
}
