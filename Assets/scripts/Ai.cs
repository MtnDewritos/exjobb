using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> nodes = new List<GameObject>();
    private float nextMove = 0;
    private float timeInterval = 3f;
    private int currentNode = 0;
    private float rayDist = 10f;
    private float viewDist = 6f;
    private float speed = 3f;
    //private float chaseSpeed; //kanske ge spelaren en sprint också

    private int layerMask = 8;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        Look();
        float time = Time.time;
        if(time > nextMove)
        {
            MoveToNextNode();
            nextMove = time + timeInterval;
        }
                     
    }
    private void MoveToNode()
    {
        Vector3 position = nodes[currentNode].transform.position;
        
       /* while (transform.position.normalized != position.normalized)
        {
            if(transform.position.normalized.x != position.normalized.x)
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);
            }
            else if (transform.position.normalized.z != position.normalized.z)
            {
                transform.Translate(0, 0, speed * Time.deltaTime);
            }            
        } */ //while loops freeze unity ??
        transform.Rotate(nodes[currentNode].transform.rotation.eulerAngles);
        Debug.Log(nodes[currentNode].transform.rotation.eulerAngles);


    }
    private void MoveToNextNode()
    {
        MoveToNode();
        if(currentNode < nodes.Count -1)
        {
            currentNode++;
        }
        else
        {
            currentNode = 0;
        }
        
    }
    private void Chase(Vector3 position)
    {
        /*while (transform.position.normalized != position.normalized)
        {
            if (transform.position.normalized.x != position.normalized.x)
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);
            }
            else if (transform.position.normalized.z != position.normalized.z)
            {
                transform.Translate(0, 0, speed * Time.deltaTime);
            }
        }
        */
    }
    private void Return()
    {

    }
    private void Look()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward));
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayDist, layerMask))
        {
            Debug.Log("raycast hit");
            if(hit.distance < viewDist)
            {
                Debug.Log("hit within view distance");
                Vector3 position = hit.collider.transform.position;

                Chase(position);
            }
            
        }
       
    }
}
