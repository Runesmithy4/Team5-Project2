using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    //Variables to help with position/direction/speed of the meteors when spawned.
    public GameObject meteorNormal;
    public GameObject meteorShield;
    public float meteorSpeed;
    public Transform spawnPoint;

    //Variables to help with the separation of spawning in terms of time.
    public int timeTilNextSpawn = 5;
    float timer = 0;

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
    }

    void Spawn()
    {
        //Picks a random number between 1 and 10. If 10 then a shield meteor spawns, if any other number, then a normal meteor spawns instead.
        //This gives shield meteors a 10% chance to spawn currently.
        int random = Random.Range(1, 11);//10; //Random.Range(1, 11); Commented to see if shields work.
        if (random < 10) //If below 10 then it spawns a normal meteor, If 10 then a shield meteor will spawn with a powerup inside.
        {
            if (timer >= timeTilNextSpawn)
            {
                GameObject meteorSpawned = Instantiate<GameObject>(meteorNormal);
                meteorSpawned.transform.position = spawnPoint.transform.position;
                Rigidbody meteorRB = meteorSpawned.GetComponent<Rigidbody>();
                meteorRB.velocity = Vector3.forward * meteorSpeed;

                //Resets timer to 0 to allow for another meteor to spawn and to stop production for 5 seconds.
                timer = 0;

                StartCoroutine(DestoryMeteorAfterTime(meteorSpawned, 9));
            }
        }
        else
        {
            if (timer >= timeTilNextSpawn)
            {
                GameObject meteorSpawned = Instantiate<GameObject>(meteorShield);
                meteorSpawned.transform.position = spawnPoint.transform.position;
                Rigidbody meteorRB = meteorSpawned.GetComponent<Rigidbody>();
                meteorRB.velocity = Vector3.forward * meteorSpeed;

                //Resets timer to 0 to allow for another meteor to spawn and to stop production for 5 seconds.
                timer = 0;

                StartCoroutine(DestoryMeteorAfterTime(meteorSpawned, 9));
            }
        }
    }

    //Destroys the meteor after a certain amount of time.
    private IEnumerator DestoryMeteorAfterTime(GameObject meteor, float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(meteor);
    }
}