using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTruckOnEdge : MonoBehaviour
{

    public GameObject truckPrefab;
    public GameObject mapPrefab;

    void Start()
    {
        // Get the bounds of the map prefab
        Bounds mapBounds = mapPrefab.GetComponent<Renderer>().bounds;

        // Calculate the edge position
        Vector3 edgePosition = new Vector3(mapBounds.center.x, 0, mapBounds.center.z + mapBounds.extents.z);

        // Instantiate the truck prefab at the edge position
        GameObject truck = Instantiate(truckPrefab, edgePosition, Quaternion.identity);

        // Rotate the truck to face towards the center of the map
        truck.transform.LookAt(mapBounds.center);
    }
}
