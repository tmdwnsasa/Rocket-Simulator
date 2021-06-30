using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    List<List<int>> Tag = new List<List<int>>();

    static public int Max;

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckClickedOn();
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
                //Debug.Log(GameFramework.position.x + ", " + GameFramework.position.y + "  !!!" + constants.size);
                //Debug.Log(i + ", " + transform.GetChild(i).transform.position.x + ", " + transform.GetChild(i).transform.position.y + ", " + transform.GetChild(i).transform.position.z);
                //Debug.Log((GameFramework.position.x - transform.GetChild(i).transform.position.x) + ", " + (GameFramework.position.y - transform.GetChild(i).transform.position.y));
                //범위안에, 클릭했을 때, 선택된 오브젝트가 없을때
                if (GameFramework.position.x - transform.GetChild(i).transform.position.x < constants.size && GameFramework.position.x - transform.GetChild(i).transform.position.x > -constants.size &&
                  GameFramework.position.y - transform.GetChild(i).transform.position.y < constants.size && GameFramework.position.y - transform.GetChild(i).transform.position.y > -constants.size &&
                  GameFramework.touchphase == TouchPhase.Began && GameFramework.selectObj == false)
                {
                    if (clicked == false)
                        temp = i;
                        clicked = true;
                    //Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!");
                    if (transform.GetChild(temp).transform.position.z >= transform.GetChild(i).transform.position.z)
                    {
                        temp = i;
                    }
                }
                //Debug.Log(i + ", " + transform.GetChild(i).transform.position.z);
            }
            if (temp >= 0 && clicked == true)
            {
                transform.GetChild(temp).transform.GetComponent<ObjectMove>().ClickedOnState = 1;
                GameFramework.selectObj = true;
            }

        }
    }

    //private void 
}