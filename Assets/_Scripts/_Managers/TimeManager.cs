using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Time time;
    [SerializeField] private float limitTime;
    
    private bool _timeTrial = false;
    private float _restantTime;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        _restantTime = limitTime;
        UpdateTime();
    }

    void Update()
    {
        if (_timeTrial == false)
        {
            _restantTime -= Time.deltaTime;

            if (_restantTime <= 0.0f)
            {
                _restantTime = 0.0f;
                _timeTrial = true;
                //You Lose!
            }
            UpdateTime();
        }
    }

    private void UpdateTime()
    {
        int minutes = Mathf.FloorToInt(_restantTime / 60);
        int seconds = Mathf.FloorToInt(_restantTime % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
