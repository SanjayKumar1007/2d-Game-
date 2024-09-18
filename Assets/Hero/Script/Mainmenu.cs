using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void HomePage()
    {
        SceneManager.LoadScene(1);
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(2);
    }
    public void NextStage()
    {
        SceneManager.LoadScene(3);
    }

    public void Pause()
    {
       // Time.timeScale = 0;
    }
    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void ButtonSfx()
    {
        Audiomanager.instance.sfxmusic("Button");
    }

    public void CheckPointMusic()
    {
      
        Audiomanager.instance.playmusic("Main Theme");
    
    }


    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
