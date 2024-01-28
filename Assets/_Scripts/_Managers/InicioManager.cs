using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InicioManager : MonoBehaviour
{
    public static InicioManager Instance;

    [SerializeField] private Slider _sonidoSlider;
    [SerializeField] private Slider _musicaSlider;
    [SerializeField] private GameObject exitPanel;
    [SerializeField] private Button leaderBoardButton;

    [SerializeField] private GameObject defaultCanvas;
    [SerializeField] private GameObject newGameCanvas;

    private PlayerInput _playerInput;
    private ControllerActions _controllerActions;
    private bool _stopGame;

    public bool HasPlayed;

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

        _stopGame = false;
        leaderBoardButton.interactable = HasPlayed;
        _playerInput = GetComponent<PlayerInput>();
        _controllerActions = new ControllerActions();
        _controllerActions.GameController.Enable();
        _controllerActions.GameController.ExitGame.performed += EscapeButton;
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("SoundVolume"))
        {
            PlayerPrefs.SetFloat("SoundVolume", 1);
            Load();
        }
        else
        {
            Load();
        }

        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }

        if (HasPlayed == true)
        {
            defaultCanvas.SetActive(false);
            newGameCanvas.SetActive(true);
        }
    }

    #region Input Sytem

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
            if (_stopGame == false)
            {
                _stopGame = true;
                exitPanel.SetActive(true);
            }
            else if (_stopGame == true)
            {
                _stopGame = false;
                exitPanel.SetActive(false);
            }
        }
    }

    public void EscapeButtonResume()
    {
        _stopGame = false;
        exitPanel.SetActive(false);
    }

    #endregion Input System

    #region LoadScene

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    #endregion

    #region Settings

    public void ChangeVolumeS()
    {
        AudioListener.volume = _sonidoSlider.value;
        SaveS();
    }

    public void ChangeVolumeM()
    {
        AudioListener.volume = _musicaSlider.value;
        SaveM();
    }

    #endregion

    #region Exit

    public void Exit()
    {
        Application.Quit();
    }

    #endregion 

    #region SaveData

    private void Load()
    {
        if (PlayerPrefs.HasKey("_hasPlayed"))
        {
            HasPlayed = Convert.ToBoolean(PlayerPrefs.GetInt("_hasPlayed"));
        }
        else
        {
            PlayerPrefs.SetInt("_hasPlayed", 0);
            HasPlayed = Convert.ToBoolean(PlayerPrefs.GetInt("_hasPlayed"));
        }

        _sonidoSlider.value = PlayerPrefs.GetFloat("SoundVolume");
        _musicaSlider.value = PlayerPrefs.GetFloat("MusicVolume");
    }

    private void SaveS()
    {
        PlayerPrefs.SetFloat("SoundVolume", _sonidoSlider.value);
    }

    private void SaveM()
    {
        PlayerPrefs.SetFloat("MusicVolume", _musicaSlider.value);
    }

    #endregion
}
