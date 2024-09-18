using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    public GameObject checkPoint_GO;
    public void CheckPoints()
    {
        checkPoint_GO.SetActive(true);
        
    }
}
