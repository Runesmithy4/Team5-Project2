using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Just destroys the laser and prints to console what it hit for debugging purposes.
        print("hit " + other.name + "!");
        Destroy(gameObject);
    }
}
