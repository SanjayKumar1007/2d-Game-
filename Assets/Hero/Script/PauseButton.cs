using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{

    public GameObject pauseButton;
    void Update()
    {
        Time.timeScale = 0; 
        if (gameObject.activeInHierarchy)
        {
            pauseButton.SetActive(false);
        }else
        {
            pauseButton.SetActive(true);
        }
    }
  
}
