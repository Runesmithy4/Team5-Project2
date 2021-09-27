using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject ship;
    public Vector3 shipStartingPos;
    public new GameObject camera;
    public Vector3 cameraStartingPos;
    public float xAngle, yAngle, zAngle;

    private void Start()
    {
        shipStartingPos = new Vector3(ship.transform.position.x, ship.transform.position.y);
        cameraStartingPos = new Vector3(camera.transform.position.x, camera.transform.position.y);
    }

    // Rotates the ship to the left around its pivot point
    public void OnLeftButtonClick()
    {
        ship.transform.Rotate(xAngle, yAngle, zAngle, Space.Self);
        ship.transform.position = shipStartingPos;
    }

    // Rotates the ship to the right around its pivot point
    public void OnRightButtonClick()
    {
        ship.transform.Rotate(xAngle, -yAngle, zAngle, Space.Self);
        ship.transform.position = shipStartingPos;
    }
}
