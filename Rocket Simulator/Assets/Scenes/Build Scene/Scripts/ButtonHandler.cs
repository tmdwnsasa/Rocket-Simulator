﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
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
        for(int i = 0; i < Rocket.ObjectMax; i++)
        {
            parent.transform.GetChild(i).GetComponent<Rigidbody2D>().simulated = true;
        }
    }
}
