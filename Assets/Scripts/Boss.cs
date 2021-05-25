using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boss : Enemy
{
    [SerializeField] public GameObject HP;

    public GameObject missile;
    public Transform missilePortA;
    public Transform missilePortB;

    // 플레이어 움직임 예측 벡터
    Vector3 lookVec;

    // 점프 공격할 위치 벡터
    Vector3 tauntVec;


    public float maxHP;
    public int currentHP;
    protected bool isDead; // 죽었는지 판별
    public bool isLook;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        meshs = GetComponentsInChildren<MeshRenderer>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        nav.isStopped = true;
        StartCoroutine(Think());
    }


    void Update()
    {
        if(isDead)
        {
            StopAllCoroutines();
            return;
        }
        // 플레이어를 예측해서 쳐다봄
        if (isLook)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            lookVec = new Vector3(h, 0, v) * 5f;
            transform.LookAt(target.position + lookVec);
        }
        else
            nav.SetDestination(tauntVec);
    }

    public virtual void Damage(int _dmg, Vector3 _targetPos)
    {
        if (!isDead)
        {
            currentHP -= _dmg;
            HP.GetComponent<Slider>().value = currentHP / maxHP;

            if (currentHP <= 0)
            {
                currentHP = 0;
                Dead();
                return;
            }
        }
    }

    protected void Dead()
    {
        isDead = true;
        anim.SetTrigger("Die");
    }

    IEnumerator Think()
    {
        yield return new WaitForSeconds(0.1f);

        int ranAction = Random.Range(0, 5);
        switch (ranAction)
        {
            // 미사일 발사
            case 0:
            case 1:
                StartCoroutine(MissileShot());
                break;

            // 돌 굴리기
            case 2:
            case 3:
                StartCoroutine(RockShot());
                break;

            // 점프 공격
            case 4:
                StartCoroutine(Taunt());
                break;
        }
    }

    IEnumerator MissileShot()
    {
        anim.SetTrigger("Shot");
        yield return new WaitForSeconds(0.2f);
        GameObject instantMissileA = Instantiate(missile, missilePortA.position, missilePortA.rotation);
        BossMissile bossMissile = instantMissileA.GetComponent<BossMissile>();
        bossMissile.Target = target;

        yield return new WaitForSeconds(0.3f);
        GameObject instantMissileB = Instantiate(missile, missilePortB.position, missilePortB.rotation);
        BossMissile bossMissileB = instantMissileB.GetComponent<BossMissile>();
        bossMissileB.Target = target;

        yield return new WaitForSeconds(2f);
        StartCoroutine(Think());
    }

    IEnumerator RockShot()
    {
        isLook = false;
        anim.SetTrigger("Bigshot");
        Instantiate(bullet, transform.position, transform.rotation);
        yield return new WaitForSeconds(3f);

        isLook = true;
        StartCoroutine(Think());
    }
    IEnumerator Taunt()
    {
        tauntVec = target.position + lookVec;

        isLook = false;
        nav.isStopped = false;
        boxCollider.enabled = false;
        anim.SetTrigger("Taunt");

        yield return new WaitForSeconds(1.5f);
        meleeArea.enabled = true;

        yield return new WaitForSeconds(0.5f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(1f);
        isLook = true;
        nav.isStopped = true;
        boxCollider.enabled = true;

        StartCoroutine(Think());
    }
}
