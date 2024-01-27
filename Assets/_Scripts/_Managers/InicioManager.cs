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
    [SerializeField] private GameObject leaderBoardObject;
    [SerializeField] private Button leaderBoardButton;

    private PlayerInput _playerInput;
    private ControllerActions _controllerActions;
    private bool _stopGame;
    public bool _hasPlayed = false;

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

        _playerInput = GetComponent<PlayerInput>();
        _controllerActions = new ControllerActions();
        _controllerActions.GameController.Enable();
        _controllerActions.GameController.ExitGame.performed += EscapeButton;

        if(_hasPlayed == true)
        {
            leaderBoardObject.SetActive(true);
            leaderBoardButton.interactable = true;
        }
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
        }
        else
        {
            Load();
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

    public void cambioDeEscena(int Escena)
    {
        SceneManager.LoadScene(Escena);
    }

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
