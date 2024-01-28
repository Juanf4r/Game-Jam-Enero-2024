using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;
    public TMP_InputField username;
    public string playerName;
    public UIManager uiManager;

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
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("playerName"))
        {
            playerName = PlayerPrefs.GetString("playerName");
        }
        else
        {
            PlayerPrefs.SetString("playerName", playerName);
            playerName = PlayerPrefs.GetString("playerName");
        }
    }

    public void SaveName(string user = null)
    {
        string textoInputField = (user != null) ? user : username.text;
        playerName = textoInputField;
        PlayerPrefs.SetString("playerName", playerName);
    }
}
