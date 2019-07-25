using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{
    // Stats and variables for the player
    public int HP = 100;
    public bool isDead;
    private int Health{ get; set;} //does this work like this

    /*
    public int playerSpeed = 10;
    public int playerJumpPower = 900;
    public bool playerFacingLeft = false;
    */

    public void Update()
    {
        if(isDead == true)
        {
            Restart();    
        }
    }

    public void Restart()
    {
        Debug.Log("Player is dead, HP = " + HP);
        SceneManager.LoadScene("GameScene");

    }



}
