using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private int lives;
    [SerializeField] private float sideMovement;

    void Start()
    {
        deathPanel.SetActive(false);
    }
    
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            if (transform.position.x + sideMovement <= sideMovement)
            {
                transform.position = new Vector3(transform.position.x + sideMovement, transform.position.y);
            }
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            if (transform.position.x - sideMovement >= -sideMovement)
            {
                transform.position = new Vector3(transform.position.x - sideMovement, transform.position.y);
            }
        }

        //When player hits spacebar the ship will shoot calling the Fire() function
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
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
            shield.SetActive(true);
        }

        if (other.CompareTag("Enemy"))
        {
            if(shield.activeInHierarchy)
            {
                shield.SetActive(false);
            }
            else
            {
                lives -= 1;
                CheckIfDead();
            }
        }
    }

    private void CheckIfDead()
    {
        if (lives <= 0)
        {
            deathPanel.SetActive(true);
            spaceShip.SetActive(false);
        }
    }
}
