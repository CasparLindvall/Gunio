using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject Player;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Deathzone")
        {
            Debug.Log("DEATHZONE");
            Player.isDead = true;
            Player.Restart();
        }
    }
}