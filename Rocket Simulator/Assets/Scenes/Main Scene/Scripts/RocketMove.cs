using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMove : MonoBehaviour
{
    public bool selectedrocketstate = true;     //선택된 로켓인지 확인
    public int rotate_state;                    //회전중인지 확인
    public bool Engine_start;                   //엔진 시동 확인
    public bool gravity_enable;                 //중력 작용 확인
    public bool CheckAirFriction;               //공기저항 확인

    private int engine_cnt;                     //작동중인 엔진 겟수 확인
    public float speed;                        //속도 계수
    public float fuel_usage = 0.0f;             //사용한 연료
    public float fuel_full;                     //최대 연료

    public float rotate_velocity;               //도는 속도
    public float Zrotate;                       //Z회전 속도
    public float velocity_x;                    //X축 이동속도
    public float velocity_y;                    //Y축 이동속도
    private float grav_x, grav_y;               //중력 XY 속도


    private float line_x, line_y;



    void Awake()
    {
        speed = 0.02f;
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


        transform.position = new Vector3(transform.position.x + velocity_x, transform.position.y + velocity_y, 0.0f);

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
            velocity_x += (float)engine_cnt * speed * -(float)System.Math.Sin(System.Math.PI * transform.eulerAngles.z / 180.0);
            velocity_y += (float)engine_cnt * speed * (float)System.Math.Cos(System.Math.PI * transform.eulerAngles.z / 180.0);
            Debug.Log("vel" + velocity_y);
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

    private void Gravity()          //무게적용
    {
        velocity_x -= speed * transform.childCount * 0.2f * grav_x;
        velocity_y -= speed * transform.childCount * 0.2f * grav_y;
    }

    private void DrawLine()
    {
        Vector3[] Varrays;
        Vector3 OrbitPos = transform.position;

        float vel_temp_X = velocity_x;
        float vel_temp_Y = velocity_y;

        GameObject OrbitLine;
        
        if(Engine_start == true)
        {
            vel_temp_X += (float)engine_cnt * speed * -(float)System.Math.Sin(System.Math.PI * transform.eulerAngles.z / 180.0);
            vel_temp_Y += (float)engine_cnt * speed * (float)System.Math.Cos(System.Math.PI * transform.eulerAngles.z / 180.0);

            Debug.Log("tempx " + line_x + "    " + engine_cnt);
            Debug.Log("tempy " + line_y + "    " + engine_cnt);
        }

        Varrays = new Vector3[2000];
        transform.Find("Orbit").position = transform.position;
        transform.Find("Orbit").transform.SetAsFirstSibling();
        OrbitLine = transform.Find("Orbit").gameObject;
        OrbitLine.GetComponent<LineRenderer>().positionCount = 2000;

        for (int i = 0; i < 2000; i++)
        {
            if(gravity_enable == true)
            {
                vel_temp_X -= 1.00f * (transform.childCount - 1) * 0.2f * grav_x;
                vel_temp_Y -= 1.00f * (transform.childCount - 1) * 0.2f * grav_y;
            }

            OrbitPos = new Vector3(vel_temp_X, vel_temp_Y, 0.0f);

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
            if (Mathf.Abs(velocity_x) + Mathf.Abs(velocity_y)  > 5.0)
            {
                Destroy(transform.gameObject);
                Debug.Log("DEL");
            }
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