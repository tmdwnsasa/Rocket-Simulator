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
    public bool CheckAirFriction;

    private int engine_cnt;
    public float fuel_usage = 0.0f;
    public float fuel_full;

    public float rotate_velocity;
    public float Zrotate;
    public float velocity_x;
    public float velocity_y;
    private float grav_x, grav_y;


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
        AirFriction();
        
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

        DrawLine();
    }

    private void MakeCenter()
    {
        Vector3 Center = new Vector3(0, 0, 0);
        Vector3 ChangedAmount = new Vector3(0, 0, 0);

        for (int i = 1; i < transform.childCount; i++)
        {
            Center = Center + transform.GetChild(i).transform.position;
        }
        Center = Center / (transform.childCount-1);

        ChangedAmount = Center - transform.position;
        transform.position = Center;

        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.position = transform.GetChild(i).transform.position - ChangedAmount;
        }
    }

    private void Movement()
    {
        Zrotate = rotate_velocity;
        if (Zrotate == 0)
            transform.GetComponent<Rigidbody2D>().freezeRotation = true;
        else
            transform.GetComponent<Rigidbody2D>().freezeRotation = false;
        double temp = System.Math.Round((double)(Zrotate * 100.0f)) / 100.0f;
        //Debug.Log(temp);
        transform.Rotate(new Vector3(0.0f, 0.0f, (float)temp));


        transform.GetComponent<Rigidbody2D>().velocity = Vector2.up * velocity_y + Vector2.right * velocity_x;
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

        for (int i = 1; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<ObjectMove>().Obj_tag == Object_type.jet_engine01)
            {
                engine_cnt++;
            }
        }

        if (Engine_start == true && fuel_full > fuel_usage)
        {
            //Debug.Log(transform.eulerAngles.z);
            //Debug.Log("X값 : " + -(float)System.Math.Sin(System.Math.PI * transform.eulerAngles.z / 180.0));
            //Debug.Log("Y값 : " + (float)System.Math.Cos(System.Math.PI * transform.eulerAngles.z / 180.0));
            velocity_x += (float)engine_cnt * 1.0f * -(float)System.Math.Sin(System.Math.PI * transform.eulerAngles.z / 180.0);
            velocity_y += (float)engine_cnt * 1.0f * (float)System.Math.Cos(System.Math.PI * transform.eulerAngles.z / 180.0);
            Debug.Log(velocity_y);
            //velocity_y += (float)engine_cnt * 1.5f;
        }
    }

    private void CalculateFuel()
    {
        int cnt = 0;
        for (int i = 1; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<ObjectMove>().Obj_tag == Object_type.fuel_tank01)
            {
                cnt++;
            }
        }
        fuel_full = 7000 * cnt;
    }

    private void Gravity()
    {
        velocity_x -= 1.00f * transform.childCount * 0.2f * grav_x;
        velocity_y -= 1.00f * transform.childCount * 0.2f * grav_y;
    }

    private void DrawLine()
    {
        Vector3[] Varrays;
        Vector3 OrbitPos = transform.position;
        float temp_X = velocity_x;
        float temp_Y = velocity_y;
        GameObject OrbitLine;

        Varrays = new Vector3[2000];
        transform.Find("Orbit").position = transform.position;
        transform.Find("Orbit").transform.SetAsFirstSibling();
        OrbitLine = transform.Find("Orbit").gameObject;
        OrbitLine.GetComponent<LineRenderer>().positionCount = 2000;
        for (int i = 0; i < 2000; i++)
        {
            
            temp_X -= 1.00f * (transform.childCount-1) * 0.2f * grav_x;
            temp_Y -= 1.00f * (transform.childCount-1) * 0.2f * grav_y;
            OrbitPos += new Vector3(temp_X, temp_Y, 0.0f);

            //Debug.Log(1.00f * (transform.childCount - 1) * 0.2f * grav_y);
            //Debug.Log(velocity_x + ", " + velocity_y);
            //Debug.Log(i + "번쨰 x : " + temp_X + ", y : " + temp_Y);
            Varrays[i] = OrbitPos;
            transform.Find("Orbit").GetComponent<LineRenderer>().SetPosition(i, Varrays[i]);
        }
        
    }

    private void AirFriction()
    {
        if (rotate_velocity > 0.00)
            rotate_velocity -= 0.0005f;
        else if (rotate_velocity < 0.00)
            rotate_velocity += 0.0005f;
        if (rotate_velocity < 0.0005 && rotate_velocity > 0.0)
            rotate_velocity = 0;
        else if (rotate_velocity > -0.0005 && rotate_velocity < 0.0)
            rotate_velocity = 0;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Earth")
        {
            gravity_enable = false;
            velocity_x = 0;
            velocity_y = 0;
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Earth")
        {
            gravity_enable = false;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Earth")
        {
            gravity_enable = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        float tempx, tempy;
        float xdir, ydir;
        if (collision.gameObject.tag == "Gravity")
        {
            float tempratio;
            tempx = transform.position.x - collision.gameObject.transform.position.x;
            tempy = transform.position.y - collision.gameObject.transform.position.y;

            xdir = tempx / System.Math.Abs(tempx);
            ydir = tempy / System.Math.Abs(tempy);
            //Debug.Log(xdir + ",  " + ydir);
            tempx = System.Math.Abs(tempx);
            tempy = System.Math.Abs(tempy);

            if (tempx < tempy)
            {
                tempratio = tempy / tempx;
                grav_x = 1.0f / (1.0f + tempratio);
                grav_y = 1.0f - grav_x;
            }
            else if (tempx > tempy)
            {
                tempratio = tempx / tempy;
                grav_y = 1.0f / (1.0f + tempratio);
                grav_x = 1.0f - grav_y;
            }
            else
            {
                tempratio = tempy / tempx;
                grav_x = 1.0f / (1.0f + tempratio);
                grav_y = 1.0f - grav_x;
            }
            grav_x = grav_x * xdir;
            grav_y = grav_y * ydir;
            gravity_enable = true;
        }
    }

private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gravity")
        {
            Debug.Log("a123sdf");
            gravity_enable = false;
        }
    }
}