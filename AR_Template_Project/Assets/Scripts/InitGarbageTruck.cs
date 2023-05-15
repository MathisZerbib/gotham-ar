using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGarbageTruck : MonoBehaviour
{
    public GameObject garbagePrefab; // the prefab you want to add
    public float speed = 5f; // Vitesse de déplacement du camion
    public Vector3 direction = Vector3.forward;
    private bool isMoving = false; // flag to check if the truck is moving

    private GameObject truckInstance; // reference to the truck instance
    private Vector3 startingPosition; // the starting position of the truck

    // create a new instance of the garbagePrefab and add it to this GameObject
    public void initTruck()
    {
        startingPosition = transform.position; // save the starting position
        truckInstance = Instantiate(garbagePrefab, startingPosition, transform.rotation, transform);
        isMoving = true;
    }

    // move the truck forward
    private void moveForward()
    {
        if (isMoving)
        {
            truckInstance.transform.position += direction * speed * Time.deltaTime;
            // check if the truck has reached the end of the map
            if (truckInstance.transform.position.z >= startingPosition.z + 100f)
            {
                // set the truck's position back to the starting position
                truckInstance.transform.position = startingPosition;
            }
        }
    }

    // stop the truck
    public void stopTruck()
    {
        isMoving = false;
    }

    // start the truck
    public void startTruck()
    {
        isMoving = true;
    }

    // update is called once per frame
    void Update()
    {
        moveForward();
    }
}
