using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCameraMove : MonoBehaviour
{
    public GameObject player1;
    //Vector3 cameramove = new Vector3(0, 746, 0);
    void Start()
    {
        
    }

    void Update()
    {
        if (750 != transform.position.y)
        {
            transform.Translate(new Vector3(0, 0, 5));
            transform.Rotate(Vector3.forward * 10);
        }
        else
        {
            player1.GetComponent<Player1>().enabled = true;
            gameObject.SetActive(false);
        }
 
    }
}
