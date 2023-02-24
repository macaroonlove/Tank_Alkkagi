using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int shotpower;   //�� �߻� �ӵ�
    public GameObject exp;

    void Start()
    {
        shotpower = 30000;
        GetComponent<Rigidbody>().AddForce(transform.forward * shotpower);
    }

    void OnCollisionEnter(Collision collision)
    {
        //GameObject copy_exp = Instantiate(exp) as GameObject;

        //copy_exp.transform.localScale = new Vector3(10f, 10f, 10f);

        if (collision.gameObject.tag == "feature") //��������
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "tank") //��ũ
        {
            //copy_exp.transform.position = collision.transform.position;
            Destroy(gameObject);
        }
    }
}
