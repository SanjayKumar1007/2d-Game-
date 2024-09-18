using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider healthSlider;
    
    
    public void HealthSlider(int damagepoints)
    {
        
        healthSlider.value -= damagepoints;

        
    }


}
