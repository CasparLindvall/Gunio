using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


/*
 * Checks if there is a GameManager, if not then create one
 */
public class Loader : MonoBehaviour
{
    public GameManager gameManager;          //GameManager prefab to instantiate.
    //public GameObject soundManager;         // TODO


    void Awake()
    {
        //Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
        if (GameManager.instance == null)
        {
            //Instantiate gameManager prefab
            Instantiate(gameManager);
        }

        /*
         * TODO
        //Check if a SoundManager has already been assigned to static variable GameManager.instance or if it's still null
        if (SoundManager.instance == null)
        {
            //Instantiate SoundManager prefab
            //Instantiate(soundManager);
        }
        */
    }

    // When pressed load scene called
    public void LoadSceneOnClick(int diff)
    {
        GameManager.instance.SetDifficulty(diff);
        GameManager.instance.SetState(GameState.LoadLevel);

    }
}