using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    public Transform Player;
    [System.NonSerialized] public float prev_Scale;
    [System.NonSerialized] public bool is_Move = false;
    public bool is_MOVE_TOTAL = true;
    [System.NonSerialized] public bool is_Clear= false;

    // Use this for initialization
    void Start () {
        prev_Scale = Player.localScale.x;
	}

    int clear_Count;
    public int over_Clear_Count;
    public GameObject Clear_Pannel;

	// Update is called once per frame
	void Update () {

        if (!is_MOVE_TOTAL)
        {
            return;
        }

        if (is_Clear)
        {

            clear_Count++;

            if(clear_Count> over_Clear_Count)
            {
                Clear_Pannel.SetActive(true);
            }

            return;
        }

        if (Player.localScale.x != prev_Scale)
        {
            prev_Scale = Player.localScale.x;
            is_Move = true;
        }

        if (is_Move)
        {
            if (prev_Scale > 0)
            {
                transform.position = new Vector3(0.1f + transform.position.x, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(-0.1f + transform.position.x, transform.position.y, transform.position.z);
            }

            if (Mathf.Abs(transform.position.x - Player.position.x) > 1.0f)
            {
                is_Move = false;
            }
        }
        else
        {
            if (prev_Scale > 0)
            {
                transform.position = new Vector3(Player.position.x + 1.0f, Player.position.y, -10.0f);
            }
            else
            {
                transform.position = new Vector3(Player.position.x - 1.0f, Player.position.y, -10.0f);
            }
            
        }

        if(transform.position.y != Player.position.y)
        {
            transform.position = new Vector3(transform.position.x, Player.position.y, transform.position.z);
        }
	}
}
