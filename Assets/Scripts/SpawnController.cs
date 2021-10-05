using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    //Variables to help with position/direction/speed of the meteors when spawned.
    public GameObject meteorNormal;
    public GameObject meteorShield;
    public GameObject enemyShip;
    
    //Meteor Speed variables
    public float meteorSpeed = 20f;
    public static float speedIncreaseInterval = 20;
    public float speedChange = 2;                       
    public float speedIntervalTimer = 20;

    //Enemy Ship speed
    public float enemyShipSpeed = 16f;
    [SerializeField] public bool enemyIsAlive = false;

    public Transform spawnPoint;

    //Variables to help with the separation of spawning in terms of time.
    public int timeTilNextSpawn;
    public float timer = 0;

    public SpawnController spawnController;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //timer will add up as normal time until it is told to reset.
        timer += Time.deltaTime;

        //Calls the Spawn() function to spawn a meteor
        Spawn();

        //Increases speed by 2 every 20 seconds.
        speedIntervalTimer -= Time.deltaTime;
        if (speedIntervalTimer < 0)
        {
            meteorSpeed += speedChange;
            speedIntervalTimer = 20;
        }

        //Random spawn timing. ISSUE: Spawners still spawn at the same time due to this variable being the same for all spawners.
        timeTilNextSpawn = Random.Range(2, 8);
    }
   

    void Spawn()
    {
        //Picks a random number between 1 and 10. If 10 then a shield meteor spawns, if any other number, then a normal meteor spawns instead.
        //This gives shield meteors a 10% chance to spawn currently.
        int random = 10;//Random.Range(1, 13);//10; //Random.Range(1, 11); Commented to see if shields work.
        if (enemyIsAlive == false)
        {
            if (random < 8) //If below 10 then it spawns a normal meteor, If 10 then a shield meteor will spawn with a powerup inside.
            {

                if (timer >= timeTilNextSpawn)
                {
                    GameObject meteorSpawned = Instantiate(meteorNormal, spawnPoint.transform.position, Quaternion.identity);
                    Rigidbody meteorRB = meteorSpawned.GetComponent<Rigidbody>();
                    meteorRB.velocity = Vector3.back * meteorSpeed;

                    //Resets timer to 0 to allow for another meteor to spawn and to stop production for 5 seconds.
                    timer = 0;

                    StartCoroutine(DestoryMeteorAfterTime(meteorSpawned, 9));
                }
            }
            else if (random < 11)
            {
                if (timer >= timeTilNextSpawn)
                {
                    enemyIsAlive = true;

                    GameObject enemySpawned = Instantiate(enemyShip, spawnPoint.transform.position, Quaternion.identity);
                    

                    timer = 0;
                }
            }
            else
            {
                if (timer >= timeTilNextSpawn)
                {
                    GameObject meteorSpawned = Instantiate(meteorShield, spawnPoint.transform.position, Quaternion.identity);
                    Rigidbody meteorRB = meteorSpawned.GetComponent<Rigidbody>();
                    meteorRB.velocity = Vector3.back * meteorSpeed;

                    //Resets timer to 0 to allow for another meteor to spawn and to stop production for 5 seconds.
                    timer = 0;

                    StartCoroutine(DestoryMeteorAfterTime(meteorSpawned, 9));
                }
            }
            }
        }
    

    //Destroys the meteor after a certain amount of time.
    public IEnumerator DestoryMeteorAfterTime(GameObject meteor, float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(meteor);
    }
}