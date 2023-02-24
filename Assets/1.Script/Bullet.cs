using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int shotpower;   //포 발사 속도
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

        if (collision.gameObject.tag == "feature") //지형지물
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "tank") //탱크
        {
            //copy_exp.transform.position = collision.transform.position;
            Destroy(gameObject);
        }
    }
}
