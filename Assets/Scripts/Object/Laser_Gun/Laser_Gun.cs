using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Gun : MonoBehaviour {
    //작동관련
    public bool is_Player_Enter = false;
    public Transform player;


    //이미지 관련
    [System.NonSerialized] public SpriteRenderer laser_Gun_Rednder;
    public Sprite[] laser_Gun_Sprite;
    public Sprite[] bullet_Sprite;

    //총알발사관련
    [System.NonSerialized] int bullet_Img_Num;
    [System.NonSerialized] Vector2 dir;
    [System.NonSerialized] int bullet_Count;
    public GameObject[] Bulletes;
    public int bullet_Fire_Count;


    // Use this for initialization
    void Start () {
        laser_Gun_Rednder = GetComponent<SpriteRenderer>();
        bullet_Img_Num = 0;
        bullet_Count = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!is_Player_Enter)
        {
            bullet_Img_Num = 0;
            bullet_Count = 0;
            return;
        }


        Transform loaction = player.transform;

        //TODO : 좌표계산 명확화 및 각계산 명확화
        Vector2 lo = loaction.position - transform.position;
        float degree = Vector2.Angle(Vector2.left, lo);
        dir = lo.normalized;

        if (degree > 0 && degree < 30)
        {
            laser_Gun_Rednder.sprite = laser_Gun_Sprite[0];
            bullet_Img_Num = 0;
        }
        else if (degree < 70)
        {
            laser_Gun_Rednder.sprite = laser_Gun_Sprite[1];
            bullet_Img_Num = 1;
        }
        else if (degree < 110)
        {
            laser_Gun_Rednder.sprite = laser_Gun_Sprite[2];
            bullet_Img_Num = 2;
        }
        else if (degree < 150)
        {
            laser_Gun_Rednder.sprite = laser_Gun_Sprite[3];
            bullet_Img_Num = 3;
        }
        else if (degree < 180)
        {
            laser_Gun_Rednder.sprite = laser_Gun_Sprite[4];
            bullet_Img_Num = 4;
        }
        else
        {
            laser_Gun_Rednder.sprite = laser_Gun_Sprite[1];
            bullet_Img_Num = 1;
        }

        //총알 관련
        bullet_Count++;

        if (bullet_Count >= bullet_Fire_Count)
        {
            bullet_Count = 0;

            foreach (GameObject bullet in Bulletes)
            {
                if (!bullet.activeSelf)
                {
                    //총알 위치 초기화 및 방향, 이미지 셋팅
                    bullet.transform.position = transform.position;
                    Bullet bull_script = bullet.GetComponent<Bullet>();

                    bullet.GetComponent<SpriteRenderer>().sprite = bullet_Sprite[bullet_Img_Num];
                    bull_script.dir = dir;
                    bullet.SetActive(true);
                    break;
                }
            }
        }

    }

}
