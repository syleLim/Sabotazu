using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Base : MonoBehaviour {
    /*
     * 플레이어 Base Script
     */

    [System.NonSerialized] public Rigidbody2D player_Rigid;
    [System.NonSerialized] public SpriteRenderer player_Render;
    [System.NonSerialized] public Animator player_Ani;

    //움직임 관련
    [System.NonSerialized] public float dir = 1.0f;
    [System.NonSerialized] public bool is_Active = true;
    public float speed_Value;
    public float speed_X = 0;
    public float speed_Y = 0;
    public Vector2 add_Force_X;
    public Vector2 add_Force_Y;
    public float max_Speed_X;
    public float max_Speed_Y;

    //크기관련
    [System.NonSerialized] public float base_Scale = 0.5f;
    [System.NonSerialized] public float scale = -0.5f;

    //점프, 접지 관련
    [System.NonSerialized] public bool is_Jumped = false;
    [System.NonSerialized] public bool is_Grounded = false;
    [System.NonSerialized] public bool is_Grounded_Prev = false;
    [System.NonSerialized] public GameObject ground_Check_Road;
    [System.NonSerialized] public GameObject ground_Check_Road_Left;
    [System.NonSerialized] public GameObject ground_Check_Road_Right;
    public Transform ground_Check_Center;
    public Transform ground_Check_Left;
    public Transform ground_Check_Right;

    public int jump_count = 0;


    // Use this for initialization
    protected virtual void Start () {
        //초기화
        player_Rigid = GetComponent<Rigidbody2D>();
        player_Render = GetComponent<SpriteRenderer>();
        player_Ani= GetComponent<Animator>();

        dir = (transform.localScale.x > 0.0f) ? 1.0f : -1.0f;
    }
	
    public virtual void FixedUpdatePlayer()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);        
        GroudCheck();

        if (is_Grounded)
        {
            player_Rigid.gravityScale = 0.6f;
        }
   
        player_Rigid.velocity = new Vector2(speed_X, player_Rigid.velocity.y);

        float vx = (Mathf.Abs(player_Rigid.velocity.x) > max_Speed_X) ? dir*max_Speed_X : player_Rigid.velocity.x;
        //float vy = (Mathf.Abs(player_Rigid.velocity.y) > max_Speed_Y) ? max_Speed_Y (player_Rigid.velocity.y; : player_Rigid.velocity.y;
        
        //TODO : 아직 처리 고민중
        player_Rigid.velocity = new Vector2(vx, player_Rigid.velocity.y);
        //Debug.Log("latlat : " + player_Rigid.velocity.x + " : " + player_Rigid.velocity.y);

    }
    
    //움직임 함수 4개
    public void Move_Left()
    {
        if (!is_Grounded)
        {
            return;
        }
        if(tag == "Player")
        {
            player_Ani.SetBool("isRunning", true);
        }
        
        dir = -1.0f;
        scale = -0.5f;
        speed_X += speed_Value * dir;
    }

    public void Move_Right()
    {
        if (!is_Grounded)
        {
            return;
        }

        player_Ani.SetBool("isRunning", true);
        dir = 1.0f;
        scale = 0.5f;
        speed_X += speed_Value * dir;
    }

    public void Standing()
    {
        if (!is_Grounded)
        {
            return;
        }

        player_Ani.SetBool("isRunning", false);
        speed_X = 0;
    }

    public virtual void Jump()
    {
        GetComponent<AudioSource>().Play();

        if (is_Grounded)
        {
            Debug.Log("is_Ground");
            if (!is_Jumped)
            {
                Debug.Log("Normal_jump");
                player_Rigid.gravityScale = 0.6f;
                player_Ani.SetBool("doJumping", true);
                player_Rigid.AddForce(add_Force_Y, ForceMode2D.Impulse); //TODO : ForceMode2D 확인

            }
        }
    }

    //접지 판정
    public void GroudCheck()
    {
        is_Grounded_Prev = is_Grounded;
        is_Grounded = false;

        ground_Check_Road = null;

        Collider2D[][] ground_Check_Collider = new Collider2D[3][];

        ground_Check_Collider[0] = Physics2D.OverlapPointAll(ground_Check_Center.position);
        ground_Check_Collider[1] = Physics2D.OverlapPointAll(ground_Check_Left.position);
        ground_Check_Collider[2] = Physics2D.OverlapPointAll(ground_Check_Right.position);

        foreach (Collider2D ground_check_collider in ground_Check_Collider[0])
        {
            if(ground_check_collider != null)
            {
                if (!ground_check_collider.isTrigger)
                { 
                    //현재 길 바닥만 체크처리
                    if(ground_check_collider.tag == "Road")
                    {
                        is_Grounded = true;
                        ground_Check_Road = ground_check_collider.gameObject;
                    }
                }
            }
        }

        foreach (Collider2D ground_check_collider in ground_Check_Collider[1])
        {
            if (ground_check_collider != null)
            {
                if (!ground_check_collider.isTrigger)
                {
                    //현재 길 바닥만 체크처리
                    if (ground_check_collider.tag == "Road")
                    {
                        is_Grounded = true;
                        ground_Check_Road_Left = ground_check_collider.gameObject;
                    }
                }
            }
        }

        foreach (Collider2D ground_check_collider in ground_Check_Collider[2])
        {
            if (ground_check_collider != null)
            {
                if (!ground_check_collider.isTrigger)
                {
                    //현재 길 바닥만 체크처리
                    if (ground_check_collider.tag == "Road")
                    {
                        is_Grounded = true;
                        ground_Check_Road_Right = ground_check_collider.gameObject;
                    }
                }
            }
        }

    }
}
