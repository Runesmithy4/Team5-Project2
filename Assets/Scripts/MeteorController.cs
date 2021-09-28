using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
    //PowerUp Variables
    public GameObject shieldPowerUp;

    //Variable inlcuding the script from the spawner allowing to access variables within a separate script.
    private SpawnController spawnController;

    // Start is called before the first frame update
    void Start()
    {
        //Finds the SpawnController script letting us access it.
        GameObject spawner = GameObject.Find("MeteorSpawnerMiddle");
        spawnController = spawner.GetComponent<SpawnController>();
    }

    // Update is called once per frame
    void Update()
    {
        //rotates the meteor over time by 45 degrees on its x and 135 degrees on its z axis.
        transform.Rotate(45 * Time.deltaTime, 0, 135 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Looks to see if the meteor is normal or if it is a shield meteor and destroys the lasers/meteor upon being shot.
        if (gameObject.CompareTag("Meteor"))
        {
            if (other.gameObject.CompareTag("Laser"))
            {
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
        }
        //If the meteor is offers a shield, upon shooting it, it will release a shield power up at is location and disappear as if it was destroyed.
        if (gameObject.CompareTag("ShieldMeteor"))
        {
            if (other.gameObject.CompareTag("Laser"))
            {
                GameObject shieldPowerUpSpawn = Instantiate<GameObject>(shieldPowerUp);
                shieldPowerUpSpawn.transform.position = gameObject.transform.position;
                Rigidbody shieldPowerUpRB = shieldPowerUpSpawn.GetComponent<Rigidbody>();
                shieldPowerUpRB.velocity = Vector3.forward * spawnController.meteorSpeed;
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
        }
    }
}