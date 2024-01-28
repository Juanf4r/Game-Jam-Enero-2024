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
    public bool TimeTrial = false;

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
        if (TimeTrial == false)
        {
            restantTime -= Time.deltaTime;
            
            if (restantTime <= 0.0f)
            {
                restantTime = 0.0f;
                TimeTrial = true;

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
