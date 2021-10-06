using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private SpawnController spawnController;

    [SerializeField] private PlayerController pc;

    private Rigidbody enemyRB;
    private Vector3 endPosition;

    public GameObject laserPrefab;
    public float shootTimer = 3;
    public float timer = 0;
    public Transform laserSpawn;
    public float laserSpeed = 15;
    
    // Start is called before the first frame update
    void Start()
    {
        //Finds the SpawnController script letting us access it.
        spawnController = GameObject.Find("EnemySpawner").GetComponent<SpawnController>();
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        enemyRB = GetComponent<Rigidbody>();

        GameObject spawner = GameObject.Find("MeteorSpawnerMiddle");
        endPosition = new Vector3(spawner.transform.position.x, spawner.transform.position.y, spawner.transform.position.z - 50);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (spawnController.enemyIsAlive == true)
        {
            if (enemyRB.position != endPosition)
            {
                Vector3 newPosition = Vector3.MoveTowards(enemyRB.position, endPosition, spawnController.enemyShipSpeed * Time.deltaTime);
                enemyRB.MovePosition(newPosition);
                StartCoroutine(EnemyShipPlacement());
            }
                Shoot();
        }
    }

    private void Shoot()
    {
        if (timer > shootTimer)
        {
            GameObject laser = Instantiate(laserPrefab, new Vector3(laserSpawn.transform.position.x, laserSpawn.transform.position.y, laserSpawn.transform.position.z), laserSpawn.rotation);
            Rigidbody laserRB = laser.GetComponent<Rigidbody>();
            laserRB.velocity = (laserSpawn.transform.rotation * Vector3.up) * laserSpeed;

            //StartCoroutine(spawnController.DestroyMeteorAfterTime(laser, 5f));

            timer = 0;
            FindObjectOfType<AudioManager>().Play("EnemyLaser");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Laser"))
        {
            spawnController.enemyIsAlive = false;
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("Explosion");
        }
    }


    public IEnumerator EnemyShipPlacement()
    {
        yield return new WaitForSeconds(4);

        enemyRB.position = endPosition;
    }
}
