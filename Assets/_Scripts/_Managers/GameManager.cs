using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject exitPanel;
    [SerializeField] private GameObject gameLost;
    [SerializeField] private GameObject gameWin;

    private PlayerInput _playerInput;
    private ControllerActions _controllerActions;
    public int _counterGames;
    private bool _stopGame;
    private bool _randomBool = false;

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

        //Input System
        _playerInput = GetComponent<PlayerInput>();
        _controllerActions = new ControllerActions();
        _controllerActions.GameController.Enable();
        _controllerActions.GameController.ExitGame.performed += EscapeButton;

        _counterGames = 0;
        _stopGame = false;
        _randomBool = Random.Range(0, 2) == 0;

        //Active Canvas
        mainCanvas.SetActive(true);
    }

    private void Start()
    {
        LoadData();
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
        StartCoroutine(Won());
    }

    public void LoseGame()
    {
        gameLost.SetActive(true);
        Time.timeScale = 0;
        //Suena musica de Perder
    }

    private IEnumerator Won()
    {
        gameWin.SetActive(true);

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene(0);
    }

    #endregion

    #region Minigames

    public void StartMiniGame()
    {
        _counterGames += _counterGames + 1;
        SaveData(); 

        switch (_counterGames)
        {
            case 1:

                StopTheAds();

                break;

            case 2:

                if (_randomBool == true)
                {
                    GuessThePassword();
                }
                else
                {
                    FollowTheIcon();
                }

                break;

            case 3:

                if (_randomBool == true)
                {
                    ClickTheButton();
                }
                else
                {
                    StopTheAds();
                }

                break;

            case 4:

                if (_randomBool == true)
                {
                    GuessThePassword();
                }
                else
                {
                    FollowTheIcon();
                }

                break;

            case 5:

                if (_randomBool == true)
                {
                    ClickTheButton();
                }
                else
                {
                    StopTheAds();
                }

                break;

            case 6:

                if (_randomBool == true)
                {
                    GuessThePassword();
                }
                else
                {
                    FollowTheIcon();
                }

                break;

            case 7:

                WinGame();

                break;

            default:

                Debug.Log("Error, no tiene el valor correcto");
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

    private void LoadData()
    {
        if (PlayerPrefs.HasKey("counterGames"))
        {
            _counterGames = PlayerPrefs.GetInt("counterGames");
        }
        else
        {
            PlayerPrefs.SetInt("counterGames", 0);
            _counterGames = PlayerPrefs.GetInt("counterGames");
        }

        _counterGames = PlayerPrefs.GetInt("counterGames");
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("counterGames", _counterGames);
    }

    #endregion

}
