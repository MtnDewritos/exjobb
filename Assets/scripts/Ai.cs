using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> nodes = new List<GameObject>();
    private AudioSource source;
    private float nextMove = 0;
    private float timeInterval = 3f;
    private int currentNode = 0;
    private float rayDist = 10f;
    private float viewDist = 6f;
    private float speed = 0.5f;
    private float chaseSpeed = 1.5f;
    private bool chase = false;
    private bool ret = false;
    private bool setTime = true;
    private bool setTimeLook = true;
    private float nextPlay = 0f;
    //private float chaseSpeed; //kanske ge spelaren en sprint också

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        Look();
        float time = Time.time;
        
        if (!chase && !ret)
        {

            MoveToNode();
            if (time >= nextPlay)
            {

                nextPlay = time + 1f;
                source.Play();
            }
        }
        if(!chase && ret)
        {
            Return();
            if (time >= nextPlay )
            {

                nextPlay = time + 1f;
                source.Play();
            }
        }
        if (chase)
        {
            if (time >= nextPlay)
            {

                nextPlay = time + 0.33f;
                source.Play();
            }
        }
    }
    private void Return()
    {

        

        Vector3 nodePosition = nodes[currentNode].transform.position;
        Vector3 position = transform.position;

        float angle;
        float nodeZ = Mathf.Round(nodePosition.z * 10f) / 10f; //rounding because floats suck
        float nodeX = Mathf.Round(nodePosition.x * 10f) / 10f;
        float z = Mathf.Round(position.z * 10f) / 10f;
        float x = Mathf.Round(position.x * 10f) / 10f;
        float diffX = nodeX - x;
        float diffZ = nodeZ - z;
        if (diffZ < 0)
        {
            diffZ *= -1;
        }
        if (diffX < 0)
        {
            diffX *= -1;
        }
        if ((diffX > 0.3 && diffZ > 1) || (diffZ > 0.3 && diffX > 1))
        {
            //Debug.Log("not gonna get stuck ");
            if (nodes[currentNode].transform.position.z > transform.position.z && diffZ < diffX)
            {
                angle = 0f;
            }
            else if (nodes[currentNode].transform.position.x > transform.position.x && diffZ > diffX)
            {
                angle = 90f;
            }
            else if (nodes[currentNode].transform.position.z < transform.position.z && diffZ < diffX)
            {
                angle = 180f;
            }
            else if (nodes[currentNode].transform.position.x < transform.position.x && diffZ > diffX)
            {
                angle = 270f;
            }
            else
            {
                Debug.Log("errr");
                angle = 0f;
            }

            if (angle != (Mathf.Round(transform.rotation.eulerAngles.y * 10f) / 10f))
            {
                RotateToAngle(angle);
            }
            else
            {
                if (x < nodeX && diffZ > diffX)
                {

                    //Debug.Log(x);
                   // Debug.Log(nodeX);
                    transform.Translate(0, 0, speed * Time.deltaTime);
                }
                else if (x > nodeX && diffZ > diffX)
                {
                   // Debug.Log(x);
                  //  Debug.Log(nodeX);
                    transform.Translate(0, 0, speed * Time.deltaTime);
                }
                else if (z < nodeZ && diffZ < diffX)
                {
                    //  Debug.Log(z);
                    //  Debug.Log(nodeZ);
                    transform.Translate(0, 0, speed * Time.deltaTime);
                }
                else if (z > nodeZ && diffZ < diffX)
                {
                    //  Debug.Log(z);
                    //  Debug.Log(nodeZ);
                    transform.Translate(0, 0, speed * Time.deltaTime);
                }
            }
        }
        else
        {
            angle = Angle(nodes[currentNode].transform.position);

            if (angle != (Mathf.Round(transform.rotation.eulerAngles.y * 10f) / 10f))
            {
                RotateToAngle(angle);
            }
            else
            {
                MoveToNode();
            }
        }
    }
    private float Angle(Vector3 toPos)
    {
        if ((Mathf.Round(toPos.z *10f) /10f) > (Mathf.Round(transform.position.z * 10f) / 10f))
            {
                return 0f;
            }
            else if ((Mathf.Round(toPos.x * 10f) / 10f) > (Mathf.Round(transform.position.x * 10f) / 10f))
            {
            return 90f;
            }
            else if ((Mathf.Round(toPos.z * 10f) / 10f) < (Mathf.Round(transform.position.z * 10f) / 10f))
            {
            return 180f;
            }
            else if ((Mathf.Round(toPos.x * 10f) / 10f) < (Mathf.Round(transform.position.x * 10f) / 10f))
            {
            return 270f;
            }
            else
            {
              //  Debug.Log("standing on node I'm trying to rotate to, rotating to node rotation");
                return nodes[currentNode].transform.rotation.eulerAngles.y;
            }
    }
    private void MoveToNode()
    {
        

        Vector3 nodePosition = nodes[currentNode].transform.position;
        Vector3 position = transform.position;
        float nodeZ = Mathf.Round(nodePosition.z * 10f) / 10f; //rounding because floats suck
        float nodeX = Mathf.Round(nodePosition.x * 10f) / 10f;
        float z = Mathf.Round(position.z * 10f) / 10f;
        float x = Mathf.Round(position.x * 10f) / 10f;


        
        if (x < nodeX)
            {

            //  Debug.Log(x);
            //     Debug.Log(nodeX);
            transform.Translate(0, 0, speed * Time.deltaTime);
            }
            else if (x > nodeX)
            {

            //      Debug.Log(x);
            // Debug.Log(nodeX);
            transform.Translate(0, 0, speed * Time.deltaTime);
            }
        
            else if (z < nodeZ)
            {

            //       Debug.Log(z);
            //Debug.Log(nodeZ);
            transform.Translate(0, 0, speed * Time.deltaTime);
            }
            else if (z > nodeZ)
            {
            //    Debug.Log(z);
            //Debug.Log(nodeZ);
            transform.Translate(0, 0, speed * Time.deltaTime);
            }
        else if (x == nodeX && !(z == nodeZ))
        {
            //Debug.Log("rotating to face point to move to");

            float angle = Angle(nodes[currentNode].transform.position);

            if (angle != (Mathf.Round(transform.rotation.eulerAngles.y * 10f) / 10f))
            {
                RotateToAngle(angle);
            }
        }
        else if(z == nodeZ && !(x == nodeX))
        {
            //Debug.Log("rotating to face point to move to");
            float angle = Angle(nodes[currentNode].transform.position);

            if (angle != (Mathf.Round(transform.rotation.eulerAngles.y * 10f) / 10f))
            {
                RotateToAngle(angle);
            }
        }
            else if (z == nodeZ && x == nodeX)
            {
            //Debug.Log("rotating to face point to move to");

            float rotation = Mathf.Round(transform.rotation.eulerAngles.y * 10f) / 10f;
            float angle; 
            
            ret = false;
            if(rotation != (Mathf.Round(nodes[currentNode].transform.rotation.eulerAngles.y * 10f) / 10f) && setTime)
            {
                //  Debug.Log("rotating");
                RotateToAngle(nodes[currentNode].transform.rotation.eulerAngles.y);
            }
            else
            {
                //Debug.Log("rotation: " + transform.rotation.eulerAngles.y + " node rotation: " + nodes[currentNode].transform.rotation.eulerAngles.y);
                if (setTime)
                {
                    nextMove = Time.time + timeInterval;
                    nextPlay = nextMove;
                 //   Debug.Log("set time to wait for to: " + nextMove + " from: " + Time.time);
                    setTime = false;
                }
                else
                {
                    angle = Angle(nodes[NextNode].transform.position);

                    if (Time.time > nextMove)
                    {
                        RotateToAngle(angle);
                   //     Debug.Log("current rotation: " + transform.rotation.eulerAngles.y);
                        if (rotation == angle)
                        {
                            //             Debug.Log("finished rotating");
                            setTime = true;
                            currentNode = NextNode;
                        }


                    }
                    else
                    {
                        float lookAngle = nodes[currentNode].transform.rotation.eulerAngles.y;
                        if(nextMove - Time.time < 1)
                        {
                            IdleRotate(lookAngle, 3);
                        }
                        else if (nextMove - Time.time < 2)
                        {
                            IdleRotate(lookAngle, 2);
                        }
                        else if (nextMove - Time.time < 3)
                        {
                            IdleRotate(lookAngle, 1);
                        }
                        //Debug.Log("idling");
                        
                    }
                }
                

                
            }
            
            }
        


    }
    private void RotateToAngle(float angle)
    {
        float rotationSpeed = 5f;
        
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, angle, 0), rotationSpeed * Time.deltaTime);
    }
    private int NextNode
    {
        get
        {
            if (currentNode < nodes.Count - 1)
            {
                return currentNode + 1;
            }
            else { return 0; }
        }
    }
    private void Chase(Vector3 targetPosition)
    {
        
        

        Vector3 position = transform.position;
        float targetZ = Mathf.Round(targetPosition.z * 10f) / 10f;
        float targetX = Mathf.Round(targetPosition.x * 10f) / 10f;
        float z = Mathf.Round(position.z * 10f) / 10f;
        float x = Mathf.Round(position.x * 10f) / 10f;

        if (x < targetX)
        {
            
            //    Debug.Log(x);
            // Debug.Log(targetX);
            transform.Translate(0, 0, chaseSpeed * Time.deltaTime);
        }
        else if (x > targetX)
        {
            
            // Debug.Log(x);
            //  Debug.Log(targetX);
            transform.Translate(0, 0, chaseSpeed * Time.deltaTime);
        }
        else if (z < targetZ)
        {

            //  Debug.Log(z);
            //            Debug.Log(targetZ);
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        else if (z > targetZ)
        {

            //   Debug.Log(z);
            // Debug.Log(targetZ);
            transform.Translate(0, 0, speed * Time.deltaTime);
        }




    }
    
    private void IdleRotate(float angle, int state) //rotate when idle to look 10 degrees to the left and the right quickly so that the player can't just hug a wall to avoid detection
    {
        
        float rotateAmount = 10f;
        float angle1 = angle + rotateAmount;
        float angle2 = angle - rotateAmount;
        if(state == 1)
        {
            RotateToAngle(angle1);
        }
        else if (state == 2)
        {
            RotateToAngle(angle2);
        }
        else if (state == 3)
        {
            RotateToAngle(angle);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "player")
        {
            Debug.Log("hit player, returning");
            chase = false;
            ret = true;
        }
        
    }
    private void Look()
    {
        RaycastHit hit;
        int layerMask = LayerMask.GetMask("player");
        int wallMask = LayerMask.GetMask("default");
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward));
        if (!(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), Mathf.Infinity, wallMask)) && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask) )
        {
            if (hit.distance < viewDist)
            {
               // Debug.Log("hit");
                Vector3 position = hit.collider.transform.position;
                if (!ret)
                {
                    chase = true;
                    setTimeLook = true;
                }
                if (chase && !ret)
                {
                    Chase(position);
                }

            }
        }
        else
        {
            if (chase)
            {
                float angle;
                if (setTimeLook)
                {
                    nextMove = Time.time + timeInterval;
                    nextPlay = nextMove;
                 //  Debug.Log("set time to wait for to: " + nextMove + " from: " + Time.time);
                    setTimeLook = false;
                }
                else
                {
                    float nodeZ = nodes[currentNode].transform.position.z;
                    float z = transform.position.z;
                    float nodeX = nodes[currentNode].transform.position.x;
                    float x = transform.position.x;
                    float diffX = nodeX - x;
                    float diffZ = nodeZ - z;
                    if(diffZ < 0)
                    {
                        diffZ *= -1;
                    }
                    if (diffX < 0)
                    {
                        diffX *= -1;
                    }

                    if (nodes[currentNode].transform.position.z < transform.position.z && diffZ > diffX)
                    {
                        angle = 0f;
                    }
                    else if (nodes[currentNode].transform.position.x < transform.position.x && diffZ < diffX)
                    {
                        angle = 90f;
                    }
                    else if (nodes[currentNode].transform.position.z > transform.position.z && diffZ > diffX)
                    {
                        angle = 180f;
                    }
                    else if (nodes[currentNode].transform.position.x > transform.position.x && diffZ < diffX)
                    {
                        angle = 270;
                    }
                    else
                    {
                        Debug.Log("err");
                        angle = 0f;
                    }

                    if (Time.time < nextMove)
                    {
                        if (nextMove - Time.time < 1)
                        {
                            IdleRotate(angle, 3);
                        }
                        else if (nextMove - Time.time < 2)
                        {
                            IdleRotate(angle, 2);
                        }
                        else if (nextMove - Time.time < 3)
                        {
                            IdleRotate(angle, 1);
                        }
                        
                    }
                    else
                    {
                        
                        setTimeLook = true;
                        ret = true;
                        chase = false;
                    }

                }
            }
        }
    }
}
