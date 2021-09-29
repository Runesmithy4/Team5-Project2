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

    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject spaceShip;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject shieldPanel;
    [SerializeField] private GameObject livesPanel;

    public int lives;
    [SerializeField] private float sideMovement;

    public int score;

    void Start()
    {
        score = 0;
        deathPanel.SetActive(false);
        shieldPanel.SetActive(false);
        livesPanel.GetComponent<Image>().color = Color.green;
    }
    
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            transform.position = new Vector3(sideMovement, transform.position.y);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            transform.position = new Vector3(-sideMovement, transform.position.y);
        }
        if(Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            transform.position = new Vector3(0, transform.position.y);
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
        GameObject lazerOne = Instantiate<GameObject>(lazerPrefab);
        lazerOne.transform.position = lazerSpawnOne.transform.position;
        Rigidbody lazerOneRB = lazerOne.GetComponent<Rigidbody>();
        lazerOneRB.velocity = Vector3.back * lazerSpeed;

        StartCoroutine(DestroyLazerAfterTime(lazerOne, lifetime));
        
        GameObject lazerTwo = Instantiate<GameObject>(lazerPrefab);
        lazerTwo.transform.position = lazerSpawnTwo.transform.position;
        Rigidbody lazerTwoRB = lazerTwo.GetComponent<Rigidbody>();
        lazerTwoRB.velocity = Vector3.back * lazerSpeed;

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
                break;
        }
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Current Score: " + score;
    }
}
