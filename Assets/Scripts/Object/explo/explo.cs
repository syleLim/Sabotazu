using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explo : MonoBehaviour {

    int ex_Count;
    public int over_ex_Count;
    int i = 0;
    [System.NonSerialized]public bool is_explo = false;
	// Use this for initialization
	void Start () {
        i = 0;
    }

    // Update is called once per frame
    void Update () {


        if (!is_explo)
        {
            return;
        }


        if (i > 8)
        {
            return;
        }

        ex_Count++;

        if (ex_Count > over_ex_Count)
        {
            transform.GetChild(i).gameObject.SetActive(true);

            i++;

            ex_Count = 0;
        }
	}
}
