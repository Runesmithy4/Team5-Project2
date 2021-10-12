using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MeteorController : MonoBehaviour
{
    //PowerUp Variables
    [SerializeField] List <GameObject> powerUps;

    //Variable including the script from the spawner allowing to access variables within a separate script.
    private SpawnController spawnController;

    [SerializeField] private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        //Finds the SpawnController script letting us access it.
        spawnController = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnController>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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
        if (gameObject.CompareTag("MeteorNormal"))
        {
            if (other.gameObject.CompareTag("Laser"))
            {
                Destroy(gameObject);
                FindObjectOfType<AudioManager>().Play("Explosion");
                
                playerController.score += 10;
                playerController.ShowScore("10", gameObject);
                playerController.UpdateScoreText();
            }
            if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EnemyLaser"))
            {
                Destroy(gameObject);
            }
        }

        //If the meteor offers a power up, upon shooting it, it will release a powerup at its location and disappear as if it was destroyed.
        if (gameObject.CompareTag("MeteorShield"))
        {
            if (other.gameObject.CompareTag("Laser"))
            {
                int randomPowerUp = 0;

                // If its not the first level, allow the pickup to be a life instead
                if (SceneManager.GetActiveScene().buildIndex != 1)
                {
                    randomPowerUp = Random.Range(0, 2);
                }

                GameObject shieldPowerUpSpawn = Instantiate(powerUps[randomPowerUp], gameObject.transform.position, Quaternion.identity);
                Rigidbody shieldPowerUpRB = shieldPowerUpSpawn.GetComponent<Rigidbody>();
                shieldPowerUpRB.velocity = Vector3.back * spawnController.meteorSpeed;
                
                Destroy(gameObject);
                FindObjectOfType<AudioManager>().Play("Explosion");

                playerController.score += 10;
                playerController.ShowScore("10", gameObject);
                playerController.UpdateScoreText();
            }
            if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EnemyLaser"))
            {
                Destroy(gameObject);
            }
        }

        if (other.CompareTag("Earth"))
        {
            Destroy(gameObject);

            playerController.lives -= 1;
            playerController.CheckIfDead();
        }
    }
}