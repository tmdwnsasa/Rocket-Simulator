using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDraw : MonoBehaviour
{
    float x_vel;
    float y_vel;


    // Start is called before the first frame update
    void Start()
    {
        x_vel = transform.parent.GetComponent<RocketMove>().velocity_x;
        y_vel = transform.parent.GetComponent<RocketMove>().velocity_y;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
