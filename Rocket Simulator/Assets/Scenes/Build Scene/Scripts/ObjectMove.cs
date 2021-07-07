using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    public int MovingState;                                     //움직일지 정하는 상태
    public int SelectedState;                                   //선택된지 정하는 상태
    public SpriteRenderer spriteRenderer;
    public Sprite ObjectSprite;
    public Sprite sel_ObjectSprite;
    private int m_Xpos;
    private int m_Ypos;
    private Vector2 m_First_pos;
    private Vector2 First_pos;
    private float time;

     
    public Object_type Obj_tag;

    public void SetTag(Object_type tag1) { Obj_tag = tag1; }
    public Object_type GetTag() { return Obj_tag; }
    public void SetTime(float time_t) { time = time_t; }
    public float GetTime() { return time; }
    public void SetXpos(int Xpos) { m_Xpos = Xpos; }
    public int GetXpos() { return m_Xpos; }
    public void SetYpos(int Ypos) { m_Ypos = Ypos; }
    public int GetYpos() { return m_Ypos; }
    public void SetMouseFirstVec2(Vector2 F_pos) { m_First_pos = F_pos; }
    public Vector2 GetMouseFirstVec2() { return m_First_pos; }

    void Awake()
    {
        spriteRenderer.sprite = sel_ObjectSprite;
        MovingState = 1;
        SelectedState = 1;
        float temp = (float)Screen.width / (float)Screen.height;
        transform.position = new Vector3(GameFramework.position.x + Screen.width / 2, GameFramework.position.y + Screen.height / 2, 0.0f);
        time = GameFramework.time;
    }


    void Update()
    {
        CheckClickedOn();
        CheckMovingOn();
        ChangeSprite();
    }

    private void CheckClickedOn()
    {
        if(Input.touchCount <= 0)
        {
            time = GameFramework.time;
        }
    }
    private void CheckMovingOn()
    {
        if (MovingState == 0)
        {
            First_pos = new Vector2(transform.position.x, transform.position.y);
        }
        if (MovingState == 1)
        {
            //Debug.Log(First_pos.x  + ",  "+ First_pos.y);.
            
            m_Xpos = (int)((First_pos.x + (GameFramework.position.x - m_First_pos.x)) / constants.size);
            m_Ypos = (int)((First_pos.y + (GameFramework.position.y - m_First_pos.y)) / constants.size);

            //Debug.Log((First_pos.x + (GameFramework.position.x - m_First_pos.x)) + ",!, " + (First_pos.y + (GameFramework.position.y - m_First_pos.y)));
            Rocket.zMax -= 0.00001f;
            transform.position = new Vector3(m_Xpos * constants.size, m_Ypos * constants.size, Rocket.zMax);
            //transform.SetAsLastSibling();
        }
    }

    private void ChangeSprite()
    {
        if(SelectedState == 1)
        {
            spriteRenderer.sprite = sel_ObjectSprite;
        }
        else if (SelectedState == 0)
        {
            spriteRenderer.sprite = ObjectSprite;
        }
    }
}
