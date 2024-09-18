using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour
{
    public GameObject color1 , color2 ;
    public static Skull instance;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
                color1.SetActive(true);
                color2.SetActive(true);
            Audiomanager.instance.sfxmusic("Glow");
            
        }
    }

   
}
