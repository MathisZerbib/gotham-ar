using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTruck : MonoBehaviour
{
    public float speed = 5f; // Vitesse de déplacement du camion
    public Vector3 direction = Vector3.forward; // Direction dans laquelle le camion se déplace
    
    // Update is called once per frame
    void Update()
    {
        // On déplace le camion selon la direction et la vitesse donnée
        transform.position += direction * speed * Time.deltaTime;
                Debug.Log("Is this mooving?");

    }
}
