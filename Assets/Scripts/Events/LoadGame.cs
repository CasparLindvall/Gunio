using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{

    public void LoadSceneOnClick(string sceneName)
    {
        Debug.Log("load scene " + sceneName);
        SceneManager.LoadScene(sceneName); 
    }

}