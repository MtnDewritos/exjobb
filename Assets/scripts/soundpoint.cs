using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundpoint : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "player")
        {
            Debug.Log("hit");
            GetComponentInParent<endpoint_maze>().SendMessage("Increment");
        }

    }

}
