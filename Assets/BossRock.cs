using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRock : MonoBehaviour
{
    public int damage;
    public bool isMelee;
    Rigidbody rigid;

    // 회전 파워, 크기
    float angularPower = 2;
    float scaleValue = 0.1f;

    bool isShoot;

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
            rigid.AddTorque(transform.right * angularPower, ForceMode.Acceleration);
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
