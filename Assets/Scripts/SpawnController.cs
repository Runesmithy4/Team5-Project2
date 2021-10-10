using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnController : MonoBehaviour
{
    // Variables to help with spawning the enemies
    public List<GameObject> enemies;
    public List<Vector3> spawnPoints;
    [SerializeField] private int startWait;
    [SerializeField] private float minWaitTime;
    [SerializeField] private float maxWaitTime;
    [SerializeField] private float spawnWaitTime;
    public bool stop = false;

    //Meteor Speed variables
    public float meteorSpeed = 20f;
    public static float speedIncreaseInterval = 10;
    public float speedChange = 10;                       
    public float speedIntervalTimer = 10;

    //Enemy Ship speed
    public float enemyShipSpeed = 16f;
    [SerializeField] public bool enemyIsAlive = false;

    private void OnEnable()
    {
        StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update()
    {
        spawnWaitTime = Random.Range(minWaitTime, maxWaitTime);

        //Increases speed by x every x seconds.
        speedIntervalTimer -= Time.deltaTime;
        if (speedIntervalTimer < 0)
        {
            meteorSpeed += speedChange;
            speedIntervalTimer = speedIncreaseInterval;
        }
    }
    
    // Spawns a random enemy at a random position after a random amount of time
    public IEnumerator Spawner()
    {
        yield return new WaitForSeconds(startWait);

        while(!stop)
        {
            int randomEnemy = Random.Range(0, 100);
            int randomSpawnPoint = Random.Range(0, 3);

            // 15% for shield meteor, 15% for enemy space ship, and 70% for normal meteor EDIT: Made it 5% for shield and 80% for meteor and 15% for enemy spaceship AB 
            if (randomEnemy >= 20)
            {
                randomEnemy = 0;
                CreateMeteor(randomEnemy, randomSpawnPoint);
            }
            else if (randomEnemy >= 15)
            {
                randomEnemy = 1;
                CreateMeteor(randomEnemy, randomSpawnPoint);
            }
            else
            {
                if (SceneManager.GetActiveScene().buildIndex == 1)
                {
                    randomEnemy = 0;
                    CreateMeteor(randomEnemy, randomSpawnPoint);
                }
                else if (enemyIsAlive == false)
                {
                    randomEnemy = 2;
                    enemyIsAlive = true;
                    Instantiate(enemies[randomEnemy], spawnPoints[randomSpawnPoint], Quaternion.identity);
                }
            }

            yield return new WaitForSeconds(spawnWaitTime);
        }
    }

    // Creates a meteor and sends it towards the player
    public void CreateMeteor(int randomEnemy, int randomSpawnPoint)
    {
        GameObject meteorSpawned = Instantiate(enemies[randomEnemy], spawnPoints[randomSpawnPoint], Quaternion.identity);
        Rigidbody meteorRB = meteorSpawned.GetComponent<Rigidbody>();
        meteorRB.velocity = Vector3.back * meteorSpeed;

        StartCoroutine(DestroyMeteorAfterTime(meteorSpawned, 9));
    }

    //Destroys the meteor after a certain amount of time.
    public IEnumerator DestroyMeteorAfterTime(GameObject meteor, float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(meteor);
    }
}