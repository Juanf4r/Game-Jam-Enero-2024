using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    //[SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Time time;
    [SerializeField] private float limitTime;
    public float restantTime;

    private GameManager _gameManager;
    private bool _timeTrial = false;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        _gameManager = GameManager.Instance;
        restantTime = limitTime;
        //UpdateTime();
    }

    void Update()
    {
        if (_timeTrial == false)
        {
            restantTime -= Time.deltaTime;

            if(restantTime <= 235f && restantTime >= 140f)
            {
                Debug.Log("Entre");
                GameManager.Instance.Background1();
            }
            else if (restantTime <= 140f && restantTime >= 80f)
            {
                GameManager.Instance.Background2();
            }
            else if(restantTime <= 80f && restantTime >= 35f)
            {
                GameManager.Instance.Background3();
            }
            
            if (restantTime <= 0.0f)
            {
                restantTime = 0.0f;
                _timeTrial = true;

                SceneManager.LoadScene(1);
                _gameManager.LoseGame();
            }
            //UpdateTime();
        }
    }

    /*private void UpdateTime()
    {
        int minutes = Mathf.FloorToInt(_restantTime / 60);
        int seconds = Mathf.FloorToInt(_restantTime % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }*/

    public void WinTime(float winTime)
    {
        restantTime += winTime;
    }

    public void LoseTime(float loseTime)
    {
        restantTime -= loseTime;
    }

    public void DestroyTimeManager()
    {
        Destroy(this.gameObject);
    }
}
