/*
 * TAKEN FROM https://hub.packtpub.com/creating-simple-gamemanager-using-unity3d/
 *
 * My alternations:
 *      GameStates were extended
 *      Changed to extend SimpleGameManager class to extend Object
*/

using UnityEngine;
using System.Collections;

// Game States
// for now we are only using these two
public enum GameState2 {
    INTRO,
    MAIN_MENU,
    // Before starting a game
    START,
    WON
}

public delegate void OnStateChangeHandler();

public class SimpleGameManager
{
    protected SimpleGameManager() { }
    private static SimpleGameManager instance = null;
    public event OnStateChangeHandler OnStateChange;
    public GameState gameState { get; private set; }

    public static SimpleGameManager Instance
    {
        get
        {
            if (SimpleGameManager.instance == null)
            {
                //DontDestroyOnLoad(SimpleGameManager.instance);
                SimpleGameManager.instance = new SimpleGameManager();
            }
            return SimpleGameManager.instance;
        }

    }

    public void SetGameState(GameState state)
    {
        this.gameState = state;
        OnStateChange();
    }

    public void OnApplicationQuit()
    {
        SimpleGameManager.instance = null;
    }

}