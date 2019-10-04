using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightcontrol : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> neighbors = new List<GameObject>();
    
    private void LightNeighbors()
    {
        
        foreach(GameObject n in neighbors)
        {
              n.SendMessage("LightUp");
        }
    }

    private void LightUp()
    {
        GetComponentInChildren<Light>().intensity = 0.1f;
    }
    
}
