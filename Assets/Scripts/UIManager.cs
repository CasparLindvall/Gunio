﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists.
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{

    public static UIManager instance = null;  //Static instance of GameManager which allows it to be accessed by any other script.
    public Text gameTimerText;                // game time
    public Text scoreText;                    // score
    public Text highscoreText;                // curr high score
    public Text healthText;                   // Reference to the UI's health text.
 



    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }

        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
            return;
        }


    }

    public void SetTime(float newTime)
    {
        string timeString = "Time: " + newTime.ToString("0.00");
        gameTimerText.text = timeString;
    }

    public void SetScore(float score)
    {
        scoreText.text = "Score: " + score.ToString("0");
    }

    public void Sethighscore(float score)
    {
        highscoreText.text = "Highscore: " + score.ToString("0");
    }

    public void SetHealth(int newHealth)
    {
        healthText.text = "Health: " + newHealth.ToString("0");
    }
}