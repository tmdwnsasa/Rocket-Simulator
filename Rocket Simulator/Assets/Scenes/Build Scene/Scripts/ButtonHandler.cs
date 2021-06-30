using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public GameObject parent;

    public GameObject pref01;
    public GameObject pref02;
    public GameObject pref03;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClickedButton01()
    {
        GameObject prefab01 = Instantiate(pref01);
        prefab01.transform.parent = parent.transform;
        Rocket.Max++;
        prefab01.AddComponent<ObjectMove>().tag = 1;
        //prefab01.GetComponent<ObjectMove>().GetTime
    }
    public void OnClickedButton02()
    {
        GameObject prefab02 = Instantiate(pref02);
        prefab02.transform.parent = parent.transform;
        Rocket.Max++;
        prefab02.AddComponent<ObjectMove>().tag = 2;
    }
    public void OnClickedButton03()
    {
        GameObject prefab03 = Instantiate(pref03);
        prefab03.transform.parent = parent.transform;
        Rocket.Max++;
        prefab03.AddComponent<ObjectMove>().tag = 3;
    }
}
