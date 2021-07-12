using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployedRockets : MonoBehaviour
{
    public GameObject m_Rocket;
    // Start is called before the first frame update
    void Awake()
    {
        if (m_Rocket == null)
            Debug.Log("로켓이 안받아졌어요!");

        for (int objcnt = 1; objcnt < Rocket.ObjectMax; objcnt++)
        {
            Instantiate(m_Rocket);
            for (int i = 0; i < 24; i++)
            {
                for (int j = 0; j < 66; j++)
                {
                    if(Rocket.SendingObjects[i][j] == objcnt)
                    {
                        for(int objcnt2 =0; objcnt2 < Rocket.ObjectMax; objcnt2++)
                        {
                            if (m_Rocket.transform.GetChild(objcnt2).GetComponent<ObjectMove>().GetXpos() == i + 8 &&
                                m_Rocket.transform.GetChild(objcnt2).GetComponent<ObjectMove>().GetYpos() == j + 1)
                            {
                                m_Rocket.transform.GetChild(objcnt2).transform.parent = m_Rocket.transform;
                            }
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
