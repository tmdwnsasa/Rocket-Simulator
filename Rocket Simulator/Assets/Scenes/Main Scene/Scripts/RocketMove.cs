using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMove : MonoBehaviour
{
    private Rigidbody2D rb2D;

    public bool selectedrocketstate = true;
    public int rotate_state;
    public bool Engine_start;
    public bool gravity_enable;

    private int engine_cnt;
    public float fuel_usage = 0.0f;
    public float fuel_full;
    public float rotate_velocity;
    public float velocity_y_gravity;

    private float velocity_x;
    public float velocity_y;


    void Awake()
    {
        rb2D = transform.GetComponent<Rigidbody2D>();
        gravity_enable = true;
        selectedrocketstate = true;
    }

    void Update()
    {
        MakeCenter();
        CalculateEngine();
        CalculateFuel();
        if(gravity_enable == true)
        {
            Gravity();
        }

        Movement();

        if (rotate_state == 1)
        {
            TurnLeft();
        }
        else if (rotate_state == 2)
        {
            TurnRight();
        }
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
        transform.GetComponent<Rigidbody2D>().velocity = Vector2.up * velocity_y;
        Debug.Log(velocity_y);
        if (Engine_start == true && fuel_full > fuel_usage)
        {
            fuel_usage += 0.01f;
        }
    }

    private void TurnLeft()
    {
        if (rotate_velocity <= 1.0f)
        {
            rotate_velocity += 0.001f;
        }
    }

    private void TurnRight()
    {
        if (rotate_velocity >= -1.0f)
        {
            rotate_velocity -= 0.001f;
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
        {
            velocity_y += (float)engine_cnt * 1.5f;
            Debug.Log(velocity_y);
        }
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
        fuel_full = 70 * cnt;
    }

    private void Gravity()
    {
        velocity_y -= 0.98f;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Earth")
        {
            gravity_enable = false;
            velocity_y = 0;
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Earth")
        {
            gravity_enable = false;
            velocity_y = 0;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Earth")
        {
            gravity_enable = true;
        }
    }
}