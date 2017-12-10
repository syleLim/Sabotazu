using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {

    public GameObject Connected_Gate;
    public GameObject Open_Image;

	public void Gate_Open()
    {
        Connected_Gate.SetActive(false);
        Open_Image.SetActive(true);
        Open_Image.GetComponent<AudioSource>().Play();

        gameObject.SetActive(false);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        //Gate_Port 박스 처리
        if (collision.tag == "Box")
        {
            Gate_Open();       
        }
    }
}
