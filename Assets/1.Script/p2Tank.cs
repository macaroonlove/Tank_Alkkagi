using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p2Tank : MonoBehaviour
{
    Rigidbody rigid;

    public Camera[] Player_Camera;
    public Camera Tank_Camera;
    public GameObject[] player;

    public int goBackSpeed;
    public float goBack;

    public int rotTankSpeed;
    public float rotTank;

    public int rotTurretSpeed;
    public float rotTurret;
    public GameObject tank_head_Benchmark;

    public int updownGunSpeed;
    public float updownGun;
    public GameObject tank_gun_Benchmark;

    public int shotPower;
    public GameObject bulletPrefab;
    public GameObject spPoint;
    public float DestroyTime;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        goBackSpeed = 300;
        rotTankSpeed = 30;
        rotTurretSpeed = 20;
        updownGunSpeed = 10000;
        shotPower = 600;
        DestroyTime = 3.0f;
    }

    void FixedUpdate()
    {
        Move();         //탱크의 움직임
    }

    void Update()
    {
        Tank_Camera.depth = 2;
        fine_tuning();  //탱크의 터렛회전, 포 상하 조절
        shot();         //포탄 쏘기(생성&삭제)
    }

    void Move()
    {
        //전진&후진
        goBack = Input.GetAxis("Vertical");
        //transform.Translate(Vector3.forward * goBack * goBackSpeed * Time.deltaTime);
        rigid.AddRelativeForce(Vector3.forward * goBack * goBackSpeed * Time.deltaTime, ForceMode.Impulse);
        //좌우 회전
        rotTank = Input.GetAxis("Horizontal");
        //transform.Rotate(Vector3.up * rotTank * rotTankSpeed * Time.deltaTime);
        float turn = rotTank * rotTankSpeed * Time.deltaTime;
        rigid.rotation = rigid.rotation * Quaternion.Euler(0f, turn, 0f);
    }

    void fine_tuning()
    {
        //터렛 회전
        rotTurret = Input.GetAxis("rotTurret");
        tank_head_Benchmark.transform.Rotate(Vector3.forward * rotTurret * rotTurretSpeed * Time.deltaTime);
        //포 상하 조절
        updownGun = Input.GetAxis("Mouse ScrollWheel");
        tank_gun_Benchmark.transform.Rotate(Vector3.right * updownGun * updownGunSpeed * Time.deltaTime);
        //포 상하 조절 제한
        Vector3 ang = tank_gun_Benchmark.transform.eulerAngles;
        if (ang.x > 180)
            ang.x -= 360;
        ang.x = Mathf.Clamp(ang.x, -95.0f, -75.0f);
        tank_gun_Benchmark.transform.eulerAngles = ang;
    }

    void shot()
    {
        if (Input.GetMouseButtonDown(0))
        {

            GameObject bullet = Instantiate(bulletPrefab, spPoint.transform.position, spPoint.transform.rotation) as GameObject;    //포탄 생성
            Rigidbody rigidBullet = bullet.GetComponent<Rigidbody>();                                       //생성한 포탄에 물리효과를 줄 수 있게 초기화
            rigidBullet.AddForce(tank_head_Benchmark.transform.forward * shotPower);                        //tank_head가 보고있는 방향으로 포탄에 힘을 주기
            Destroy(bullet, DestroyTime);

            End();
        }
    }

    void End()
    {
        GetComponent<p2Tank>().enabled = false;
        player[0].GetComponent<Player1>().enabled = true;
        player[1].GetComponent<Player2>().enabled = false;
        Invoke("CameraEnd", 2.5f);
    }

    void CameraEnd()
    {
        Tank_Camera.depth = -1;
    }

    //void OnDisable()
    //{
    //    if (tank_being == true)
    //    {
    //        Tank_Camera.depth = -1;
    //        player[0].GetComponent<Player1>().enabled = true;
    //        player[1].GetComponent<Player2>().enabled = false;
    //    }
    //}
}
