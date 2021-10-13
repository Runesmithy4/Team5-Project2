using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Public variables used for instantiated lazers
    public GameObject lazerPrefab;
    public Transform lazerSpawnOne;
    public Transform spreadSpawnOne;
    public Transform spreadSpawnTwo;
    public float lazerSpeed = 30;
    public float lifetime = 3;
    public float shootCDTime;
    private float nextFire;


    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject spaceShip;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject shieldPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private List<GameObject> livesPanels;
    [SerializeField] private Text scoreText;
    [SerializeField] UIControllerInGame uiGame;
    [SerializeField] SpawnController enemySpawner;

    [SerializeField] private float sideMovement;

    public int lives;
    public int score;
    public bool spreadShot;

    void Start()
    {
        score = 0;
        deathPanel.SetActive(false);
        shieldPanel.SetActive(false);
        CheckIfDead();
        Time.timeScale = 1;
        spreadShot = false;
    }
    
    void Update()
    {
        // Moves the player to one of three lanes
        if(Input.GetKeyUp(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(-sideMovement, transform.position.y);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            transform.position = new Vector3(sideMovement, transform.position.y);
        }
        if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            transform.position = new Vector3(0, transform.position.y);
        }

        //When player hits spacebar the ship will shoot calling the Fire() function
       
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire && !deathPanel.activeInHierarchy)
        {
            nextFire = Time.time + shootCDTime;
            Fire();
            FindObjectOfType<AudioManager>().Play("Laser");
        }       

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (!pausePanel.activeInHierarchy && !deathPanel.activeInHierarchy)
            {
                pausePanel.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
                pausePanel.SetActive(false);
            }
        }
    }

    //Instantiates two lazer projectiles at two different starting locations (both are children of the parent ship). Then starts a IEnumerator to destroy the object after some time.
    private void Fire()
    {
        if (spreadShot == true)
        {
            GameObject lazerSpreadOne = Instantiate(lazerPrefab, new Vector3(lazerSpawnOne.transform.position.x, lazerSpawnOne.transform.position.y), lazerSpawnOne.transform.rotation);
            Rigidbody lazerSpreadOneRB = lazerSpreadOne.GetComponent<Rigidbody>();
            lazerSpreadOneRB.velocity = (spaceShip.transform.rotation * Vector3.back) * lazerSpeed;

            GameObject lazerTwo = Instantiate(lazerPrefab, new Vector3(spreadSpawnOne.transform.position.x, spreadSpawnOne.transform.position.y), spreadSpawnOne.transform.rotation);
            Rigidbody lazerTwoRB = lazerTwo.GetComponent<Rigidbody>();
            lazerTwoRB.velocity = (spreadSpawnOne.transform.rotation * Vector3.down) * lazerSpeed;

            GameObject lazerThree = Instantiate(lazerPrefab, new Vector3(spreadSpawnTwo.transform.position.x, spreadSpawnTwo.transform.position.y), spreadSpawnTwo.transform.rotation);
            Rigidbody lazerThreeRB = lazerThree.GetComponent<Rigidbody>();
            lazerThreeRB.velocity = (spreadSpawnTwo.transform.rotation * Vector3.down) * lazerSpeed;

            StartCoroutine(DestroyLazerAfterTime(lazerSpreadOne, lifetime));
            StartCoroutine(DestroyLazerAfterTime(lazerTwo, lifetime));
            StartCoroutine(DestroyLazerAfterTime(lazerThree, lifetime));
        }
        else
        {

            GameObject lazerOne = Instantiate(lazerPrefab, new Vector3(lazerSpawnOne.transform.position.x, lazerSpawnOne.transform.position.y), lazerSpawnOne.transform.rotation);
            Rigidbody lazerOneRB = lazerOne.GetComponent<Rigidbody>();
            lazerOneRB.velocity = (spaceShip.transform.rotation * Vector3.back) * lazerSpeed;

            /*GameObject lazerTwo = Instantiate(lazerPrefab, new Vector3(lazerSpawnTwo.transform.position.x, lazerSpawnTwo.transform.position.y), lazerSpawnTwo.transform.rotation);
            Rigidbody lazerTwoRB = lazerTwo.GetComponent<Rigidbody>();
            lazerTwoRB.velocity = (spaceShip.transform.rotation * Vector3.back) * lazerSpeed;
            */

            StartCoroutine(DestroyLazerAfterTime(lazerOne, lifetime));
            //StartCoroutine(DestroyLazerAfterTime(lazerTwo, lifetime));
        }
    }

    //Destroys object after a set amount of time.
    private IEnumerator DestroyLazerAfterTime(GameObject lazer, float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(lazer);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Gives the player a shield
        if (other.CompareTag("ShieldPickup"))
        {
            Destroy(other.gameObject);
            shield.SetActive(true);
            shieldPanel.SetActive(true);
        }

        // Gives the player an extra life
        if (other.CompareTag("LifePickup"))
        {
            Destroy(other.gameObject);

            if (lives < 3)
            {
                lives += 1;
            }
            CheckIfDead();
        }

        if (other.CompareTag("SpreadShot"))
        {
            Destroy(other.gameObject);
            StartCoroutine(SpreadShotActivate(5));
        }

        // Removes a life, and checks if the player is dead
        if (other.CompareTag("MeteorNormal") || other.CompareTag("MeteorShield") || other.CompareTag("MeteorLife") || other.CompareTag("EnemyLaser"))
        {
            print(other.tag);

            if(shield.activeInHierarchy)
            {
                Destroy(other.gameObject);
                shield.SetActive(false);
                shieldPanel.SetActive(false);
            }
            else
            {
                Destroy(other.gameObject);
                lives -= 1;
                FindObjectOfType<AudioManager>().Play("Explosion");
                CheckIfDead();
            }
        }
    }

    // Checks if the player is dead, and if not changes the color of the health bar to display the amount of lives left
    public void CheckIfDead()
    {
        switch (lives)
        {
            case 3:
                livesPanels[0].SetActive(true);
                livesPanels[1].SetActive(false);
                livesPanels[2].SetActive(false);
                break;
            case 2:
                livesPanels[1].SetActive(true);
                livesPanels[0].SetActive(false);
                livesPanels[2].SetActive(false);
                break;
            case 1:
                livesPanels[2].SetActive(true);
                livesPanels[0].SetActive(false);
                livesPanels[1].SetActive(false);
                break;
            default:
                deathPanel.SetActive(true);
                enemySpawner.StopAllCoroutines();
                Time.timeScale = 0;
                FindObjectOfType<AudioManager>().Play("Explosion");
                break;
        }
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Current Score: " + score;
    }

    public IEnumerator SpreadShotActivate(float delay)
    {
        spreadShot = true;
        yield return new WaitForSeconds(delay);
        spreadShot = false;

    }
}
