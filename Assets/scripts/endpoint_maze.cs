using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endpoint_maze : MonoBehaviour //more like soundpoints
{
    [SerializeField]
    private GameObject[] endpoints = new GameObject[4]; 

    private float nextPlay = 0f;
    private int i = 0;

    public void SelectPosition(int start)
    {
        StartCoroutine(PlaySound(endpoints[start]));
    }
    public void Increment()
    {
        i++;
        if(i == (endpoints.Length - 1))
        {
            endpoints[i].SendMessage("IsEnd", true);
            Debug.Log("sending isEnd to enpoint nr " + i);
        }
    }
    private void Update()
    {
        float time = Time.time;
        float timeInterval = 1.5f;
        if (time >= nextPlay && timeInterval != 0f && i < endpoints.Length)
        {
            nextPlay = time + timeInterval;
            endpoints[i].SendMessage("Play");

        }
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
