using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundpoint : MonoBehaviour
{
    bool enabled = true;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.name == "player" && enabled)
        {
            Debug.Log("hit");
            GetComponentInParent<endpoint_maze>().SendMessage("Increment");
            enabled = false;
        }

    }


}
