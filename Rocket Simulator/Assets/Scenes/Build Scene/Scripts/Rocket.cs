using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Object_type
{ 
    Head01 = 1,
    fuel_tank01 = 2,
    jet_engine01 = 3
}
public class Rocket : MonoBehaviour
{
    static public List<List<Object_type>> ObjectTag = new List<List<Object_type>>();

    static public List<List<int>> SendingObjects = new List<List<int>>();

    static public int Max;

    private Vector2 First_pos;
    private float fuel_amount;
    private float weight;
    private bool drag;
    private bool clicked = false;

    private List<Vector2> Engine = new List<Vector2>();

    void Awake()
    {
        for (int i = 0; i < 24; i++)
        {
            ObjectTag.Add(new List<Object_type>());

            for (int j = 0; j < 65; j++)
            {
                ObjectTag[i].Add(0);
            }
        }

        for (int i = 0; i < 24; i++)
        {
            SendingObjects.Add(new List<int>());

            for (int j = 0; j < 65; j++)
            {
                SendingObjects[i].Add(0);
            }
        }
    }

    void Update()
    {
        CheckClickedOn();
        Checkobject();
        ErrorCheck();
    }

    //클릭하면
    private void CheckClickedOn()
    {
        int temp = 0;
        float time_temp = 0.0f;
        //터치를 하고 있으면
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Max; i++)
            {
                //오브젝트범위에 클릭을 누른 순간 (유지는 배제)
                if (GameFramework.position.x - transform.GetChild(i).transform.position.x < constants.size && GameFramework.position.x - transform.GetChild(i).transform.position.x > -constants.size &&
                  GameFramework.position.y - transform.GetChild(i).transform.position.y < constants.size && GameFramework.position.y - transform.GetChild(i).transform.position.y > -constants.size)
                {
                    if(GameFramework.touchphase == TouchPhase.Began)
                    {
                        First_pos = new Vector2(transform.GetChild(i).transform.position.x, transform.GetChild(i).transform.position.y);
                        transform.GetChild(temp).transform.GetComponent<ObjectMove>().SetMouseFirstVec2(new Vector2(GameFramework.position.x, GameFramework.position.y));
                        Debug.Log(First_pos.x + ", " + First_pos.y);
                        // 오브젝트가 눌린적이 없었으면
                        if (clicked == false)
                            temp = i;

                        clicked = true;
                        if (transform.GetChild(temp).transform.position.z >= transform.GetChild(i).transform.position.z)
                        {
                            temp = i;
                        }
                    }

                    if (GameFramework.touchphase == TouchPhase.Moved || GameFramework.touchphase == TouchPhase.Stationary)
                    {
                        if (First_pos.x - GameFramework.position.x <= -constants.size * 0.75f || First_pos.x - GameFramework.position.x >= constants.size * 0.75f ||
                            First_pos.y - GameFramework.position.y <= -constants.size * 0.75f || First_pos.y - GameFramework.position.y >= constants.size * 0.75f)
                        {
                            Debug.Log((First_pos.x - GameFramework.position.x) + ", " + (First_pos.y - GameFramework.position.y));
                            drag = true;
                        }
                    }

                    if (GameFramework.touchphase == TouchPhase.Ended)
                    {
                        if (drag == false)
                        {
                            if (transform.GetChild(i).GetComponent<ObjectMove>().SelectedState == 0)
                            {
                                transform.GetChild(i).GetComponent<ObjectMove>().SelectedState = 1;
                            }
                            else if (transform.GetChild(i).GetComponent<ObjectMove>().SelectedState == 1)
                            {
                                transform.GetChild(i).GetComponent<ObjectMove>().SelectedState = 0;
                            }
                        }

                        transform.GetChild(i).GetComponent<ObjectMove>().MovingState = 0;
                        drag = false;
                        clicked = false;
                    }
                }
            }

            //안이 클릭되지 않았다면
            if (clicked == false)
            {
                for(int i = 0; i < Max; i++)
                {
                    transform.GetChild(i).GetComponent<ObjectMove>().SelectedState = 0;
                }
            }

            //Debug.Log(temp + ", " + clicked + ", " + drag);
            //오브젝트 선택
            if (temp >= 0 && clicked == true && drag == true)
            {
                transform.GetChild(temp).transform.GetComponent<ObjectMove>().MovingState = 1;
                GameFramework.selectObj = true;
            }
            //Debug.Log(transform.GetChild(temp).transform.GetComponent<ObjectMove>().MovingState);
        }

        //클릭이 끝나면!
        if(GameFramework.touchphase == TouchPhase.Ended)
        {
            for (int i = 0; i < Max; i++)
            {
                transform.GetChild(i).GetComponent<ObjectMove>().MovingState = 0;
            }
            drag = false;
            clicked = false;
        }
    }

    private void Checkobject()
    {
        for (int i = 0; i < 24; i++)
        {
            for (int j = 0; j < 65; j++)
            {
                ObjectTag[i][j] = 0;
            }
        }

        for (int i = 0; i < Max; i++)
        {
            if ((transform.GetChild(i).transform.GetComponent<ObjectMove>().GetXpos() - 8) >= 0 && (transform.GetChild(i).transform.GetComponent<ObjectMove>().GetXpos() - 8) < 24 &&
                (transform.GetChild(i).transform.GetComponent<ObjectMove>().GetYpos() - 1) >= 0 && (transform.GetChild(i).transform.GetComponent<ObjectMove>().GetYpos() - 1) < 65)
            {
                int tempx, tempy;
                tempx = transform.GetChild(i).transform.GetComponent<ObjectMove>().GetXpos() - 8;
                tempy = transform.GetChild(i).transform.GetComponent<ObjectMove>().GetYpos() - 1;
                ObjectTag[tempx][tempy] = transform.GetChild(i).transform.GetComponent<ObjectMove>().GetTag();
                //Debug.Log(transform.GetChild(i).transform.GetComponent<ObjectMove>().Obj_tag);
                //Debug.Log(i + "번째 원소" + tempx + ", " + tempy + "에다가 " + ObjectTag[tempx][tempy] + "저장");
            }
        }
    }

    private void ErrorCheck()
    {
        for (int i = 0; i < 24; i++)
        {
            for (int j = 0; j < 65; j++)
            {
                if (ObjectTag[i][j] != 0)
                {

                   // Debug.Log(ObjectTag[i][j] + "가" + i + ", " + j + "에 들어있다.");
                }
            }
        }
    }

    private void MakeRocketInfo()
    {
        for (int i = 0; i < 24; i++)
        {
            for (int j = 0; j < 65; j++)
            {
                if (ObjectTag[i][j] == Object_type.Head01)
                {
                    SendingObjects[i][j] = 1;

                }
            }
        }
    }
}