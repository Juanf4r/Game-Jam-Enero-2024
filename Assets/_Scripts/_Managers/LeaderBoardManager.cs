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

    #region SaveSO

    public void SaveData(float timeLeft, string userName)
    {
        for(int i = 0; i < stats.Length; i++)
        {
            if (stats[i].time >= timeLeft)
            {
                //En caso de que haya un valor menor que el que se consiguio, este se recorre abajo
                stats[i + 1].time = stats[i].time;
                stats[i + 1].playerName = stats[i].playerName;

                //Se reemplaza el valor viejo con el nuevo
                stats[i].time = timeLeft;
                stats[i].playerName = userName;

                stats[5].time = 0;
                stats[5].playerName = "";
            }
            else if (stats[i].time <= timeLeft)
            {
                //Tu progreso fue muy bajo, sigue jugando
            }
        }
    }

    #endregion

    #region Exit

    public void ExitScene()
    {
        SceneManager.LoadScene(0);
    }

    #endregion
}
