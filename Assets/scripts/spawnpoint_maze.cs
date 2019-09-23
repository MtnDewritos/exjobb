﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnpoint_maze : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject[] spawnpoints = new GameObject[2];
    [SerializeField]
    private GameObject endpoints;
    private Vector3 startPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {   
        int rand = Random.Range(0, 2);
        startPosition = spawnpoints[rand].transform.position;
        player.transform.position = startPosition;
        endpoints.SendMessage("SelectPosition",rand);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}