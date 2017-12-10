using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex : MonoBehaviour {

    public GameObject Ex_Pannel;
    public bool is_Ex_Show = false;
    int ex_Count;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (is_Ex_Show)
        {
            ex_Count++;

            if (ex_Count > 200)
            {
                Ex_Pannel.SetActive(false);
                is_Ex_Show = false;
            }
        }
            
	}
}
