using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnpoint : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject[] spawnpoints = new GameObject[6];
    [SerializeField]
    private GameObject endpoints;
    private Vector3 startPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {   
        int rand = Random.Range(0, 6);
        startPosition = spawnpoints[rand].transform.position;
        player.transform.position = startPosition;
        player.transform.rotation = spawnpoints[rand].transform.rotation;
        endpoints.SendMessage("SelectPosition",startPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
