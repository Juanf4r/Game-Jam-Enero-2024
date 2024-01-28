using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;
    public TMP_InputField username;
    public string playerName;

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

    public void SaveName()
    {
        string textoInputField = username.text;

        PlayerPrefs.SetString("TextoGuardado", textoInputField);
        PlayerPrefs.Save();
        playerName = textoInputField;

    }
}
