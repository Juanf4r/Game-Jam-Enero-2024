using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderBoardManager : MonoBehaviour
{
    public static LeaderBoardManager Instance;

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

    #region Exit

    public void ExitScene()
    {
        SceneManager.LoadScene("Pantalla_Inicio");
    }

    #endregion
}
