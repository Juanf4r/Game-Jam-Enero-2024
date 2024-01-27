using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;

    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject panelWin;
    [SerializeField] private GameObject panelLose;

    private bool _winGame = false;

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
        StartGame();
    }

    private void StartGame()
    {
        InvokeRepeating(nameof(LoadAd), 4f, 2f);
    }

    private void StopGame()
    {
        CancelInvoke(nameof(LoadAd));

        if(_winGame == true)
        {
            panelWin.SetActive(true);
        }
        else if(_winGame == false)
        {

        }
        
        
    }

    private void LoadAd()
    {

    }

    
}
