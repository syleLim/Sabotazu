using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class ButtonManager : MonoBehaviour
{
    private bool loadOn = false;
    private bool pauseOn = false;
    private GameObject homeCanvas;
    [System.NonSerialized] public GameObject scoreCanvas;
    private GameObject pauseCanvas;
    [System.NonSerialized] public GameObject deadCanvas;
    [System.NonSerialized] public GameObject playCanvas;
    [System.NonSerialized] public GameObject clearCanvas;

    GameObject player;
    Player_Main playerScript;
    Time_Label t_Label;
    public Text score_Board;
    

    //Ex
    public GameObject[] ex_Pannel;
    bool is_Ex = false;
    int ex_Count = 0;

    void Awake()
    {
        scoreCanvas = GameObject.Find("UI").transform.Find("Score Canvas").gameObject;
        homeCanvas = GameObject.Find("UI").transform.Find("Home Canvas").gameObject;
        pauseCanvas = GameObject.Find("UI").transform.Find("Pause Canvas").gameObject;
        deadCanvas = GameObject.Find("UI").transform.Find("Dead Canvas").gameObject;
        playCanvas = GameObject.Find("UI").transform.Find("Play Canvas").gameObject;
        clearCanvas = GameObject.Find("UI").transform.Find("Clear Canvas").gameObject;
        t_Label = playCanvas.GetComponent<Time_Label>();

        GetComponent<AudioSource>().Play();
    }

    public void Init()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player_Main>();

    }

    private void Update()
    {
        if (is_Ex)
        {
            ex_Count++;
            Debug.Log(ex_Count);
            if (ex_Count > 150)
            {
                ex_Pannel = GameObject.FindGameObjectsWithTag("Ex");
                foreach (GameObject ex in ex_Pannel)
                {
                    ex.SetActive(false);
                }

                is_Ex = false;
            }
        }
    }

    // 플레이 패널
    public void LeftDown()
    {
        playerScript.inputLeft = true;
    }

    public void LeftUp()
    {
        playerScript.inputLeft = false;
    }

    public void RightDown()
    {
        playerScript.inputRight = true;
    }

    public void RightUp()
    {
        playerScript.inputRight = false;
    }

    public void JumpClick()
    {
        playerScript.inputJump = true;
    }

    public void FireClick()
    {
        playerScript.inputFire = true;
    }

    public void Play_Pause()
    {

        t_Label.sw.Stop();
        playCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    // 일시정지 패널
    public void Pause_Home()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Play_Scene");
    }

    public void Pause_Exit()
    {
        t_Label.is_ReStart = true;
        Time.timeScale = 1.0f;
        pauseCanvas.SetActive(false);
        playCanvas.SetActive(true);
    }

    public void Pause_Retry()
    {
        Debug.Log("게임 재시작.");
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Play_Scene");
        homeCanvas.SetActive(false);
        playCanvas.SetActive(true);
    }

    // 데드 패널
    public void Dead_Home()
    {
        SceneManager.LoadScene("Play_Scene");
    }

    public void Dead_Retry()
    {
        Debug.Log("게임 재시작.");
        SceneManager.LoadScene("Play_Scene");
        homeCanvas.SetActive(false);
        playCanvas.SetActive(true);
    }

    // 홈 패널
    public void Main_Start()
    {
        homeCanvas.SetActive(false);
        is_Ex = true;
        playCanvas.SetActive(true);
    }

    public void Main_Score()
    {
        homeCanvas.SetActive(false);
        scoreCanvas.SetActive(true);

        string[] scores = t_Label.Load();

        Debug.Log(scores[2]);
        string score = "Score\n";

        int over = (scores.Length < 9) ? scores.Length : 9;

        for(int i =0; i<over; i++)
        {
            score = score + scores[i] +"\n";
        }

        score_Board.text = score;
        Debug.Log(score + scores[2]);
    }

    public void Main_Exit()
    {
        Debug.Log("게임 종료.");
        Application.Quit();
    }

    // 스코어 패널
    public void Score_Exit()
    {
        scoreCanvas.SetActive(false);
        homeCanvas.SetActive(true);
    }

    // 클리어 패널
    public void Clear_Home()
    {
        SceneManager.LoadScene("Play_Scene");
    }

    public void Clear_Retry()
    {
        Debug.Log("게임 재시작.");
        SceneManager.LoadScene("Play_Scene");
        homeCanvas.SetActive(false);
        playCanvas.SetActive(true);
    }

    public void Return_Mode()
    {

        SceneManager.LoadScene("Sean_to_Re");
    }
}