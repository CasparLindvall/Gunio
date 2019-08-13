using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists.
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/*
 * 
 * Based on https://unity3d.com/pt/learn/tutorials/projects/2d-roguelike-tutorial/writing-game-manager
 * 
 */


public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;                //Static instance of GameManager which allows it to be accessed by any other script.
    private GameState gameState;
    private float gameTime;
    private int totalScore;
    private float highScore;

    //Display the game time;
    // Unsure if I should use local referene or just UIManager.instance?
    private UIManager uiManager;

    //Awake is always called before any Start functions
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
        //Get a component reference to the attached BoardManager script
        uiManager = GetComponent<UIManager>();

        //Call the InitGame function to initialize the first level 
        // InitGame();
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
        //Call the SetupScene function of the BoardManager script, pass it current level number.
        //boardScript.SetupScene(level);
        LoadLevel("GameScene");

    }


    //Update is called every frame.
    // Todo onchange() for state
    void Update()
    {
        switch (gameState)
        {
            case GameState.MainMenu:
                // nada?
                break;

            case GameState.LoadLevel:
                // To differentiate from resetGame ?
                ResetGame();
                if (UIManager.instance)
                {
                    UIManager.instance.Sethighscore(highScore);
                }
                break;

            case GameState.Playing:

                // Continue playing
                gameTime += Time.deltaTime;
                UIManager.instance.SetTime(gameTime);
                break;

            case GameState.Won:
                highScore = Mathf.Max(totalScore, highScore);
                /*
                if (totalScore > highScore)
                {
                    highScore = totalScore;
                }
                */
                ResetGame();
                break;

            case GameState.IsDead:
                ResetGame();
                break;
        }
    }


    void LoadLevel(string sceneName)
    {
        if (instance = this)
        {
            SceneManager.LoadScene(sceneName);
            System.Threading.Thread.Sleep(1000);

            gameState = GameState.Playing;
        }
    }

    void ResetGame()
    {
        // Save previous game state then load
        gameTime = 0;
        totalScore = 0;
        LoadLevel("GameScene");
    }

    public void AddScore(int score)
    {
        totalScore += score;
        UIManager.instance.SetScore(totalScore);
    }


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
        //Debug.Log("Level Loaded");
        //Debug.Log(scene.name);
        //Debug.Log(mode);
        if(scene.name == "GameScene")
        {
            UIManager.instance.SetTime(gameTime);
            UIManager.instance.SetScore(totalScore);
            UIManager.instance.Sethighscore(highScore);
        }
    }



}