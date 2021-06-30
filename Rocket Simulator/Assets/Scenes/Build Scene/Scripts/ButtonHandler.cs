using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
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
        Instantiate(pref01, new Vector3(0, 0, 0), Quaternion.identity);
    }
    public void OnClickedButton02()
    {
        Instantiate(pref02, new Vector3(0, 0, 0), Quaternion.identity);
    }
    public void OnClickedButton03()
    {
        Instantiate(pref03, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
