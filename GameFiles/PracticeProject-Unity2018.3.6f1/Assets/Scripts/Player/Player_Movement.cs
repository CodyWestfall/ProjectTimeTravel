using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float PlayerMovementSpeed = 12f, JumpTakeOfSpeed = 5f;

    [HideInInspector] public bool isGrounded = false, isFacingRight;

    private Rigidbody2D rb2d;

    private Transform _transform;

    public GameObject groundTag;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        _transform = transform;
    }

    //Gets called around 50 Times per Sec.
    private void FixedUpdate()
    {
        isGrounded = Physics2D.Linecast(_transform.position, groundTag.transform.position);

        Move(Input.GetAxis("Horizontal"));
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    //Adds a force to the Player on the Y axis the elevate the Player.
    void Jump()
    {
        rb2d.velocity += JumpTakeOfSpeed * Vector2.up;
    }

    //Moves the Player in a direction. If the direction is fliped the player will also flip.
    void Move(float _Input)
    {
        
        
        Vector2 moveX = rb2d.velocity;
        moveX.x = _Input * PlayerMovementSpeed;
        rb2d.velocity = moveX;

        if (moveX.x < 0f) transform.eulerAngles = new Vector3(0, -180, 0);
        else if (moveX.x > 0f) transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
