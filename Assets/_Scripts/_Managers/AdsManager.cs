using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class AdsManager : MonoBehaviour
{
    [SerializeField] private RectTransform mainCanvas;
    [SerializeField] private GameObject panelWin;
    [SerializeField] private GameObject panelLose;
    [SerializeField] private GameObject objectPooling;
    [SerializeField] private Time time;
    [SerializeField] private float limitTime;
    private Vector2 _canvasSize;
    private ObjectPool _objectPool;

    public float _restantTime;
    private int _iterator;
    private bool _winGame;
    private bool _timeTrial;


    private void Start()
    {
        _objectPool = ObjectPool.Instance;
        _timeTrial = false;
        _winGame = false;
        _restantTime = limitTime;
        
        StartGame();  
    }

    private void Update()
    {
        if (_timeTrial == false)
        {
            _restantTime -= Time.deltaTime;

            if (_restantTime <= 0.0f)
            {
                _restantTime = 0.0f;
                _timeTrial = true;

                StartCoroutine(StopGame()); 
            }
        }
    }

    private void StartGame()
    {
        StartCoroutine(ChargeAds());

        InvokeRepeating(nameof(LoadAd), 10f, 2.5f);
    }

    private IEnumerator ChargeAds()
    {
        while(true)
        {
            Invoke(nameof(LoadAd), .25f);
            _iterator++;

            yield return new WaitForSeconds(.5f);

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
