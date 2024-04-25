using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayCube : MonoBehaviour
{
    private int speed = 100;
    private Vector3 vAxis = new(0, 1, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(vAxis * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(- vAxis * speed * Time.deltaTime);
        }
    }
}
