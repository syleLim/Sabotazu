using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    GameObject Player;
    Player m_Player;

    float laser_Count;
    public int start_Count;
    public int end_Count;
    public int term_Count;
    
    bool is_Laser = true;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        m_Player = Player.GetComponent<Player>();
	}

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (m_Player.is_Second_Stage)
        {
            transform.GetChild(1).gameObject.SetActive(false);

            return;
        }

        laser_Count++;

        if (laser_Count == start_Count)
        {
            transform.GetChild(1).gameObject.SetActive(false);
            is_Laser = false;
        }

        if(laser_Count == end_Count)
        {
            transform.GetChild(1).gameObject.SetActive(true);
            is_Laser = true;
        }

        if (laser_Count == end_Count + term_Count)
        {
            laser_Count = start_Count - 1;
        }
    }
}
