using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class constants
{
    public const float size = 100;
}

public class ObjectMove : MonoBehaviour
{
    private int ClickedOnState = 1;
    private int m_Xpos;
    private int m_Ypos;

    public int tag;

    public void SetXpos(int Xpos) { m_Xpos = Xpos; }
    public float GetXpos() { return m_Xpos; }
    public void SetYpos(int Ypos) { m_Ypos = Ypos; }
    public float GetYpos() { return m_Ypos; }

    // Start is called before the first frame update
    void Awake()
    {
        float temp = (float)Screen.width / (float)Screen.height;
        transform.position = new Vector3(GameFramework.position.x + Screen.width / 2, GameFramework.position.y + Screen.height / 2, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        m_Xpos = (int)GameFramework.position.x / 50;
        m_Ypos = (int)GameFramework.position.y / 50;

        CheckClickedOn();
        if (ClickedOnState == 1)
        {
            transform.position = new Vector3(m_Xpos * 50, m_Ypos*50, 0.0f);
            Debug.Log(m_Xpos + ", " + GameFramework.position.x);
        }
    }

    private void CheckClickedOn()
    {
        if(Input.touchCount > 0)
        {
            if (GameFramework.position.x - transform.position.x < constants.size && GameFramework.position.x - transform.position.x > -constants.size &&
                GameFramework.position.y - transform.position.y < constants.size && GameFramework.position.y - transform.position.y > -constants.size &&
                GameFramework.touchphase == TouchPhase.Began && GameFramework.selectObj == false)
            {
                ClickedOnState = 1;
                GameFramework.selectObj = true;
            }
        }

        if(Input.touchCount <= 0)
        {
            ClickedOnState = 0;
            GameFramework.selectObj = false;
        }
    }
}
