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

    private float fuel_amount;
    private float weight;


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
        bool clicked = false;
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Max; i++)
            {
                //오브젝트범위에 클릭을 누른순간
                if (GameFramework.position.x - transform.GetChild(i).transform.position.x < constants.size && GameFramework.position.x - transform.GetChild(i).transform.position.x > -constants.size &&
                  GameFramework.position.y - transform.GetChild(i).transform.position.y < constants.size && GameFramework.position.y - transform.GetChild(i).transform.position.y > -constants.size &&
                  GameFramework.touchphase == TouchPhase.Began)
                {
                    if(transform.GetChild(i).GetComponent<ObjectMove>().SelectedState == 0)
                    {
                        transform.GetChild(i).GetComponent<ObjectMove>().SelectedState = 1;
                    }
                    else
                    {
                        transform.GetChild(i).GetComponent<ObjectMove>().SelectedState = 0;
                    }
                    // 오브젝트가 눌리지 않았으면
                    if (clicked == false)
                        temp = i;
                    
                    clicked = true; 
                    if (transform.GetChild(temp).transform.position.z >= transform.GetChild(i).transform.position.z)
                    {
                        temp = i;
                    }
                }
            }
            if (clicked == false)
            {
                for(int i = 0; i < Max; i++)
                {
                    transform.GetChild(i).GetComponent<ObjectMove>().SelectedState = 0;
                }
            }

            //오브젝트 선택
            if (temp >= 0)
            {
                transform.GetChild(temp).transform.GetComponent<ObjectMove>().ClickedState = 1;
                GameFramework.selectObj = true;
                transform.GetChild(temp).transform.GetComponent<ObjectMove>().SetMouseFirstVec2(new Vector2(GameFramework.position.x, GameFramework.position.y));
            }
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