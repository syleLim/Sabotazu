using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Main : MonoBehaviour {

    Player m_Player;
    public GameObject warming;
    int warming_Count = 0;
    public int warming_Term;
    bool is_Warming = false;

    //움직임 버튼 관련
    public bool inputLeft = false;
    public bool inputRight = false;
    public bool inputJump = false;
    public bool inputFire = false;

    // Use this for initialization
    void Start () {
        m_Player = GetComponent<Player>();

        ButtonManager ui = GameObject.FindGameObjectWithTag("Manager").GetComponent<ButtonManager>();
        ui.Init ();
	}
	
	// Update is called once per frame
	void Update () {
        if (!m_Player.is_Active)
        {
            return;
        }

        if (m_Player.is_Died)
        {
            return;
        }
        /*
        if (!m_Player.is_Second_Stage)
        {
            float mv = Input.GetAxis("Horizontal");

            if (mv == 0)
            {
                //Debug.Log("Stand");
                m_Player.Standing();
            }
            else if (mv > 0)
            {
                Debug.Log("Move_Right");
                m_Player.Move_Right();
            }
            else
            {
                Debug.Log("Move_Left");
                m_Player.Move_Left();
            }

            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("Jump");
                m_Player.Jump();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                Debug.Log("Event");
                m_Player.Do_Action();
            }
        }
        else
        {
            m_Player.Move_Left();

            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("Second_Jump");
                m_Player.Jump();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                Debug.Log("Second_Event");
                m_Player.Do_Action();
            }

            warming_Count++;

            if (warming_Count > warming_Term)
            {
                if (is_Warming)
                {
                    warming.SetActive(false);
                    is_Warming = false;
                    warming_Count = 0;
                }
                else
                {
                    warming.SetActive(true);
                    is_Warming = true;
                    warming_Count = 0;
                }
            }
        }
        */
        
        if (!m_Player.is_Second_Stage)
        {

            if (!inputLeft && !inputRight)
            {
                //Debug.Log("Stand");
                m_Player.Standing();

            }
            else if (inputRight)
            {
                Debug.Log("Move_Right");
                m_Player.Move_Right();
            }
            else if (inputLeft)
            {                 

                Debug.Log("Move_Left");
                m_Player.Move_Left();
            }

            if (inputJump)
            {
                Debug.Log("Jump");
                inputJump = false;
                m_Player.Jump();
            }

            if (inputFire)
            {
                Debug.Log("Event");
                inputFire = false;
                m_Player.Do_Action();
            }
        }
        else
        {
            m_Player.Move_Left();

            if (inputJump)
            {
                Debug.Log("Second_Jump");
                inputJump = false;
                m_Player.Jump();
            }

            if (inputFire)
            {
                Debug.Log("Second_Event");
                inputFire = false;
                m_Player.Do_Action();
            }

            warming_Count++;

            if(warming_Count > warming_Term)
            {
                if (is_Warming)
                {
                    warming.SetActive(false);
                    is_Warming = false;
                    warming_Count = 0;
                }
                else
                {
                    warming.SetActive(true);
                    is_Warming = true;
                    warming_Count = 0;
                }
            }

        }
        
	}
}
