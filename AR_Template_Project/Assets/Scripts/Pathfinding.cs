using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private List<GameObject> waypointList = new List<GameObject>();
    private List<GameObject> targetList = new List<GameObject>();
    public GameObject self;
    private GameObject startP;
    public float moveSpeed = 3f;
    private bool isMoving = false;
    private GameObject closestObjectTarget;
    private float startTime;
    private float closestDistance = Mathf.Infinity;
    private int targetNumber = 0;

    void Start()
    {
        startP = GameObject.FindGameObjectWithTag("StartP");
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Waypoint");
        foreach (GameObject obj in objects)
        {
            waypointList.Add(obj);
        }
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject obj in targets)
        {
            targetList.Add(obj);
        }
        closestObjectTarget = self;
        Pathfind();
    }

    void Update()
    {
        if (isMoving)
        {
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distCovered / Vector3.Distance(self.transform.position, closestObjectTarget.transform.position);
            self.transform.position = Vector3.Lerp(self.transform.position, closestObjectTarget.transform.position, fractionOfJourney);
            if (Vector3.Distance(self.transform.position, targetList[targetNumber].transform.position) < 0.05)
            {
                if (targetNumber == targetList.Count - 1)
                {
                    isMoving = false;
                }
                else if (targetNumber == targetList.Count)
                {
                    targetNumber = 0;
                    closestObjectTarget = self;
                    closestDistance = Mathf.Infinity;
                    Pathfind();
                }
                else
                {
                    targetNumber += 1;
                    closestObjectTarget = self;
                    closestDistance = Mathf.Infinity;
                    Pathfind();
                }
            }
            else if (Vector3.Distance(self.transform.position, closestObjectTarget.transform.position) < 0.1f)
            {
                Pathfind();
            }
        }
    }

    void Pathfind()
    {
        GameObject target;
        if (targetNumber == 0)
        {
            target = startP;
        }
        else
        {
            target = targetList[targetNumber];
        }
        foreach (GameObject obj in waypointList)
        {
            float distanceToReference1 = Vector3.Distance(obj.transform.position, self.transform.position);
            float distanceToReference2 = Vector3.Distance(obj.transform.position, target.transform.position);
            float totalDistance = distanceToReference1 + distanceToReference2;
            if (totalDistance < closestDistance && distanceToReference1 < distanceToReference2 &&
                Vector3.Distance(obj.transform.position, target.transform.position) < Vector3.Distance(closestObjectTarget.transform.position, target.transform.position))
            {
                closestObjectTarget = obj;
                closestDistance = totalDistance;
            }
        }
        startTime = Time.time;
        isMoving = true;
    }
}
