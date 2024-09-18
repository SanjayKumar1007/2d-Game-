using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIn : MonoBehaviour
{
    public Slider powerSlider;
    
    

    public void PowerSlider(int increasepoints)
    {

        powerSlider.value += increasepoints;
     

    }
}
