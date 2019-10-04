using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class counter : MonoBehaviour
{
    private static int nr = 0;

    public int Nr
    {
        get { return nr; }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "player")
        {
            Debug.Log("hit");
            nr++;
        }
        
    }
    
}
