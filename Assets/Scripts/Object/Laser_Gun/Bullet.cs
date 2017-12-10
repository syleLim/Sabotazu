using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [System.NonSerialized] public Vector2 dir;
    public float bullet_Speed;

    //총알 무효화 관련
    int bullet_Count;
    public int bullet_Dead_Count;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        bullet_Count++;

        if(bullet_Count >= bullet_Dead_Count)
        {
            bullet_Count = 0;
            gameObject.SetActive(false);
            return;
        }

        Debug.Log(dir.x +" : " + dir.y );
        transform.position += new Vector3(dir.x, dir.y, 0) * bullet_Speed * Time.deltaTime;
	}
}
