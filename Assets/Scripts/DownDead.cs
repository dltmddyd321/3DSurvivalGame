using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DownDead : MonoBehaviour
{
    void Update()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(this.transform.position, Vector3.down, out hitInfo, 1f, LayerMask.GetMask("Floor")))
        {
            SceneManager.LoadScene("JumpOver");
        }
    }
}
