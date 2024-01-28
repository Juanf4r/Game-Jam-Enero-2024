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
    private GameManager _gameManager;
    private bool _timeTrial = false;

    public float restantTime;

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
    }

    void Update()
    {
        if (_timeTrial == false)
        {
            restantTime -= Time.deltaTime;

            if(restantTime <= 200f && restantTime >= 140f)
            {
                _gameManager.Background1();
            }
            else if (restantTime <= 140f && restantTime >= 80f)
            {
                _gameManager.Background2();
            }
            else if(restantTime <= 80f && restantTime >= 35f)
            {
                _gameManager.Background3();
            }
            
            if (restantTime <= 0.0f)
            {
                restantTime = 0.0f;
                _timeTrial = true;

                SceneManager.LoadScene(1);
                _gameManager.LoseGame();
            }
        }
    }

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
