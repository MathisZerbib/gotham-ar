using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private List<GameObject> waypointList; 
    private List<GameObject> TargetList; 
    public GameObject self;
    public GameObject startP;
    private int targetNumber = 0;
    public float moveSpeed = 3f;
    private bool doPathfind = true;
    private bool isMoving = false;
    private GameObject closestObjectTarget;
    private float journeyLength;
    private float startTime;
    private float closestDistance;
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Waypoint");
        foreach (GameObject obj in objects)
        {
            Debug.Log(obj);
            waypointList.Add(obj);
        }
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject obj in targets)
        {
            TargetList.Add(obj);
        }
        closestDistance = Mathf.Infinity;
        closestObjectTarget = self;
        target = TargetList[targetNumber];
        Pathfind();
    }
    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            self.transform.position = Vector3.Lerp(self.transform.position, closestObjectTarget.transform.position, fractionOfJourney);
        if (Vector3.Distance(self.transform.position, target.transform.position) < 4)        
        {
             if(targetNumber == TargetList.Count+1)
            {
                isMoving = false;
                doPathfind = false;
            }
            else if(targetNumber == TargetList.Count)
            {
                closestObjectTarget =self;
                closestDistance = Mathf.Infinity;
                targetNumber +=1;
                target = startP;
                isMoving = false;
                Pathfind();
            }
            else
            {   
                closestObjectTarget =self;
                closestDistance = Mathf.Infinity;
                targetNumber +=1;
                target = TargetList[targetNumber];
                isMoving = false;
                Pathfind();
            }
        }
        else
        {   
            if (doPathfind == true && Vector3.Distance(self.transform.position, closestObjectTarget.transform.position) < 0.1f)
            {
                isMoving = false;
                Pathfind();
            }
        }
        }
    }
    void Pathfind()
    {
        foreach (GameObject obj in waypointList)
        {   
            float distanceToReference1 = Vector3.Distance(obj.transform.position, self.transform.position);
            float distanceToReference2 = Vector3.Distance(obj.transform.position, target.transform.position);
            float totalDistance = distanceToReference1 + distanceToReference2;
            if (totalDistance < closestDistance)
            {
                if (distanceToReference1 < distanceToReference2)
                {   
                    if (Vector3.Distance(obj.transform.position, target.transform.position) < Vector3.Distance(closestObjectTarget.transform.position, target.transform.position))
                    {
                    closestObjectTarget = obj;
                    closestDistance = totalDistance;
                    }
                }
            }
        }
        startTime = Time.time;
        journeyLength = Vector3.Distance(self.transform.position, closestObjectTarget.transform.position);
         isMoving = true;
    }
}