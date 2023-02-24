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
        Move();         //��ũ�� ������
    }

    void Update()
    {
        Tank_Camera.depth = 2;
        fine_tuning();  //��ũ�� �ͷ�ȸ��, �� ���� ����
        shot();         //��ź ���(����&����)
    }

    void Move()
    {
        //����&����
        goBack = Input.GetAxis("Vertical");
        //transform.Translate(Vector3.forward * goBack * goBackSpeed * Time.deltaTime);
        rigid.AddRelativeForce(Vector3.forward * goBack * goBackSpeed * Time.deltaTime, ForceMode.Impulse);
        //�¿� ȸ��
        rotTank = Input.GetAxis("Horizontal");
        //transform.Rotate(Vector3.up * rotTank * rotTankSpeed * Time.deltaTime);
        float turn = rotTank * rotTankSpeed * Time.deltaTime;
        rigid.rotation = rigid.rotation * Quaternion.Euler(0f, turn, 0f);
    }

    void fine_tuning()
    {
        //�ͷ� ȸ��
        rotTurret = Input.GetAxis("rotTurret");
        tank_head_Benchmark.transform.Rotate(Vector3.forward * rotTurret * rotTurretSpeed * Time.deltaTime);
        //�� ���� ����
        updownGun = Input.GetAxis("Mouse ScrollWheel");
        tank_gun_Benchmark.transform.Rotate(Vector3.right * updownGun * updownGunSpeed * Time.deltaTime);
        //�� ���� ���� ����
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

            GameObject bullet = Instantiate(bulletPrefab, spPoint.transform.position, spPoint.transform.rotation) as GameObject;    //��ź ����
            Rigidbody rigidBullet = bullet.GetComponent<Rigidbody>();                                       //������ ��ź�� ����ȿ���� �� �� �ְ� �ʱ�ȭ
            rigidBullet.AddForce(tank_head_Benchmark.transform.forward * shotPower);                        //tank_head�� �����ִ� �������� ��ź�� ���� �ֱ�
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
