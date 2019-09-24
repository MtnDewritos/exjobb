using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorscript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GetComponentInParent<lightcontrol>().SendMessage("LightNeighbors");
        GetComponentInParent<lightcontrol>().SendMessage("LightUp");
    }
}
