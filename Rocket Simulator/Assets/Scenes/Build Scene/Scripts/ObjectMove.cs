using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    static public float zMax = 0.0f;
    public int ClickedOnState = 1;
    private int m_Xpos;
    private int m_Ypos;
    private float time;

    public int tag;

    public void SetTime(float time_t) { time = time_t; }
    public float GetTime() { return time; }
    public void SetXpos(int Xpos) { m_Xpos = Xpos; }
    public float GetXpos() { return m_Xpos; }
    public void SetYpos(int Ypos) { m_Ypos = Ypos; }
    public float GetYpos() { return m_Ypos; }

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
        m_Xpos = (int)GameFramework.position.x / (int)constants.size;
        m_Ypos = (int)GameFramework.position.y / (int)constants.size;

        CheckClickedOn();
        if (ClickedOnState == 1)
        {
            transform.position = new Vector3(m_Xpos * (int)constants.size, m_Ypos* (int)constants.size, zMax - 0.00001f);
            Debug.Log(m_Xpos + ", " + m_Ypos);
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
