using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public int playerSpeed = 10;
    public int playerJumpPower = 900;
    public bool playerFacingLeft = false;

    private bool grounded = false;
    private int numOfJumps = 0;
    private int maxJumps = 2;


    // Get input and move player
    void Update()
    {
        Rigidbody2D playerBody = GetComponent<Rigidbody2D>();
        PlayerMove(playerBody);
    }

    void PlayerMove(Rigidbody2D playerBody)
    {

        float moveX = Input.GetAxis("Horizontal");

        //Player dirr
        if (moveX < 0.0f && playerFacingLeft == false)
        {
            FlipPlayer();
        }
        else if (moveX > 0.0f && playerFacingLeft == true)
        {
            FlipPlayer();
        }

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            JumpCharacter(playerBody);
        }

        //Controll
        playerBody.velocity = new Vector2(moveX * playerSpeed, playerBody.velocity.y);


        //Anims TODO

        if (Input.GetKey("escape"))
        {
            GameManager.instance.SetState(GameState.Quit);
        }

    }

    // Flips the player
    void FlipPlayer()
    {
        playerFacingLeft = !playerFacingLeft;
        Vector2 localScaleNew = gameObject.transform.localScale;
        localScaleNew.x *= -1;
        transform.localScale = localScaleNew;
    }

    void JumpCharacter(Rigidbody2D playerBody)
    {
        if (grounded)
        {
            numOfJumps = 0;
        }
        if (grounded || numOfJumps < maxJumps)
        {
            playerBody.AddForce(Vector2.up * playerJumpPower);
            numOfJumps += 1;
            grounded = false;
        }
    }

    // Reset jump if touching ground or turrets
    void OnCollisionEnter2D(Collision2D collide)
    {
        var otherTag = collide.gameObject.tag;
        grounded |= otherTag == "Ground" || otherTag == "Turret";
    }
}