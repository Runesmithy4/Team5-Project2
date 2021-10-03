using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Public variables used for instantiated lazers
    public GameObject lazerPrefab;
    public Transform lazerSpawnOne;
    public Transform lazerSpawnTwo;
    public float lazerSpeed = 30;
    public float lifetime = 3;

    [SerializeField] private List<GameObject> meteorSpawners;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject spaceShip;
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject shieldPanel;
    [SerializeField] private GameObject livesPanel;
    [SerializeField] private Text scoreText;
    [SerializeField] UIControllerInGame uiGame;

    [SerializeField] private float sideMovement;
    [SerializeField] private Vector3 shipStartingPos;
    [SerializeField] private Vector3 cameraStartingPos;
    [SerializeField] private float yAngle;

    public int lives;
    public int score;

    void Start()
    {
        score = 0;
        deathPanel.SetActive(false);
        shieldPanel.SetActive(false);
        livesPanel.GetComponent<Image>().color = Color.green;

        foreach (GameObject spawner in meteorSpawners)
        {
            spawner.SetActive(true);
        }

        shipStartingPos = new Vector3(spaceShip.transform.position.x, spaceShip.transform.position.y);
        cameraStartingPos = new Vector3(camera.transform.position.x, camera.transform.position.y);
    }
    
    void Update()
    {
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
        if(Input.GetKeyUp(KeyCode.A))
        {
            camera.transform.SetParent(spaceShip.transform);
            spaceShip.transform.Rotate(0, yAngle, 0, Space.Self);
            spaceShip.transform.position = shipStartingPos;
            camera.transform.SetParent(null);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            camera.transform.SetParent(spaceShip.transform);
            spaceShip.transform.Rotate(0, -yAngle, 0, Space.Self);
            spaceShip.transform.position = shipStartingPos;
            camera.transform.SetParent(null);
        }
        //When player hits spacebar the ship will shoot calling the Fire() function
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
            FindObjectOfType<AudioManager>().Play("Laser");
        }
    }
    
    //Instantiates two lazer projectiles at two different starting locations (both are children of the parent ship). Then starts a IEnumerator to destroy the object after some time.
    private void Fire()
    {
        GameObject lazerOne = Instantiate(lazerPrefab, new Vector3(lazerSpawnOne.transform.position.x, lazerSpawnOne.transform.position.y), lazerSpawnOne.transform.rotation);
        Rigidbody lazerOneRB = lazerOne.GetComponent<Rigidbody>();
        lazerOneRB.velocity = (spaceShip.transform.rotation * Vector3.back) * lazerSpeed;

        GameObject lazerTwo = Instantiate(lazerPrefab, new Vector3(lazerSpawnTwo.transform.position.x, lazerSpawnTwo.transform.position.y), lazerSpawnTwo.transform.rotation);
        Rigidbody lazerTwoRB = lazerTwo.GetComponent<Rigidbody>();
        lazerTwoRB.velocity = (spaceShip.transform.rotation * Vector3.back) * lazerSpeed;

        StartCoroutine(DestroyLazerAfterTime(lazerOne, lifetime));
        StartCoroutine(DestroyLazerAfterTime(lazerTwo, lifetime));
    }

    //Destroys object after a set amount of time.
    private IEnumerator DestroyLazerAfterTime(GameObject lazer, float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(lazer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShieldPickup"))
        {
            Destroy(other.gameObject);
            shield.SetActive(true);
            shieldPanel.SetActive(true);
        }

        if (other.CompareTag("MeteorNormal") || other.CompareTag("MeteorShield"))
        {
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
                CheckIfDead();
            }
        }
    }

    public void CheckIfDead()
    {
        switch (lives)
        {
            case 3:
                break;
            case 2:
                livesPanel.GetComponent<Image>().color = Color.yellow;
                break;
            case 1:
                livesPanel.GetComponent<Image>().color = Color.red;
                break;
            default:
                deathPanel.SetActive(true);
                spaceShip.SetActive(false);

                foreach (GameObject spawner in meteorSpawners)
                {
                    spawner.SetActive(false);
                }

                break;
        }
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Current Score: " + score;
        uiGame.UpdateHighScore();
    }
}
