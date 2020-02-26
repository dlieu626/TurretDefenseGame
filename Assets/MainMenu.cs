using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "Main Level";
    public void Play ()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void Quit ()
    {
        Debug.Log("Quitting game. Thanks for trying it out homies.");
        Application.Quit();
    }
}
