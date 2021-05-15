using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateShit : MonoBehaviour
{
    public GameObject Center;
    public float Speed;

    // Update is called once per frame
    void Update()
    {
        OrbitAround();
    }

    void OrbitAround()
    {
        transform.RotateAround(Center.transform.position, Vector3.down, Speed * Time.deltaTime);
    }
}
