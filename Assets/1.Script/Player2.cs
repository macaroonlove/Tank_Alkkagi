using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public Camera Camera;
    public GameObject[] Tank;

    void Start()
    {
        Camera.depth = 1;
    }

    void Update()
    {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            for (int i = 0; i < 9; i++)
            {
                if (hit.transform.gameObject.name == "p2Tank" + (i + 1) && Input.GetMouseButtonDown(0))
                {
                    Tank[i].GetComponent<p2Tank>().enabled = true;
                }
            }
        }
    }
}
