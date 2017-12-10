using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Player_Base
{
    bool is_Action_Mode = false;
    public bool is_Second_Stage = false;

    //Mode에 따른 다른 행동 실시
    public enum Action_Mode { NONE, ROPE, CRANE, Door, CLIMB, BOMB, ETC };
    public Action_Mode action_Mode = Action_Mode.NONE;

    //P
    Player_Main m_player_Main;
    ButtonManager m_Btn_Mannager;
    public Time_Label t_Label;

    //Rope_Mode 설정
    bool is_Rope_Mode = false;
    public float rope_Jump_Force;
    public float max_Ropd_Speed;
    public int jump_Power;
    bool roping_Img_Num = true;
    public Sprite[] roping_Sprite;

    //문넘어가기 위한 Transfomr
    Vector3 next_Door;

    //Crane_Mode 설정
    bool is_Crane_Mode = false;
    public float crane_Velocity;
    Transform crane_Object;
    public Sprite crane_Sprite;

    //Climb_Mode 설정
    bool is_Climb_Mode = false;
    public float climb_velocity;
    public Sprite[] climb_Sprite;
    public float max_Climb_Speed;
    public float climb_Jump_Force;
    public bool climb_Img_Num = true;

    //Bomb
    public Transform CPU;

    //툭수행동 관련
    
    [System.NonSerialized] public bool is_Clear = false;
    int clear_Count;

    //Dead 관련
    public float die_Force_X;
    public float die_Force_Y;
    [System.NonSerialized] public bool is_Died = false;
    int die_Count;
    public int over_Die_Count;
    bool is_Zoom = false;
    public SpriteRenderer front_Back;
    Camera m_Camera;
    public GameObject explosion;


    public GameObject BTM_MANAHGER;

    protected override void Start()
    {
        m_Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        m_player_Main = GetComponent<Player_Main>();
        m_Btn_Mannager = BTM_MANAHGER.GetComponent<ButtonManager>();

        base.Start();
    }

    public override void FixedUpdatePlayer()
    {
        
    }

    // Graphic & Input Updates
    protected override void FixedUpdate ()
    {
        if (is_Died)
        {
            if (is_Zoom)
            {
                m_Camera.GetComponent<FollowCamera>().is_MOVE_TOTAL = false;
                m_Camera.orthographicSize *= 0.96f;
                m_Camera.transform.position += dir * (new Vector3(-1.5f, 0.0f, 0)) * Time.deltaTime;

                if (m_Camera.orthographicSize < 0.6f)
                {
                    m_Btn_Mannager.playCanvas.SetActive(false);
                    m_Btn_Mannager.deadCanvas.SetActive(true);
                    is_Zoom = false;

                    //Re_Pannel.SetActive(true);
                }
            }

            return;
        }

        if (!is_Active)
        {
            Debug.Log("NOt you1?");
            if (transform.position.x > 29.6)
            {
                Debug.Log("NOt you?");
                transform.localScale = new Vector3(-0.5f, 0.5f, 1.0f);
                transform.position += new Vector3(-0.5f, 0, 0) * Time.deltaTime;
            }
            else
            {
                transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
            }
            return;
        }


        if (is_Rope_Mode)
        {
            player_Ani.enabled = false;

            if (roping_Img_Num)
            {
                player_Render.sprite = roping_Sprite[0];
            }
            else
            {
                player_Render.sprite = roping_Sprite[1];
            }

      
            float vy = 0;
            //TODO : 로프 애니메이션 진행
            if (m_player_Main.inputLeft)
            {     
                vy = -0.5f;
            }
            else if (m_player_Main.inputRight)
            {
                vy = 0.5f;
            }
      
            /*
            //TODO : 로프 애니메이션 진행
            float vy = Input.GetAxis("Vertical");
            float dir_rope = Mathf.Sign(vy);
            vy = (max_Ropd_Speed > Mathf.Abs(vy)) ? vy : dir_rope * max_Ropd_Speed;
            */
            if(vy != 0)
            {
                roping_Img_Num = !roping_Img_Num;
            }

            player_Rigid.velocity = new Vector2(0, vy);

            return;
        }

        if (is_Climb_Mode)
        {
            player_Ani.enabled = false;

            if (climb_Img_Num)
            {
                player_Render.sprite = climb_Sprite[0];
            }
            else
            {
                player_Render.sprite = climb_Sprite[1];
            }

            if (is_Second_Stage)
            {
                player_Rigid.velocity = new Vector2(0, max_Speed_Y / 2);
            }
            else
            {
                float vy = 0;
                if (m_player_Main.inputRight)
                {
                    if (dir > 0)
                    {
                        vy = 0.5f;
                    }
                    else
                    {
                        vy = -0.5f;
                    }
                }
                else if (m_player_Main.inputLeft)
                {
                    if (dir > 0)
                    {
                        vy = -1;
                    }
                    else
                    {
                        vy = 1;
                    }
                }
                /*
                float vy = Input.GetAxis("Horizontal");
                float dir_climb = Mathf.Sign(vy);
                vy = (max_Climb_Speed > Mathf.Abs(vy)) ? vy : dir_climb * max_Climb_Speed;
                */
                if (vy != 0)
                {
                    climb_Img_Num = !climb_Img_Num;
                }

                player_Rigid.velocity = new Vector2(0, vy);
            }
        }

        if (is_Crane_Mode)
        {
            Debug.Log("UPdeta_crane");
            player_Ani.enabled = false;

            player_Render.sprite = crane_Sprite;

            player_Rigid.velocity = new Vector2(dir*crane_Velocity, 0);

            if (crane_Object == null)
            {
                Debug.Log("OUt_Crnas");
                player_Ani.enabled = true;
                player_Rigid.gravityScale = 0.6f;

                action_Mode = Action_Mode.NONE;
                is_Crane_Mode = false;

                player_Rigid.velocity = new Vector2(0.0f, player_Rigid.velocity.y);   
            }
            return;
        }
        
        base.FixedUpdate();
        
    }

    public override void Jump()
    {
        base.Jump();

        if (is_Rope_Mode)
        {
            player_Ani.enabled = true;

            Debug.Log("Jump_Rope");

            float var = 0;
            if (m_player_Main.inputLeft)
            {
                var = -1;
            }
            else
            {
                var = 1;
            }
            //float var = Input.GetAxis("Horizontal");

            if (is_Second_Stage)
            {
                var = -1;
            }

            if(var!= 0)
            {
                var = Mathf.Sign(var) * jump_Power;
                scale = Mathf.Sign(var) / 2;
                dir = Mathf.Sign(var);
            }
            //Debug.Log(var);
            //Debug.Log("pre : "+player_Rigid.velocity.x + " : " + player_Rigid.velocity.y);
            player_Rigid.AddForce(new Vector2(var, rope_Jump_Force), ForceMode2D.Impulse);
            //Debug.Log("lat : "+player_Rigid.velocity.x +" : " + player_Rigid.velocity.y);
            player_Rigid.gravityScale = 0.6f;

            is_Rope_Mode = false;
        }

        if (is_Climb_Mode)
        {
            is_Climb_Mode = false;
            player_Ani.enabled = true;

            Debug.Log("Climb_Jump");
            float var = 0;
            if (m_player_Main.inputLeft)
            {
                var = -1.0f;
            }
            else if (m_player_Main.inputRight)
            {
                var = 1.0f;
            }

            if (var != 0)
            {
                var = Mathf.Sign(var) * climb_Jump_Force;
                scale = Mathf.Sign(var) / 2;
                dir = Mathf.Sign(var);
            }

            player_Rigid.AddForce(new Vector2(var, climb_Jump_Force), ForceMode2D.Impulse);
            
            player_Rigid.gravityScale = 0.6f;
        }

        if (is_Crane_Mode)
        {
            player_Ani.enabled = true;

            float vy = 0.0f;
            float vx = 0;
            if (dir > 0)
            {
                vx = 3.0f;
            }
            else
            {
                vx = -3.0f;
            }

            player_Rigid.AddForce(new Vector2(vx, vy), ForceMode2D.Impulse);
            player_Rigid.gravityScale = 0.6f;

            is_Crane_Mode = false;
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO : Enermy한테 잡혔을떄 처리
        if(collision.tag == "Enermy")
        {
            Die();
            //Re();
        }

        //레이저 충돌시 반응
        if (collision.tag == "Laser")
        {
            Debug.Log("In_Laser");
            Die();
            //Re();
        }

        if(collision.tag == "Cliff")
        {
            Die();
            //Re();
        }

        if (collision.tag == "CPU")
        {
            Debug.Log("in_CPU");
            CPU = collision.transform.GetChild(0);
            is_Action_Mode = true;
            action_Mode = Action_Mode.BOMB;
        }

        if (collision.tag == "Clear")
        {
            Debug.Log("In_Clear");
            m_Camera.GetComponent<FollowCamera>().is_Clear = true;
            explosion.GetComponent<explo>().is_explo = true;
            explosion.GetComponent<AudioSource>().Play();

            t_Label.is_Clear = true;
            //TODO : 클리어 처리
        }

        if (collision.tag == "Ex_T")
        {
            Debug.Log("In_Ex");
            collision.GetComponent<Ex>().Ex_Pannel.SetActive(true);
            collision.GetComponent<Ex>().is_Ex_Show = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Rope")
        {
            is_Action_Mode = true;
            action_Mode = Action_Mode.ROPE;
        }   

        if(collision.tag == "Laser_Gun")
        {
            Debug.Log("In_Laser_Gun");
            collision.GetComponent<Laser_Gun>().is_Player_Enter = true;
        }

        if(collision.tag == "Door")
        {
            Debug.Log("In_Door");
            action_Mode = Action_Mode.Door;
            next_Door = collision.GetComponent<Door>().next_Door_Position;
        }

        if(collision.tag == "Crane")
        {
            Debug.Log("in_Crane");
            is_Action_Mode = true;
            action_Mode = Action_Mode.CRANE;
            crane_Object = collision.transform;
        }

        if(collision.tag == "Climb")
        {
            Debug.Log("in_Climb");

            if (is_Second_Stage)
            {
                is_Climb_Mode = true;
                return;
            }

            is_Action_Mode = true;
            action_Mode = Action_Mode.CLIMB;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Rope")
        {
            is_Action_Mode = false;
            action_Mode = Action_Mode.NONE;
            is_Rope_Mode = false;
        }

        if (collision.tag == "Laser_Gun")
        {
            collision.GetComponent<Laser_Gun>().is_Player_Enter = false;
        }

        if(collision.tag == "Door")
        {
            next_Door = new Vector3(0,0,0);
            action_Mode = Action_Mode.NONE;
        }

        if (collision.tag == "Crane")
        {
            is_Action_Mode = false;
            is_Crane_Mode = false;
            action_Mode = Action_Mode.NONE;
            crane_Object = null;

            player_Ani.enabled = true;
            player_Rigid.gravityScale = 0.6f;
            player_Rigid.AddForce(new Vector2(1.0f, 0.0f));
        }

        if (collision.tag == "Climb")
        {
            if (is_Climb_Mode)
            {
                player_Rigid.AddForce(new Vector2(6 * dir, 3));
                player_Ani.enabled = true;
                player_Ani.SetBool("doJumping", true);

                is_Climb_Mode = false;
                player_Rigid.gravityScale = 0.6f;
            }

            is_Action_Mode = false;
            action_Mode = Action_Mode.NONE;
        }

        if(collision.tag == "CPU")
        {
            is_Action_Mode = false;
            action_Mode = Action_Mode.NONE;
            CPU = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            Debug.Log("Push_Box");
            player_Ani.SetBool("isPushing", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            Debug.Log("Push_Box");
            player_Ani.SetBool("isPushing", false);
        }
    }


    public void Do_Action()
    {
        Debug.Log("Action_Mode : " + action_Mode);
        if(action_Mode == Action_Mode.Door)
        {
            transform.position = next_Door;
        }

        if (!is_Action_Mode)
        {
            return;
        }

        //다양한 특수 행동들 처리
        switch (action_Mode)
        {
            case Action_Mode.NONE:

                break;
               
            case Action_Mode.ROPE :
                is_Rope_Mode = true;
                
                //Rope 잡으면 반드시 속도 0
                player_Rigid.velocity = new Vector2(0.0f, 0.0f);
                player_Rigid.gravityScale = 0;
                break;

            case Action_Mode.CRANE:
                if (is_Crane_Mode)
                {
                    return;
                }
                Debug.Log("Do_Crane");
                //TODO : 톱니 위치에따라 이동하게끔 처리
                player_Rigid.gravityScale = 0;
                float pos_X = transform.position.x;
                
                transform.position = new Vector3(pos_X, crane_Object.position.y - 0.25f, 0);

                is_Crane_Mode = true;

                break;

            case Action_Mode.CLIMB:
                player_Rigid.gravityScale = 0;
                player_Rigid.velocity = new Vector2(0.0f, 0.0f);

                is_Climb_Mode = true;
                break;

            case Action_Mode.BOMB:
                if(CPU != null)
                {
                    is_Active = false;
                    CPU.parent.GetComponent<Effect_Move>().Do_Effect();
                    action_Mode = Action_Mode.NONE;
                    player_Rigid.velocity = new Vector2(0, 0);
                    transform.GetChild(1).gameObject.SetActive(false);
                    CPU.gameObject.SetActive(true);
               
                }
                
                break;

            case Action_Mode.ETC:

                break;


        }
    }

    public void Die()
    {
        if (!is_Died)
        {
            is_Died = true;
            is_Zoom = true;

            player_Ani.SetBool("isDieing", true);
            player_Rigid.AddForce(new Vector2(-dir * die_Force_X, die_Force_Y));
            player_Render.color = new Color(0, 0, 0);
            //TODO : 새창띄우기
        }

    }

    void SetNextStage()
    {
        if (is_Died)
        {
            die_Count++;

            if (die_Count >= 200)
            {
                Debug.Log("Die And Next Stage");
                //TODO : 화면 변환
            }
        }

        if (is_Clear)
        {
            clear_Count++;

            if (clear_Count >= 200)
            {
                Debug.Log("Clear And Next Stage");
                //TODO : 화면변환
            }
        }
    }

    public void Re()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
