using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //카메라 해상도 변경
        transform.position = new Vector3((float)Screen.width / 2.0f, (float)Screen.height / 2.0f, -10.0f);
        GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize = Screen.height / 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
