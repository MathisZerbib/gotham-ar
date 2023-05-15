using UnityEngine;

public class TruckController : MonoBehaviour
{
    public float moveSpeed = 5f; // regular speed
    private bool isMoving = false;
    private Vector3 targetPosition;
    private float startTime;
    private float journeyLength;

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(transform.position, targetPosition, fractionOfJourney);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
            }
        }
    }

    public void Move()
    {
        // set the target position for the truck to move towards
        targetPosition = new Vector3(transform.position.x + 5f, transform.position.y, transform.position.z);

        // calculate the distance between the current position and the target position
        journeyLength = Vector3.Distance(transform.position, targetPosition);

        // record the start time for lerping
        startTime = Time.time;

        // set isMoving to true to start the movement
        isMoving = true;
    }
}
