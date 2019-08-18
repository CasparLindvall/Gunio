using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/*
 * 
 * Based on https://unity3d.com/pt/learn/tutorials/projects/2d-roguelike-tutorial/writing-game-manager
 * 
 */


public class GameManager : MonoBehaviour
{

    //Static instance of GameManager which allows it to be accessed by any other script.
    public static GameManager instance = null;
    private GameState gameState;
    private float gameTime;
    private float totalScore;
    private float highScore;
    private int health = 100;
    private int levelDifficulty = 1;
    private int bulletDmg = 25;

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

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        gameState = GameState.MainMenu;

        //Call the InitGame function to initialize the first level 
        gameTime = 0;
        totalScore = 0;
        highScore = 0;
    }

    public void SetState(GameState newState)
    {
        GameState previousState = gameState;
        if(previousState!= newState)
        {
            gameState = newState;
        }
    }

    public GameState GetState()
    {
        return gameState;
    }

    //Initializes the game for each level.
    void InitGame()
    {
        LoadLevel("GameScene");

    }

    // If GameState is changed then handle it accordingly
    void Update()
    {
        switch (gameState)
        {
            case GameState.MainMenu:
                break;

            case GameState.LoadLevel:
                // To differentiate from resetGame ?
                ResetGame();
                //1st time loading edge-case
                if (UIManager.instance)
                {
                    UIManager.instance.Sethighscore(highScore);
                }
                break;

            //Core game-loop
            case GameState.Playing:
                gameTime += Time.deltaTime;
                UIManager.instance.SetTime(gameTime);
                break;

            // Reached goal
            case GameState.Won:
                CalculateFinalScore();
                totalScore += gameTime*10;
                highScore = Mathf.Max(totalScore, highScore);
                ResetGame();
                break;

            case GameState.IsDead:
                ResetGame();
                break;
            case GameState.Quit:
                LoadLevel("MainMenu");
                break;
        }
    }


    void LoadLevel(string sceneName)
    {
        if (instance = this)
        {
            SceneManager.LoadScene(sceneName);
            System.Threading.Thread.Sleep(1000);
            if (sceneName == "GameScene")
                gameState = GameState.Playing;
            else if (sceneName == "MainMenu")
                gameState = GameState.MainMenu;
        }
    }

    void ResetGame()
    {
        // Save previous game state then load
        gameTime = 0;
        totalScore = 0;
        health = 100;
        LoadLevel("GameScene");
    }

    public void AddScore(int score)
    {
        totalScore += score;
        UIManager.instance.SetScore(totalScore);
    }


    public void ChangeHealth(int amount)
    {
        health += amount;
        UIManager.instance.SetHealth(health); 
        if(health <= 0)
        {
            instance.gameState = GameState.IsDead;
        }
    }


    // scene changer taken from
    //https://answers.unity.com/questions/1174255/since-onlevelwasloaded-is-deprecated-in-540b15-wha.html
   
    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {

        if(scene.name == "GameScene")
        {
            UIManager.instance.SetTime(gameTime);
            UIManager.instance.SetScore(totalScore);
            UIManager.instance.Sethighscore(highScore);
            UIManager.instance.SetHealth(health);
        }
    }

    public int GetBulletDmg()
    {
        return bulletDmg;
    }

    //Set bullet dmg depending on diff
    public void SetDifficulty(int diff)
    {
        levelDifficulty = diff;
        switch (levelDifficulty)
        {
            case 1:
                bulletDmg = 10;
                break;
            case 2:
                bulletDmg = 25;
                break;
            case 4:
                bulletDmg = 50;
                break;
            case 10:
                bulletDmg = 100;
                break;
        }
    }

    //Create score multiplier depending on gameTime, health and difficulty
    private void CalculateFinalScore()
    {
        float extraScore = 0;
        float timeScore = (45 - gameTime) * 50;
        if (timeScore > 0)
            extraScore += timeScore;
        extraScore *= health / 50; //scale with %health left
        extraScore *= levelDifficulty; //1 for easy, 2 normal, 4 hard
        totalScore += extraScore;

    }

}