using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployedRockets : MonoBehaviour
{
    public GameObject RocketList;               //로켓 저장소
    public GameObject m_Rocket;                 //새로운 로켓 생성 변수
    private GameObject Buildrocket = null;      //빌드에서 가져온 로켓

    void Awake()
    {
        Buildrocket = GameObject.Find("Rocket");

        if (Buildrocket == null)
            Debug.Log("로켓이 안받아졌어요!");

        for (int objcnt = 1; objcnt < ButtonHandler.RocketNum; ++objcnt)
        {
            GameObject Rocket1 = Instantiate(m_Rocket);
            Rocket1.transform.parent = RocketList.transform;

            for (int i = 0; i < 24; i++)
            {
                for (int j = 0; j < 66; j++)
                {
                    if(Rocket.SendingObjects[i][j] == objcnt)
                    {
                        for(int objcnt2 = 0; objcnt2 < Rocket.ObjectMax; objcnt2++)
                        {
                            //Debug.Log(objcnt2);
                            //Debug.Log("i : " + (i + 8) + ", " + "j : " + (j + 1));
                            //Debug.Log("x : " + (Buildrocket.transform.GetChild(objcnt2).GetComponent<ObjectMove>().GetXpos()) + ", " + "y : " + (Buildrocket.transform.GetChild(objcnt2).GetComponent<ObjectMove>().GetYpos()));

                            if (Buildrocket.transform.GetChild(objcnt2).GetComponent<ObjectMove>().GetXpos() == i + 8 &&
                                Buildrocket.transform.GetChild(objcnt2).GetComponent<ObjectMove>().GetYpos() == j + 1)
                            {   
                                Buildrocket.transform.GetChild(objcnt2).transform.parent = Rocket1.transform;
                                Rocket.ObjectMax--;
                                break;
                            }
                        }
                    }
                }
            }
        }
        for(int i = 0; i < RocketList.transform.childCount; i++)
        {
            if(RocketList.transform.GetChild(i).transform.childCount == 0)
            {
                Destroy(RocketList.transform.GetChild(i).gameObject);
            }
        }


    }

    void Update()
    {
        
    }
}
