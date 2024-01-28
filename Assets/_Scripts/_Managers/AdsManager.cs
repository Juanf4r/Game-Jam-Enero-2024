using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;

    [SerializeField] private RectTransform mainCanvas;
    [SerializeField] private GameObject panelWin;
    [SerializeField] private GameObject panelLose;
    [SerializeField] private GameObject objectPooling;
    [SerializeField] private Time time;
    [SerializeField] private float limitTime;
    private Vector2 _canvasSize;
    private ObjectPool _objectPool;
    private int _iterator;
    private bool _winGame;
    private bool _timeTrial;

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
        }
    }

    private void Start()
    {
        _objectPool = ObjectPool.Instance;
        _timeTrial = false;
        _winGame = false;
        restantTime = limitTime; 
    }

    private void Update()
    {
        if (_timeTrial == false)
        {
            restantTime -= Time.deltaTime;

            if (restantTime <= 0.0f)
            {
                restantTime = 0.0f;
                _timeTrial = true;

                StartCoroutine(StopGame()); 
            }
        }
    }

    public void StartGame()
    {
        StartCoroutine(ChargeAds());

        InvokeRepeating(nameof(LoadAd), 10f, 2.5f);
    }

    private IEnumerator ChargeAds()
    {
        while(true)
        {
            Invoke(nameof(LoadAd), .15f);
            _iterator++;

            yield return new WaitForSeconds(.25f);

            if(_iterator >= 15)
            {
                break;
            }
        }   
    }

    public IEnumerator StopGame()
    {
        StopCoroutine(ChargeAds());
        CancelInvoke(nameof(LoadAd));

        if(_winGame == true)
        {
            panelWin.SetActive(true);
            yield return new WaitForSeconds(6f);

            SceneManager.LoadScene(2);
        }
        else if(_winGame == false)
        {
            panelLose.SetActive(true);
            yield return new WaitForSeconds(6f);

            SceneManager.LoadScene(2);
        }   
    }

    public void StopAds()
    {
        _winGame = true;
        StartCoroutine(StopGame());
    }

    private void LoadAd()
    {
        GameObject ad = _objectPool.GetAd();
        ad.SetActive(true);
    }  
}
