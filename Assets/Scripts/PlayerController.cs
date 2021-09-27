using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float sideMovement;
    
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.LeftArrow))
        {
            transform.position = new Vector3 (transform.position.x - sideMovement, transform.position.y);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            transform.position = new Vector3(transform.position.x + sideMovement, transform.position.y);
        }
    }
}
