using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    static public float zMax = 0.0f;
    public int ClickedOnState = 1;
    public int m_Xpos;
    public int m_Ypos;
    private float time;
     
    public int Obj_tag;

    public void SetTag(int tag1) { Obj_tag = tag1; }
    public int GetTag() { return Obj_tag; }
    public void SetTime(float time_t) { time = time_t; }
    public float GetTime() { return time; }
    public void SetXpos(int Xpos) { m_Xpos = Xpos; }
    public int GetXpos() { return m_Xpos; }
    public void SetYpos(int Ypos) { m_Ypos = Ypos; }
    public int GetYpos() { return m_Ypos; }

    // Start is called before the first frame update
    void Awake()
    {
        float temp = (float)Screen.width / (float)Screen.height;
        transform.position = new Vector3(GameFramework.position.x + Screen.width / 2, GameFramework.position.y + Screen.height / 2, 0.0f);
        time = GameFramework.time;
    }

    // Update is called once per frame
    void Update()
    {
        CheckClickedOn();
        if (ClickedOnState == 1)
        {
            m_Xpos = (int)GameFramework.position.x / (int)constants.size;
            m_Ypos = (int)GameFramework.position.y / (int)constants.size;
            transform.position = new Vector3(m_Xpos * (int)constants.size, m_Ypos* (int)constants.size, zMax - 0.00001f);
            Debug.Log((m_Xpos - 8) + ", " + (m_Ypos - 1));
        }
    }

    private void CheckClickedOn()
    {

        if(Input.touchCount <= 0)
        {
            ClickedOnState = 0;
            GameFramework.selectObj = false;
            time = GameFramework.time;
            zMax -= 0.00001f;
        }
    }
}
