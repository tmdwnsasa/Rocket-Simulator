using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    public int MovingState;                                     //움직일지 정하는 상태
    public int SelectedState;                                   //선택된지 정하는 상태

    public SpriteRenderer spriteRenderer;                       //소스 변경(자신의 SpriteRenderer를 가짐)
    public Sprite ObjectSprite;                                 //기본 스프라이트
    public Sprite sel_ObjectSprite;                             //선택시 스프라이트

    private int m_Xpos;                                         //x좌표(시작8)
    private int m_Ypos;                                         //y좌표(시작1)
    private Vector2 m_First_pos;                                //마우스 클릭시 마우스 첫좌표
    private Vector2 First_pos;                                  //마우스 클릭시 오브젝트 첫좌표

    private float time;                                         //시간

    public Object_type Obj_tag;                                 //오브젝트 테그
    


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
