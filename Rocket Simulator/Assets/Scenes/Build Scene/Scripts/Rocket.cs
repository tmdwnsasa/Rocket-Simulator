using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    static public List<List<int>> ObjectTag = new List<List<int>>();

    static public int Max;

    private List<Vector2> Engine = new List<Vector2>();

    void Awake()
    {
        for(int i = 0; i < 24; i++)
        {
            ObjectTag.Add(new List<int>());

            for(int j = 0; j  < 65; j++)
            {
                ObjectTag[i].Add(0);
            }
        }
    }

    // Update is called once per frame
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
                if (GameFramework.position.x - transform.GetChild(i).transform.position.x < constants.size && GameFramework.position.x - transform.GetChild(i).transform.position.x > -constants.size &&
                  GameFramework.position.y - transform.GetChild(i).transform.position.y < constants.size && GameFramework.position.y - transform.GetChild(i).transform.position.y > -constants.size &&
                  GameFramework.touchphase == TouchPhase.Began && GameFramework.selectObj == false)
                {
                    if (clicked == false)
                        temp = i;
                    clicked = true;
                    if (transform.GetChild(temp).transform.position.z >= transform.GetChild(i).transform.position.z)
                    {
                        temp = i;
                    }
                }
            }

            //오브젝트 선택
            if (temp >= 0 && clicked == true)
            {
                transform.GetChild(temp).transform.GetComponent<ObjectMove>().ClickedOnState = 1;
                GameFramework.selectObj = true;
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
                Debug.Log(i + "번째 원소" + tempx + ", " + tempy + "에다가 " + ObjectTag[tempx][tempy] + "저장");
            }
        }
    }

    private void ErrorCheck()
    {
        for(int i = 0; i < 24; i++)
        {
            for(int j =0; j < 65; j++)
            {
                if(ObjectTag[i][j] != 0)
                {

                    Debug.Log(ObjectTag[i][j] + "가" + i + ", " + j + "에 들어있다.");
                }
            }
        }
    }
}