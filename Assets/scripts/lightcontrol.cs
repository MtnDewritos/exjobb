using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightcontrol : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> neighbors = new List<GameObject>();

    bool debugLights = false;

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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            foreach (GameObject n in neighbors)
            {
                n.GetComponent<Light>().enabled = false;
            }
            debugLights = !debugLights;
            
        }
        if (debugLights)
        {
            foreach (GameObject n in neighbors)
            {
                n.GetComponent<Light>().enabled = true;
            }
        }
        
    }

}
