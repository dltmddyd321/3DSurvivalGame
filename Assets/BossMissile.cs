using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMissile : MonoBehaviour
{
    public int damage;
    public bool isMelee;
    public Transform Target;
    NavMeshAgent nav;
    
    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        nav.SetDestination(Target.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
