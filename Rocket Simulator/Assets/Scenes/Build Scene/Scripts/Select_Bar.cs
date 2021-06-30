using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Bar : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        float temp = (float)Screen.width / (float)Screen.height;
        //transform.position = new Vector3(150, Screen.height / 2, 0.0f);
        //Debug.Log(transform.position.x + ", " + transform.position.y + ", " + transform.position.z);
        //Debug.Log(transform..x + ", " + transform.localPosition.y + ", " + transform.localPosition.z);

        Debug.Log(temp);

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    
}