using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotation : MonoBehaviour
{
    public static class AxisInput
    {
        public const string Horizontal = "Horizontal";
        public const string Vertical = "Vertical";
    }

    void Update()
    {
        float speed = Input.GetAxisRaw(AxisInput.Horizontal) * 1.0f;
        transform.Rotate(-0.005f, 0, 0);
    }
}
