using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    [SerializeField]
    private string animalName; // 동물이름
    [SerializeField] private int hp; //동물 체력

    [SerializeField] private float walkSpeed; //걷는 속도
    [SerializeField] private float runSpeed;
    private float applySpeed;

    private Vector3 direction; // 방향

    private bool isAction;
    private bool isWalking;
    private bool isRunning;
    private bool isDead;

    [SerializeField]
    private float walkTime; // 걷는 시간
    [SerializeField]
    private float waitTime;
    [SerializeField]
    private float runTime;
    private float currentTime;

    //필요한 컴포넌트
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private BoxCollider bCol;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = waitTime;
        isAction = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Move();
            Rotation();
            ElapseTime();

        }

    }

    private void Move()
    {
        if (isWalking || isRunning)
            rigid.MovePosition(transform.position + (transform.forward * applySpeed * Time.deltaTime));
    }

    private void Rotation()
    {
        if (isWalking || isRunning)
        {
            Vector3 _rotation = Vector3.Lerp(transform.eulerAngles, new Vector3(0f, direction.y, 0f), 0.01f);
            rigid.MoveRotation(Quaternion.Euler(_rotation));
        }
    }

    private void ElapseTime()
    {
        if(isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
                ReSet();
        }
    }

    private void ReSet()
    {
        isWalking = false;
        isRunning = false;
        isAction = true;
        applySpeed = walkSpeed;
        anim.SetBool("Walking", isWalking);
        anim.SetBool("Running", isRunning);
        direction.Set(0f, Random.Range(0f, 360f), 0f); // 어느방향으로든 랜덤하게 움직인다.
        RandomAction();
    }

    private void RandomAction()
    {
        isAction = true;

        int _random = Random.Range(0 ,2); // 대기, 걷기

        if (_random == 0)
            Wait();
        else if (_random == 1)
            TryWalk();
    }

    private void Wait()
    {
        currentTime = waitTime;
        Debug.Log("대기");
    }

    private void TryWalk()
    {
        isWalking = true;
        anim.SetBool("Walking", isWalking);
        currentTime = walkTime;
        applySpeed = walkSpeed;
        Debug.Log("걷기");
    }

    private void Run(Vector3 _targetPos)
    {
        direction = Quaternion.LookRotation(transform.position - _targetPos).eulerAngles;

        currentTime = runTime;
        isWalking = false;
        isRunning = true;
        applySpeed = runSpeed;
        anim.SetBool("Running", isRunning);
    }

    private void Dead()
    {
        isWalking = false;
        isRunning = false;
        isDead = true;
        anim.SetTrigger("Dead");
    }

    public void Damage(int _dmg, Vector3 _targetPos)
    {
        if (!isDead)
        {
            hp -= _dmg;

            if (hp <= 0)
            {
                Dead();
                return;
            }
            anim.SetTrigger("Jump");
            Run(_targetPos);
        }
    }
}
