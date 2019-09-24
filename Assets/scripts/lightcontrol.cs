using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightcontrol : MonoBehaviour
{
    [SerializeField]
    private GameObject[] neighbors;
    
    private void LightNeighbors()
    {
        foreach(GameObject n in neighbors)
        {
            n.SendMessage("LightUp");
        }
    }

    private void LightUp()
    {
        GetComponent<Light>().intensity = 0.1f;
    }
    
}
