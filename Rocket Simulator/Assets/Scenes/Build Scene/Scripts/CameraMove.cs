using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMove : MonoBehaviour
{
    public GameObject RocketList;
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
        if (SceneManager.GetActiveScene().name == "Build Scene")
        {
            transform.position = new Vector3((float)Screen.width / 2.0f, (float)Screen.height / 2.0f, -10.0f);
            GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize = Screen.height / 2;
        }
        if (SceneManager.GetActiveScene().name == "Main Scene")
        {
            if(RocketList.transform.childCount == 0)
            {
                transform.position = new Vector3((float)Screen.width / 2.0f, (float)Screen.height / 2.0f, -10.0f);
                GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize = Screen.height / 2;
            }
            for (int i = 0; i < RocketList.transform.childCount; i++)
            {
                if (RocketList.transform.GetChild(i).GetComponent<RocketMove>().selectedrocketstate == true)
                {
                    transform.position = new Vector3(RocketList.transform.GetChild(i).GetComponent<RocketMove>().transform.position.x, RocketList.transform.GetChild(i).GetComponent<RocketMove>().transform.position.y, -10.0f);
                }
            }
        }
    }
}
