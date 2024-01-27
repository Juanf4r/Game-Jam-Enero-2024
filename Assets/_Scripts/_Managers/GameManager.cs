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

    private PlayerInput _playerInput;
    private ControllerActions _controllerActions;
    private bool _stopGame;

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

        //Active Canvas
        mainCanvas.SetActive(true);
    }

    private void Start()
    {
        _stopGame = false;
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

    public void VirusButton(int scene)
    {
        SceneManager.LoadScene(scene);
    }

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

}
