using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crane : MonoBehaviour {

    Animator m_Ani;
    public GameObject Player;
    Player m_Player;

	// Use this for initialization
	void Start () {
        
        m_Player = Player.GetComponent<Player>();
        m_Ani = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (m_Player.is_Second_Stage)
        {
            m_Ani.Play("Crane_Re");
            
            
        }
	}
}
