using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LeaderBoardManager : MonoBehaviour
{
    public static LeaderBoardManager Instance;

    [Header("ScriptableObject")]
    [SerializeField] private LeaderBoardStats[] stats = new LeaderBoardStats[5];

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI[] nameStats = new TextMeshProUGUI[5];
    [SerializeField] private TextMeshProUGUI[] timeStats = new TextMeshProUGUI[5];

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

        for(int i  = 0; i < stats.Length - 1; i++)
        {
            int minutes = Mathf.FloorToInt(stats[i].time / 60);
            int seconds = Mathf.FloorToInt(stats[i].time % 60);
            
            timeStats[i].text = string.Format("{0:00}:{1:00}", minutes, seconds);
            nameStats[i].text = stats[i].playerName;
        }
    }

    #region Exit

    public void ExitScene()
    {
        SceneManager.LoadScene(0);
    }

    #endregion
}
