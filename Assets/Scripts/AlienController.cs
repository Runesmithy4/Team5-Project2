using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienController : MonoBehaviour
{
    [SerializeField] private List<Vector3> alienPositions;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform laserSpawn;

    private Rigidbody alienRB;
    private Vector3 nextPosition;
    private Vector3 currentPosition;

    private float shootTimer = 5;
    private float timer = 0;
    private float laserSpeed = 25f;
    private float alienSpeed = 5f;
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        alienRB = GetComponent<Rigidbody>();
        currentPosition = transform.position;

        int random = Random.Range(0, 3);
        nextPosition = new Vector3(alienPositions[random].x, alienPositions[random].y, alienPositions[random].z);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if (alienRB.position != nextPosition)
        {
            Vector3 newPosition = Vector3.MoveTowards(alienRB.position, nextPosition, alienSpeed * Time.deltaTime);
            alienRB.MovePosition(newPosition);
            StartCoroutine(AlienMovement());
        }
        
        Shoot();
    }

    private void Shoot()
    {
        if (timer > shootTimer)
        {
            GameObject laser = Instantiate(laserPrefab, new Vector3(laserSpawn.transform.position.x, laserSpawn.transform.position.y, laserSpawn.transform.position.z), laserSpawn.rotation);
            Rigidbody laserRB = laser.GetComponent<Rigidbody>();
            laserRB.velocity = (laserSpawn.transform.rotation * Vector3.up) * laserSpeed;

            timer = 0;
            FindObjectOfType<AudioManager>().Play("EnemyLaser");

            int random = Random.Range(0, 3);
            nextPosition = new Vector3(alienPositions[random].x, alienPositions[random].y, alienPositions[random].z);
        }
    }

    public IEnumerator AlienMovement()
    {
        yield return new WaitForSeconds(20);

        alienRB.position = nextPosition;
    }
}
