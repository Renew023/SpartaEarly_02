using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject square;
    public GameObject EndPanel;
    public Text timeTxt;
    public Text nowScoreTxt;
    public Text BestScore;
    
    public Animator anim;

    bool isPlay = true;

    float time = 0.0f;

    string key = "bestScore";

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        InvokeRepeating("MakeSquare", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {
            time += Time.deltaTime;
            timeTxt.text = time.ToString(".##");
        }
    }

    void MakeSquare()
    {
        Instantiate(square);
        Debug.Log("�����Ѵ�");
    }

    public void GameOver()
    {
        isPlay = false;
        anim.SetBool("IsDie", true);
        Invoke("TimeStop", 2f);
        nowScoreTxt.text = time.ToString(".##");

        // �ְ������� �ִٸ�
        if (PlayerPrefs.HasKey(key))
        {
            //�ְ� ���� < ���� ����
            float best = PlayerPrefs.GetFloat(key);
            if (best < time)
            {
                // ���� ������ �ְ� ������ �����Ѵ�.
                PlayerPrefs.SetFloat(key, time);
                BestScore.text = time.ToString(".##");
            }
            else
            {
                BestScore.text = best.ToString(".##");
            }
        }
        else // �ְ������� ���ٸ�
        {
            // ���� ������ �����Ѵ�.
            PlayerPrefs.SetFloat(key, time);
            BestScore.text = time.ToString(".##");
        }
        

        EndPanel.SetActive(true);
    }

    void TimeStop()
    {
        Time.timeScale = 0.0f;
    }

}
