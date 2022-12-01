using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        LockOnPlayer();
    }

    void LockOnPlayer()
    {
        //transform.position = new Vector3(target.position.x, 0, -10);

        if(target.transform.position.x < 0)
        {
            transform.position = new Vector3(0 , 0, -10);
        }

        else if(target.transform.position.x > 109)
        {
            transform.position = new Vector3(109, 0, -10);
        }

        else
        {
            transform.position = new Vector3(target.position.x, 0, -10);
        }
    }
}
