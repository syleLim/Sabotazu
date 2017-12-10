using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using debug = UnityEngine.Debug;
using System.IO;
using System;

public class Time_Label : MonoBehaviour {

    string store_Path = "Assets/Resource/";

    float Time_State_lo;
    string Time_Score;

    [System.NonSerialized] public bool is_ReStart = false;
    [System.NonSerialized] public bool is_Clear = false;
    bool already_Save = false;

    public Stopwatch sw = new Stopwatch();
    public Text Score_Label;
    public Text score_Board;

    private void OnEnable()
    {
        sw.Start();
        already_Save = false;
    }

    // Update is called once per frame
    void Update () {

        Time_State_lo = sw.ElapsedMilliseconds / 1000.0f;

        Score_Label.text = Time_State_lo.ToString("N1");


        if (is_ReStart)
        {
            sw.Start();
            is_ReStart = false;
        }


        if (is_Clear)
        {
            Time_Score = (sw.ElapsedMilliseconds / 1000.0f).ToString("N1");
            sw.Stop();
            Save(Time_Score);
        }
	}

    public void Save(string save_data)
    {
        if (!already_Save)
        {
            UnityEngine.Debug.Log("Save");
            FileStream f = new FileStream(store_Path + "Data.txt", FileMode.Append, FileAccess.Write);

            StreamWriter wr = new StreamWriter(f, System.Text.Encoding.UTF8);
            wr.WriteLine(save_data);
            wr.Close();

            already_Save = true;
        }
    }


    public string[] Load()
    {
        FileStream f = new FileStream(store_Path + "Data.txt", FileMode.Open);
        
        StreamReader sr = new StreamReader(f);
        
        string score ="";
        string val;
        //UnityEngine.Debug.Log("do");
        while ((val=sr.ReadLine()) != null)
        {
            score += "," + val;
            UnityEngine.Debug.Log("val : "+val);
        }

        sr.Close();

        string[] scores = score.Split(',');
        UnityEngine.Debug.Log("val : " + scores[2]);

        float[] sc_tm = new float[scores.Length];
        for(int i = 1; i<scores.Length; i++)
        {
            sc_tm[i] = System.Convert.ToSingle(scores[i]);
        }

        Array.Sort(sc_tm);

        for (int i = 0; i < scores.Length; i++)
        {
            scores[i] = sc_tm[i].ToString();
        }

        return scores;

        //UnityEngine.Debug.Log("Possible_clos");
    }
}
