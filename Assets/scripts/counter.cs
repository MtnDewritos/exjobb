using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class counter : MonoBehaviour
{
    private int nr = 0;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
        nr++;
        //write to file "hit " + nr + " times"

    }
}
