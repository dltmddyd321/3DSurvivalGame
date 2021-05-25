using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRock : MonoBehaviour
{
    public int damage; // 바위 데미지
    public bool isMelee; // 바위 데미지 범위
    Rigidbody rigid; // 물리 효과 구현

    // 회전 파워, 크기
    float angularPower = 2;
    float scaleValue = 0.1f;

    bool isShoot; // 생성되었는지 아닌지

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        StartCoroutine(GainPowerTimer());
        StartCoroutine(GainPower());
    }

    // 쏘는 타이밍 관리
    IEnumerator GainPowerTimer()
    {
        // 기모으는 시간이 2.2초후 발사
        yield return new WaitForSeconds(2.2f);
        isShoot = true;
    }
    IEnumerator GainPower()
    {
        while (!isShoot)
        {
            angularPower += 0.02f;
            scaleValue += 0.005f; 
            transform.localScale = Vector3.one * scaleValue; 
            // 바위가 점점 커지는 것과 동시에 전방으로 굴러온다.
            rigid.AddTorque(transform.right * angularPower, ForceMode.Acceleration);
            // 바위의 RigidBody에 힘을 생성하여 Vector로 방향을 설정하여 전방에 힘을 가한다.
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

}
