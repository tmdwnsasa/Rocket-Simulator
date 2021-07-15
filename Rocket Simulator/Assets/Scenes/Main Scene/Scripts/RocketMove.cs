using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMove : MonoBehaviour
{
    public bool selectedrocketstate = true;
    public int rotate_state;
    public bool Engine_start;

    private int engine_cnt;
    public float fuel_usage = 0.0f;
    public float fuel_full;
    public float rotate_velocity;

    private float velocity_x;
    public float velocity_y;


    // Start is called before the first frame update
    void Awake()
    {
        selectedrocketstate = true;
    }

    // Update is called once per frame
    void Update()
    {
        MakeCenter();
        CalculateEngine();
        CalculateFuel();
        //Gravity();

        Movement();
        if (rotate_state == 1)
        {
            TurnLeft();
        }
        else if (rotate_state == 2)
        {
            TurnRight();
        }
        //transform.position = transform.forward * Time.deltaTime;
    }

    private void MakeCenter()
    {
        Vector3 Center = new Vector3(0, 0, 0);
        Vector3 ChangedAmount = new Vector3(0, 0, 0);

        for (int i = 0; i < transform.childCount; i++)
        {
            Center = Center + transform.GetChild(i).transform.position;
        }
        Center = Center / transform.childCount;

        ChangedAmount = Center - transform.position;
        transform.position = Center;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.position = transform.GetChild(i).transform.position - ChangedAmount;
        }

    }

    private void Movement()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, rotate_velocity));
        if (Engine_start == true && fuel_full > fuel_usage)
        {
            transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(velocity_x, velocity_y));
            fuel_usage += 0.01f;
        }
    }

    private void TurnLeft()
    {
        if (rotate_velocity >= -1.0f)
        {
            rotate_velocity -= 0.001f;
        }
    }

    private void TurnRight()
    {
        if (rotate_velocity <= 1.0f)
        {
            rotate_velocity += 0.001f;
        }
    }

    private void CalculateEngine()
    {
        engine_cnt = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<ObjectMove>().Obj_tag == Object_type.jet_engine01)
            {
                engine_cnt++;
            }
        }
        if (Engine_start == true)
            velocity_y += (float)engine_cnt * 1.0f;
    }

    private void CalculateFuel()
    {
        int cnt = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<ObjectMove>().Obj_tag == Object_type.fuel_tank01)
            {
                cnt++;
            }
        }
        fuel_full = 100 * cnt;
    }

    private void Gravity()
    {
        velocity_y -= 1.0f;
    }
}