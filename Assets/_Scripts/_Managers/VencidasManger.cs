using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VencidasManager : MonoBehaviour
{
    public Button clickButton;
    public Slider scoreSlider;
    public GameObject PanelGanador;
    public GameObject PanelPerdedor;
    private float _maxScore = 150f;
    private float _scoreDecreaseRate = 40f;
    private float _clickValue = 6f;

    private float currentScore;

    void Start()
    {
        clickButton.onClick.AddListener(OnClickButton);
        currentScore = 75f;
        ResetGame();
    }

    void Update()
    {
        currentScore -= _scoreDecreaseRate * Time.deltaTime;
        currentScore = Mathf.Clamp(currentScore, 0f, _maxScore);
        scoreSlider.value = currentScore;

        if (currentScore <= 0)
        {
            Debug.Log("Perdiste");
            PanelPerdedor.SetActive(true);
            TimeManager.Instance.LoseTime(30f);
            StartCoroutine(Cambio(5f, 2));
        }
    }

    void OnClickButton()
    {
        currentScore += _clickValue;
        currentScore = Mathf.Clamp(currentScore, 0f, _maxScore);
        if (currentScore >= _maxScore)
        {
            Debug.Log("¡Ganaste!");
            PanelGanador.SetActive(true);
            StartCoroutine(Cambio(5f, 2));
            TimeManager.Instance.WinTime(45f);
        }
    }
    IEnumerator Cambio(float tiempoEspera, int Escena)
    {
        yield return new WaitForSeconds(tiempoEspera);
        SceneManager.LoadScene(Escena);
    }

    public void ResetGame()
    {
        PanelGanador.SetActive(false);
        PanelPerdedor.SetActive(true);
    }
}