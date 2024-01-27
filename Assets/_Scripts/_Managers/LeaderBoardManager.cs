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

        for(int i  = 0; i < stats.Length; i++)
        {
            timeStats[i].text = stats[i].time.ToString("");
            nameStats[i].text = stats[i].playerName;
        }
    }

    #region Exit

    public void ExitScene()
    {
        SceneManager.LoadScene("Pantalla_Inicio");
    }

    #endregion
}
