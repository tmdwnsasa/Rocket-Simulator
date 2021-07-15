using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class constants
{
    public const float size = 40.0f;
}

public class GameFramework : MonoBehaviour
{
    public GameObject GF;

    public static float                 time;                       //시간
    public static Vector3               position;                   //마우스 위치
    public static TouchPhase            touchphase;                 //터치의 상태

    private float                       width;
    private float                       height;

    void Awake()
    {
        DontDestroyOnLoad(GF);

        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;

        position = new Vector3(0.0f, 0.0f, 0.0f);
        
    }

    void Update()
    {
        time += Time.deltaTime;

        if (Input.touchCount > 0)
        {
            float temp = (float)Screen.width / (float)Screen.height * 4.2f;
            transform.position = new Vector3(GetTouchEvent().x, GetTouchEvent().y, 0.0f);
        }
    }

    Vector2 GetTouchEvent()
    {
        Vector2 pos = new Vector2(0.0f, 0.0f);

        Touch touch = Input.GetTouch(0);

        pos = touch.position;
        position = new Vector3(pos.x, pos.y, 0.0f);
        touchphase = touch.phase;

        //터치 후 드래그
        if (touch.phase == TouchPhase.Moved)
        {
            pos = touch.position;
            position = new Vector3(pos.x, pos.y, 0.0f);
        }

        //2손가락 터치
        if (Input.touchCount == 2)
        {
            touch = Input.GetTouch(1);

            if (touch.phase == TouchPhase.Began)
            {
                // Halve the size of the cube.
                transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                // Restore the regular size of the cube.
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }

        return pos;
    }
}
