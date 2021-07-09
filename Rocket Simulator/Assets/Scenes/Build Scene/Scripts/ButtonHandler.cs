using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public int RocketNum = 1;
    public GameObject parent;

    public GameObject pref01;
    public GameObject pref02;
    public GameObject pref03;

    void Start()
    {
        
    }

    void Update()
    {

    }
    public void OnClickedButton01()
    {
        GameObject prefab01 = Instantiate(pref01);
        prefab01.transform.parent = parent.transform;
        Rocket.ObjectMax++;
        prefab01.GetComponent<ObjectMove>().Obj_tag = Object_type.Head01;
    }
    public void OnClickedButton02()
    {
        GameObject prefab02 = Instantiate(pref02);
        prefab02.transform.parent = parent.transform;
        Rocket.ObjectMax++;
        prefab02.GetComponent<ObjectMove>().Obj_tag = Object_type.fuel_tank01;
    }
    public void OnClickedButton03()
    {
        GameObject prefab03 = Instantiate(pref03);
        prefab03.transform.parent = parent.transform;
        Rocket.ObjectMax++;
        prefab03.GetComponent<ObjectMove>().Obj_tag = Object_type.jet_engine01;
    }
    public void OnClickedButtonToMain()
    {
        SceneManager.LoadScene("Main Scene");

        MakeRocketNum();
        Errorcheck();

        for (int i = 0; i < Rocket.ObjectMax; i++)
        {
            parent.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            parent.transform.GetComponent<Rigidbody2D>().freezeRotation = true;
            parent.transform.GetComponent<Rigidbody2D>().gravityScale = 9.8f;
        }
    }

    private void MakeRocketNum()
    {
        for (int i = 0; i < Rocket.ObjectMax; i++)
        {
            bool changecheck = false;
            int xpos = parent.transform.GetChild(i).GetComponent<ObjectMove>().GetXpos() - 8;           //자식오브젝트의 배열안의 x값
            int ypos = parent.transform.GetChild(i).GetComponent<ObjectMove>().GetYpos() - 1;           //자식오브젝트의 배열안의 y값

            int UpperRange = 2;
            int DownRange = 2;
            int LeftRange = 2;
            int RightRange = 2;
            
            //범위생성
            if (xpos < 2) { LeftRange = xpos; }
            if (xpos > 21) { RightRange = 23 - xpos; }
            if (ypos < 2) { DownRange = ypos; }
            if (ypos > 63) { UpperRange = 65 - ypos; }

            for (int j = 0; j <= LeftRange; j++)
            {
                if(Rocket.SendingObjects[xpos - j][ypos] != 0)
                {
                    Debug.Log("왼쪽에 0아닌거 있다");
                    Rocket.SendingObjects[xpos][ypos] = Rocket.SendingObjects[xpos - j][ypos];
                    changecheck = true;
                }
            }
            for (int j = 0; j <= RightRange; j++)
            {
                if (Rocket.SendingObjects[xpos + j][ypos] != 0)
                {
                    Debug.Log("오른쪽에 0아닌거 있다");
                    Rocket.SendingObjects[xpos][ypos] = Rocket.SendingObjects[xpos + j][ypos];
                    changecheck = true;
                }
            }
            for (int j = 0; j <= DownRange; j++)
            {
                if (Rocket.SendingObjects[xpos][ypos - j] != 0)
                {
                    Debug.Log("아래에 0아닌거 있다");
                    Rocket.SendingObjects[xpos][ypos] = Rocket.SendingObjects[xpos][ypos - j];
                    changecheck = true;
                }
            }
            for (int j = 0; j <= UpperRange; j++)
            {
                if (Rocket.SendingObjects[xpos][ypos + j] != 0)
                {
                    Debug.Log("위에 0아닌거 있다");
                    Rocket.SendingObjects[xpos][ypos] = Rocket.SendingObjects[xpos][ypos + j];
                    changecheck = true;
                }
            }
            if(changecheck == false)
            {
                Rocket.SendingObjects[xpos][ypos] = RocketNum;
                ++RocketNum;
            }
        }
    }

    private void Errorcheck()
    {
        for(int i = 0; i < 24; ++i)
        {
            for(int  j = 0; j < 66; j++)
            {
                if(Rocket.SendingObjects[i][j] != 0)
                {
                    Debug.Log("x : " + i + ", " + "y : " + j + ", " + "값 : " + Rocket.SendingObjects[i][j]);
                }
            }
        }
    }
}