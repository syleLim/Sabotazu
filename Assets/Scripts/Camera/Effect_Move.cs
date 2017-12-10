using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Move : MonoBehaviour {

    bool is_Camera_Move = false;
    bool is_Camera_Zoom = true;
    bool is_Alpha_Change = false;
    bool is_Alpha_Up = true;
    int zoom_Count = 0;
    public int over_Count;
    float pre_Camera_Size;

    int dark_Count = 0;
    public int over_dark_Count;

    public GameObject Player;
    public GameObject[] Enermy;
    public Camera m_Camera;
    int enermy_Appear_Count = 0;
    int enermy_Num = -1;

    public SpriteRenderer front_Back;
    public GameObject clear_Collider;

    Player m_Player;
    Enermy_Move[] m_Enermy;

	// Use this for initialization
	void Start () {
        m_Player = Player.GetComponent<Player>();

        pre_Camera_Size = m_Camera.orthographicSize;

        zoom_Count = 0;
        dark_Count = 0;
	}
	
	// Update is called once per frame
	public void Do_Effect ()
    {
        clear_Collider.SetActive(true);

        m_Player.is_Active = false;

        m_Enermy = new Enermy_Move[Enermy.Length];

        for (int i = 0; i<Enermy.Length; i++)
        {
            m_Enermy[i] = Enermy[i].GetComponent<Enermy_Move>();
        }

        foreach (Enermy_Move en in m_Enermy)
        {
            en.is_Appear = true;
            en.is_Active = false;
        }

        is_Camera_Move = true;
	}

    private void FixedUpdate()
    {
      
        if (is_Camera_Move)
        {
            if (is_Camera_Zoom)
            {
                m_Camera.orthographicSize *= 0.9f;
                if(m_Camera.orthographicSize < 1.0f)
                {
                    is_Camera_Zoom = false;
                }
            }

            zoom_Count++;

            if (enermy_Num < Enermy.Length - 1)
            {
                enermy_Appear_Count++;

                if (enermy_Appear_Count > 40)
                {
                    enermy_Num++;

                    Enermy[enermy_Num].SetActive(true);
                    enermy_Appear_Count = 0;
                }
            }

            if (zoom_Count > over_Count)
            {
                is_Alpha_Change = true;
                is_Camera_Move = false;
                zoom_Count = 0;
            }
        }

        if (is_Alpha_Change)
        {
            if (is_Alpha_Up)
            {
                if (front_Back.color.a < 0.9f)
                {
                    Color col = front_Back.color;
                    col.a += 0.01f;
                    front_Back.color = col;
                    Debug.Log("In_Alpha");
                }
                else
                {
                    is_Alpha_Up = false;
                }
            }
            else         
            {
                dark_Count++;

                m_Camera.orthographicSize = pre_Camera_Size;
                Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                Player.transform.position = new Vector3(29.6f, -1.829f, 0);

                if (dark_Count > over_dark_Count)
                {
                    Debug.Log("In_Dark");
                    Color col = front_Back.color;
                    col.a -= 0.01f;
                    front_Back.color = col;

                    if (col.a <= 0.03f)
                    {
                        front_Back.gameObject.SetActive(false);

                        m_Player.is_Active = true;
                        m_Player.is_Second_Stage = true;

                        m_Camera.GetComponent<AudioSource>().Play();
                        

                        foreach (Enermy_Move en in m_Enermy)
                        {
                            Debug.Log("Move_EN_Change");
                            en.is_Active = true;
                            en.is_Appear = false;
                        }

                        is_Alpha_Change = false;
                    }
                }
            }
        }
    }
}
