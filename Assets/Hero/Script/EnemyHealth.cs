using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    private Slider healthSlider;
    public EnemyController controller;
    public Vector3 healthOffSet;
    public EnemyHealth instance;

    private void Start()
    {
        healthSlider = GetComponent<Slider>();
    }
    public void HealthSlider(float damagepoints)
    {
        print("into the health slider");
            
        
    }
    public void Update()         
    {
       
    }

    private void OnDestroy()
    {
        
            Destroy(gameObject);
        
    }
}
