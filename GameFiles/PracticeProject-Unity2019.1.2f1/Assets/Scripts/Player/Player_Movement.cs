using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float PlayerMovementSpeed = 12f, JumpTakeOfSpeed = 5f, PlayerMoveSpeedWhileAirborn = 5f;

    public bool isGrounded = false, SecJumpAllowed = false, Jumping = false;

    private Rigidbody2D rb2d;

    private Transform _transform;

    public GameObject groundTag1,groundTag2;

    public LayerMask ground_Layer;

    void Start()
    {
        //gets the Rigidbody2D component attached to the GameObject.
        rb2d = GetComponent<Rigidbody2D>();

        SecJumpAllowed = true;
        _transform = transform;
    }

    void Update()
    {

        //Checks if the "Jump" button is pressed and if the player is on the ground. when this is true the Jump methode is called.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                //The Jump methode is called.
                Jump();
            }
            else if (SecJumpAllowed)
            {
                //The Jump methode is called.
                Jump();
                SecJumpAllowed = false;
            }

        }

        //When the Player isgrounded and was jumping the SecJump will be true again.
        if (isGrounded)
        {
            Jumping = false;
            SecJumpAllowed = true;
        }
    }

    //Gets called around 50 Times per Sec.
    void FixedUpdate()
    {
        //Draws a line between two given points. When they collide with the ground_layer the isGrounded bool is set to true.
        isGrounded = Physics2D.OverlapArea(groundTag1.transform.position, groundTag2.transform.position, ground_Layer);


        //Calls the Move methode and input's the required float.
        Move(Input.GetAxis("Horizontal"));

        
    }

    //Adds a force to the Player on the Y axis the elevate the Player.
    void Jump()
    {
        if (isGrounded)
        {
            Jumping = true;
            rb2d.velocity += JumpTakeOfSpeed * Vector2.up;
        }else if (SecJumpAllowed)
        {
            Jumping = true;
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.velocity += JumpTakeOfSpeed * Vector2.up;
        }
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
