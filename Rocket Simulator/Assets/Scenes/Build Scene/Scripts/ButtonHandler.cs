using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ButtonHandler : MonoBehaviour
{
    static public int RocketNum = 1;
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

        List<List<int>> TempList = new List<List<int>>();
        int how = 0;
        for (int i = 0; i < 24; i++)
        {
            TempList.Add(new List<int>());

            for (int j = 0; j < 66; j++)
            {
                TempList[i].Add(0);
            }
        }

        while (true)
        {
            //Debug.Log(++how + "번 돌았따");

            TempList = DeepCopy<List<List<int>>>(Rocket.SendingObjects);
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

                for (int j = 1; j <= LeftRange; j++)
                {
                    if (Rocket.SendingObjects[xpos - j][ypos] != 0)
                    {
                        if (Rocket.SendingObjects[xpos][ypos] >= Rocket.SendingObjects[xpos - j][ypos])
                        {
                            Rocket.SendingObjects[xpos][ypos] = Rocket.SendingObjects[xpos - j][ypos];
                        }
                        else if (Rocket.SendingObjects[xpos][ypos] < Rocket.SendingObjects[xpos - j][ypos] && Rocket.SendingObjects[xpos][ypos] != 0)
                        {
                            Rocket.SendingObjects[xpos - j][ypos] = Rocket.SendingObjects[xpos][ypos];
                        }
                        else if (Rocket.SendingObjects[xpos][ypos] < Rocket.SendingObjects[xpos - j][ypos] && Rocket.SendingObjects[xpos][ypos] == 0)
                        {
                            Rocket.SendingObjects[xpos][ypos] = Rocket.SendingObjects[xpos - j][ypos];
                        }
                        changecheck = true;
                    }
                }
                for (int j = 1; j <= RightRange; j++)
                {
                    if (Rocket.SendingObjects[xpos + j][ypos] != 0)
                    {
                        if (Rocket.SendingObjects[xpos][ypos] >= Rocket.SendingObjects[xpos + j][ypos])
                        {
                            Rocket.SendingObjects[xpos][ypos] = Rocket.SendingObjects[xpos + j][ypos];
                        }
                        else if (Rocket.SendingObjects[xpos][ypos] < Rocket.SendingObjects[xpos + j][ypos] && Rocket.SendingObjects[xpos][ypos] != 0)
                        {
                            Rocket.SendingObjects[xpos + j][ypos] = Rocket.SendingObjects[xpos][ypos];
                        }
                        else if (Rocket.SendingObjects[xpos][ypos] < Rocket.SendingObjects[xpos + j][ypos] && Rocket.SendingObjects[xpos][ypos] == 0)
                        {
                            Rocket.SendingObjects[xpos][ypos] = Rocket.SendingObjects[xpos + j][ypos];
                        }
                        changecheck = true;
                    }
                }
                for (int j = 1; j <= DownRange; j++)
                {
                    if (Rocket.SendingObjects[xpos][ypos - j] != 0)
                    {
                        if (Rocket.SendingObjects[xpos][ypos] >= Rocket.SendingObjects[xpos][ypos - j])
                        {
                            Rocket.SendingObjects[xpos][ypos] = Rocket.SendingObjects[xpos][ypos - j];
                        }
                        else if (Rocket.SendingObjects[xpos][ypos] < Rocket.SendingObjects[xpos][ypos - j] && Rocket.SendingObjects[xpos][ypos] != 0)
                        {
                            Rocket.SendingObjects[xpos][ypos - j] = Rocket.SendingObjects[xpos][ypos];
                        }
                        else if (Rocket.SendingObjects[xpos][ypos] < Rocket.SendingObjects[xpos][ypos - j] && Rocket.SendingObjects[xpos][ypos] == 0)
                        {
                            Rocket.SendingObjects[xpos][ypos] = Rocket.SendingObjects[xpos][ypos - j];
                        }
                        changecheck = true;
                    }
                }
                for (int j = 1; j <= UpperRange; j++)
                {
                    if (Rocket.SendingObjects[xpos][ypos + j] != 0)
                    {
                        if (Rocket.SendingObjects[xpos][ypos] >= Rocket.SendingObjects[xpos][ypos + j])
                        {
                            Rocket.SendingObjects[xpos][ypos] = Rocket.SendingObjects[xpos][ypos + j];
                        }
                        else if (Rocket.SendingObjects[xpos][ypos] < Rocket.SendingObjects[xpos][ypos + j] && Rocket.SendingObjects[xpos][ypos] != 0)
                        {
                            Rocket.SendingObjects[xpos][ypos + j] = Rocket.SendingObjects[xpos][ypos];
                        }
                        else if(Rocket.SendingObjects[xpos][ypos] < Rocket.SendingObjects[xpos][ypos + j] && Rocket.SendingObjects[xpos][ypos] == 0)
                        {
                            Rocket.SendingObjects[xpos][ypos] = Rocket.SendingObjects[xpos][ypos + j];
                        }
                        changecheck = true;
                    }
                }
                if (changecheck == false)
                {
                    Rocket.SendingObjects[xpos][ypos] = RocketNum;
                    ++RocketNum;
                    //Debug.Log(i + "객체의 " + (RocketNum - 1) + "개");
                }
            }
            bool same = true;
            for (int x = 0; x < 24; x++)
            {
                for (int y = 0; y < 66; y++)
                {
                    if(TempList[x][y] != Rocket.SendingObjects[x][y])
                    {
                        same = false;
                    }
                }
            }

            if (same == true)
            {
                break;
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
                    //Debug.Log("x : " + i + ", " + "y : " + j + ", " + "값 : " + Rocket.SendingObjects[i][j]);
                }
            }
        }
    }
    public static T DeepCopy<T>(T obj)
    {
        using (var stream = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
            stream.Position = 0;

            return (T)formatter.Deserialize(stream);
        }
    }
}