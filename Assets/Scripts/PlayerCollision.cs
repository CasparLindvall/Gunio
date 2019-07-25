using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameManager gameManager;

    void Awake()
    {
        //Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
        if (GameManager.instance == null)
        {
            //Instantiate gameManager prefab
            Instantiate(gameManager);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        tag = col.gameObject.tag;
        if (tag == "Deathzone")
        {
            GameManager.instance.SetState(GameState.IsDead);

        }

        else if (tag == "Goal")
        {
            GameManager.instance.SetState(GameState.Won);

        }
        else if (tag == "Coin")
        {
            GameManager.instance.AddScore(100);
            Destroy(col.gameObject);

        }
    }
}