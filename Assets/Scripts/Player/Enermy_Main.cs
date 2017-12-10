using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy_Main : MonoBehaviour {

    Enermy_Move m_Enermy;

	// Use this for initialization
	void Start () {
        m_Enermy = GetComponent<Enermy_Move>();
	}
	
	// Update is called once per frame
	void Update () {
        if (m_Enermy.is_Active)
        {
            m_Enermy.Move_Left();
        }
        Debug.Log("Main_DO? en");
	}
}
