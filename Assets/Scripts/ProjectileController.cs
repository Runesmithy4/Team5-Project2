using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("hit " + other.name + "!");
        Destroy(gameObject);
    }
}
