using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endpoint_maze : MonoBehaviour
{
    [SerializeField]
    private GameObject[] endpoints = new GameObject[7];
   

    public void SelectPosition(int start)
    {
        StartCoroutine(PlaySound(endpoints[start]));
    }
    IEnumerator PlaySound(GameObject point) 
    {
        while (true)
        {
            float time = 1.5f;
            yield return new WaitForSeconds(time);
            Debug.Log("played sound");
            point.SendMessage("Play");
        }
       
    }
}
