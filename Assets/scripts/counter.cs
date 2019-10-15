using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class counter : MonoBehaviour
{
    private static int nr = 0;

    public int Nr
    {
        get { return nr;}
        set { nr = value; }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "player")
        {
            if(!(SceneManager.GetActiveScene().name == "Room" || SceneManager.GetActiveScene().name == "room_noraycast" || SceneManager.GetActiveScene().name == "tutorial_room"))
            {
                if (GetComponentInParent<Ai>().Chasing)
                {
                    nr++; 
                }
            }
            else
            {
                nr++;
            }
        }
        
    }
    
}
