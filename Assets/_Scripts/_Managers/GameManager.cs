using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private LeaderBoardStats[] stats = new LeaderBoardStats[6];

    [Header("GameObjects")]
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject exitPanel;
    [SerializeField] private GameObject gameLost;
    [SerializeField] private GameObject gameWin;
    [SerializeField] private GameObject antiVirus;
    [SerializeField] private GameObject antiVirus2;

    [Header("Background")]
    [SerializeField] private Image background;
    [SerializeField] private Sprite[] backgroundImages = new Sprite[7];

    private InicioManager _inicioManager;
    private PlayerInput _playerInput;
    private ControllerActions _controllerActions;
    private string _playerUser;
    private float _timeLeft;
    private bool _stopGame;
    private bool _randomBool = false;
    private bool _gameCatch = false;

    public int counterGames;

    private void Awake()
    {
        //SingleTon
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        _inicioManager = InicioManager.Instance;

        //Input System
        _playerInput = GetComponent<PlayerInput>();
        _controllerActions = new ControllerActions();
        _controllerActions.GameController.Enable();
        _controllerActions.GameController.ExitGame.performed += EscapeButton;

        gameLost.SetActive(false);
        gameWin.SetActive(false);
        counterGames = 0;
        _stopGame = false;
        _randomBool = UnityEngine.Random.Range(0, 2) == 0;

        //Active Canvas
        mainCanvas.SetActive(true);
    }

    private void Start()
    {
        LoadData();

        if(_gameCatch == false)
        {
            background.sprite = backgroundImages[counterGames];
        }
        else if(_gameCatch == true)
        {
            //No cambia
        }
    }

    #region Input System

    private void OnEnable()
    {
        _controllerActions.GameController.Enable();
        _controllerActions.GameController.ExitGame.Enable();
    }

    private void OnDisable()
    {
        _controllerActions.GameController.Disable();
        _controllerActions.GameController.ExitGame.Disable();
    }

    public void EscapeButton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(_stopGame == false)
            {
                _stopGame = true;
                exitPanel.SetActive(true);
                Time.timeScale = 0;
            }
            else if(_stopGame == true)
            {
                _stopGame = false;
                exitPanel.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    #endregion

    #region Buttons

    public void EscapeButtonResume()
    {
        _stopGame = false;
        exitPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void EscapeButtonSucess(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void OpenWindows()
    {
        //open Window 
    }

    public void CloseWindows()
    {
        //close Window
    }

    public void openTimer()
    {

    }

    public void closeTimer()
    {

    }

    #endregion

    #region Win or Lose

    public void WinGame()
    {
        WIN();
        //StartCoroutine(Won());
    }

    public void LoseGame()
    {
        SceneManager.LoadScene(2);
        gameLost.SetActive(true);
        Time.timeScale = 0;
    }

    public void GoBackToTitleScreen()
    {
        counterGames = 0;
        Prueba.Instancia.contador = 0;
        StatsManager.Instance.playerName = "";
        //Suena musica de Perder
        SceneManager.LoadScene(0);
    }

    private IEnumerator Won()
    {
        counterGames = 0;
        Prueba.Instancia.contador = 0;

        _timeLeft = TimeManager.Instance.restantTime;
        _playerUser = StatsManager.Instance.playerName;
        TimeManager.Instance.TimeTrial = true;

        SaveData(_timeLeft, _playerUser);   

        InicioManager.Instance.HasPlayed = true;
        StatsManager.Instance.playerName = "";

        SaveData();

        //Suena musica de Ganar
        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene(0);
    }

    public void WIN()
    {
        gameWin.SetActive(true);
    }

    #endregion

    #region Minigames

    public void StartMiniGame()
    {
        counterGames += 1;
        SaveData();
        Prueba.Instancia.SaveData();

        switch (counterGames)
        {
            case 1:

                StopTheAds();

                break;

            case 2:

                _gameCatch = true;
                FollowTheIcon();

                break;

            case 3:

                 ClickTheButton();

                break;

            case 4:

                if (_randomBool == true)
                {
                    GuessThePassword();
                }
                else
                {
                    StopTheAds();
                }

                break;

            case 5:

                if (_randomBool == true)
                {
                    ClickTheButton();
                }
                else
                {
                    _gameCatch = true;
                    FollowTheIcon();
                }

                break;

            case 6:

                GuessThePassword();

                break;

            case 7:

                WinGame();

                break;

            default:

                Debug.Log("Error en la matrix");
                break;
        }
    }

    private void GuessThePassword()
    {
        SceneManager.LoadScene(3);
    }

    private void StopTheAds()
    {
        SceneManager.LoadScene(6);
    }

    private void FollowTheIcon()
    {
        SceneManager.LoadScene(4);
    }

    private void ClickTheButton()
    {
        SceneManager.LoadScene(5);
    }

    #endregion

    #region Save Data

    public void SaveData(float timeLeft, string userName)
    {
        for (int i = 0; i < stats.Length; i++)
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

    private void LoadData()
    {

        if (PlayerPrefs.HasKey("counterGames"))
        {
            counterGames = PlayerPrefs.GetInt("counterGames");
        }
        else
        {
            PlayerPrefs.SetInt("counterGames", 0);
            counterGames = PlayerPrefs.GetInt("counterGames");
        }
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("counterGames", counterGames);
        PlayerPrefs.SetInt("_hasPlayed", Convert.ToInt32(_inicioManager.HasPlayed));
    }

    #endregion
}
