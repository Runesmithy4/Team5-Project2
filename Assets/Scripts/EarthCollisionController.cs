using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthCollisionController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MeteorNormal") || collision.gameObject.CompareTag("MeteorShield") || collision.gameObject.CompareTag("MeteorLife"))
        {
            Destroy(collision.gameObject);

            playerController.lives -= 1;
            playerController.CheckIfDead();
        }
    }
}
