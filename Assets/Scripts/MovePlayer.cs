using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public int playerSpeed = 10;
    public int playerJumpPower = 900;
    public bool playerFacingLeft = false;
    //PlayerCharacter Player = new PlayerCharacter();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // TODO: Move to main loop and then call playerMove
    void Update()
    {
        Rigidbody2D playerBody = GetComponent<Rigidbody2D>(); 
        PlayerMove(playerBody);
    }

    void PlayerMove(Rigidbody2D playerBody) 
    {

        float moveX = Input.GetAxis("Horizontal");

        //Player dirr
        if(moveX < 0.0f && playerFacingLeft == false)
        {
            FlipPlayer();
        }
        else if (moveX > 0.0f && playerFacingLeft == true){
            FlipPlayer();
        }

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            Jump(playerBody);
        }

        //Controll
        playerBody.velocity = new Vector2(moveX * playerSpeed, playerBody.velocity.y);
         



        //Anims

    }

    // Flips the player
    void FlipPlayer()
    {
        playerFacingLeft = !playerFacingLeft;
        Vector2 localScaleNew = gameObject.transform.localScale;
        localScaleNew.x *= -1;
        transform.localScale = localScaleNew;
    } 

    // Jump
    void Jump(Rigidbody2D playerBody)
    {
        //Jump
        playerBody.AddForce(Vector2.up * playerJumpPower);
    }

}
