using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainButtonhandler : MonoBehaviour
{
    public GameObject RocketList;

    private int SeletedRocketNum = 0;           //선택된 로켓 번호
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MakeSelRocketNum()
    {
        for (int i = 0; i < ButtonHandler.RocketNum - 1; i++)
        {
            if (RocketList.transform.GetChild(i).GetComponent<RocketMove>().selectedrocketstate == true)
            {
                SeletedRocketNum = i;
                return;
            }
        }
    }

    public void OnClickedDownLeftButton()
    {
        RocketList.transform.GetChild(SeletedRocketNum).GetComponent<RocketMove>().rotate_state = 1;
    }
    public void OnClickedDownRightButton()
    {
        RocketList.transform.GetChild(SeletedRocketNum).GetComponent<RocketMove>().rotate_state = 2;
    }

    public void OnClickedUpLeftButton()
    {
        RocketList.transform.GetChild(SeletedRocketNum).GetComponent<RocketMove>().rotate_state = 0;
    }

    public void OnClickedUpRightButton()
    {
        RocketList.transform.GetChild(SeletedRocketNum).GetComponent<RocketMove>().rotate_state = 0;
    }
    public void OnClickedDownEngineButton()
    {
        if (RocketList.transform.GetChild(SeletedRocketNum).GetComponent<RocketMove>().Engine_start == false)
            RocketList.transform.GetChild(SeletedRocketNum).GetComponent<RocketMove>().Engine_start = true;
        else if (RocketList.transform.GetChild(SeletedRocketNum).GetComponent<RocketMove>().Engine_start == true)
            RocketList.transform.GetChild(SeletedRocketNum).GetComponent<RocketMove>().Engine_start = false;
    }
}