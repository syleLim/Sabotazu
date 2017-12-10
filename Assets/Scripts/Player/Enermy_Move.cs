using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy_Move : Player_Base {

    enum Enermy_Mode { Follow, UP, UP_END, NO_GRAVITY ,DOOR};
    Enermy_Mode enermy_Mode = Enermy_Mode.Follow;

    public float up_Speed;
    public float up_Jump;
    public float up_End_X;
    public float no_Gravity_Speed;

    Vector3 next_Door_position;
    bool is_Door_Go = false;
    public Transform Player;
    bool is_Stop = false;

    [System.NonSerialized] public bool is_Appear = false;

	// Use this for initialization
	protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void FixedUpdate()
    {
        if (is_Stop)
        {
            return;
        }
        //TODO : Player 근처에서 잡기

        Debug.Log("Enermy_Mode"+enermy_Mode);

        if(enermy_Mode != Enermy_Mode.UP || enermy_Mode != Enermy_Mode.UP_END)
        {
            if (Mathf.Abs(transform.position.x - Player.transform.position.x) < 0.4)
            {
                Debug.Log("Close");
                Vector3 dir = Player.transform.position - transform.position;

                transform.position += Vector3.Normalize(dir) * Time.deltaTime;

                return;
            }
        }

        switch (enermy_Mode)
        {
            case Enermy_Mode.Follow:
                Debug.Log("Enermy_move_Fol");
                player_Rigid.gravityScale = 0.6f;
                base.FixedUpdate();
                break;

            case Enermy_Mode.UP :
                player_Rigid.gravityScale = 0;
                player_Rigid.velocity = new Vector2(0, up_Speed);
                break;

            case Enermy_Mode.UP_END :
                player_Rigid.AddForce(new Vector2(dir * up_End_X, up_Jump));
                enermy_Mode = Enermy_Mode.Follow;
                break;

            case Enermy_Mode.NO_GRAVITY:
                player_Rigid.gravityScale = 0;
                player_Rigid.velocity = new Vector2(dir * no_Gravity_Speed, 0);
                break;

            case Enermy_Mode.DOOR:
                if (!is_Door_Go)
                {
                    transform.position = next_Door_position;
                    is_Door_Go = true;
                    break;
                }
                base.FixedUpdate();
                break;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "UP":
                enermy_Mode = Enermy_Mode.UP;
                break;

            case "UP_End":
                enermy_Mode = Enermy_Mode.UP_END;
                break;

            case "No_Gravity":
                enermy_Mode = Enermy_Mode.NO_GRAVITY;
                break;

            case "Follow":
                enermy_Mode = Enermy_Mode.Follow;
                break;

            case "Door":
                if (!is_Door_Go)
                {
                    next_Door_position = collision.gameObject.GetComponent<Door>().next_Door_Position;
                    enermy_Mode = Enermy_Mode.DOOR;
                    break;
                }
                break;
            case "Clear":
                is_Stop = true;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Door_Out")
        {
            enermy_Mode = Enermy_Mode.Follow;
            is_Door_Go = false;
        }
    }
}
