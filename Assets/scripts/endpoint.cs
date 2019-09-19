using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endpoint : MonoBehaviour
{
    [SerializeField]
    private GameObject[] endpoints = new GameObject[7];
    [SerializeField] //for testing purposes
    private int range = 20;

    public void SelectPosition(Vector3 startPosition)
    {
        int rand = Random.Range(0, 7);
        Vector3 selectedPosition = endpoints[rand].transform.position;

        while (Vector3.Distance(startPosition, selectedPosition) <= range)
        {
            rand = Random.Range(0, 7);
            selectedPosition = endpoints[rand].transform.position;
        }
        StartCoroutine(PlaySound(endpoints[rand]));
    }
    IEnumerator PlaySound(GameObject point) //måste på nått sätt få den konstanta ändringen i velocity
    {
            float time = 1.5f;
            yield return new WaitForSeconds(time);
            Debug.Log("played sound");
            point.SendMessage("Play");
       
    }
}
