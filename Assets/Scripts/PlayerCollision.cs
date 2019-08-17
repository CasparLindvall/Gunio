using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Handles the collision detection between player and the game world
public class PlayerCollision : MonoBehaviour
{
    public GameManager gameManager;
    private int bulletDmg = 25;

    void Awake()
    {
        //Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
        if (GameManager.instance == null)
        {
            //Instantiate gameManager prefab
            Instantiate(gameManager);
        }
        bulletDmg = GameManager.instance.GetBulletDmg();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        string otherTag = col.gameObject.tag;
        switch (otherTag)
        {
            case "Deathzone":
                GameManager.instance.SetState(GameState.IsDead);
                break;
            case "Goal":
                GameManager.instance.SetState(GameState.Won);
                break;
            case "Coin":
                GameManager.instance.AddScore(75);
                Destroy(col.gameObject);
                break;
            case "Bullet":
                GameManager.instance.ChangeHealth(-bulletDmg);
                Destroy(col.gameObject);
                break;
            //case "Ground": see moveplayer
            //    break;
            //case "Turret":
            //    break;

            default:
                break;
        }
    }
}