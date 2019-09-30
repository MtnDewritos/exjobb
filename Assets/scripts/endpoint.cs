using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endpoint : MonoBehaviour
{
    [SerializeField]
    private GameObject[] endpoints = new GameObject[7];
    private int upperRange = 15;
    private int lowerRange = 5;

    public void SelectPosition(Vector3 startPosition)
    {
        int rand = Random.Range(0, 7);
        Vector3 selectedPosition = endpoints[rand].transform.position;

        while (Vector3.Distance(startPosition, selectedPosition) >= upperRange || Vector3.Distance(startPosition, selectedPosition) <= lowerRange)
        {
            rand = Random.Range(0, 7);
            selectedPosition = endpoints[rand].transform.position;
            //Debug.Log("picking position");
        }
        endpoints[rand].SendMessage("IsEnd", true);
        StartCoroutine(PlaySound(endpoints[rand]));
    }
    IEnumerator PlaySound(GameObject point) 
    {
        while (true)
        {
            float time = 1.5f;
            yield return new WaitForSeconds(time);
            point.SendMessage("Play");
        }
       
    }
}
