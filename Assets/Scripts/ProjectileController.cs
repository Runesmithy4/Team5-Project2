using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Only if the laser hits an enemy, will the laser be destroyed
        if(other.CompareTag("MeteorNormal") || other.CompareTag("MeteorShield") || other.CompareTag("Enemy"))
        {
            //Just destroys the laser and prints to console what it hit for debugging purposes.
            print("hit " + other.name + "!");
            Destroy(gameObject);
        }
    }
}
