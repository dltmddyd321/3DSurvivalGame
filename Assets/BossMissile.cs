using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMissile : MonoBehaviour
{
    public int damage; // 미사일의 데미지를 지정할 변수
    public bool isMelee; // 유도탄의 범위 계산 변수
    public Transform Target; // 유도탄에 추적당할 플레이어를 담을 변수
    NavMeshAgent nav; // 유도탄이 플레이어를 쫒아오기 위한 내비게이션 AI 적용
    
    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        nav.SetDestination(Target.position);
        // 내비게이션 AI의 목적지를 지정하는 함수로서 플레이어의 위치를 지정
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {// 충돌물체의 태그가 플레이어라면 미사일이 사라지는(터지는) 반응
            Destroy(gameObject);
        }
    }
}
