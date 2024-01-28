using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VencidasManager : MonoBehaviour
{
    [SerializeField] private Button clickButton;
    [SerializeField] private Slider scoreSlider;
    [SerializeField] private GameObject PanelGanador;
    [SerializeField] private GameObject PanelPerdedor;
    private float _maxScore = 150f;
    private float _scoreDecreaseRate = 25f;
    private float _clickValue = 6f;
    private float currentScore;
    private bool _update = true;

    void Start()
    {
        clickButton.onClick.AddListener(OnClickButton);
        currentScore = 100f;
        ResetGame();
    }

    void Update()
    {

        if(_update == true)
        {
            currentScore -= _scoreDecreaseRate * Time.deltaTime;
            currentScore = Mathf.Clamp(currentScore, 0f, _maxScore);
            scoreSlider.value = currentScore;

            if (currentScore <= 0.01f)
            {
                _update = false;
                PanelPerdedor.SetActive(true);
                TimeManager.Instance.LoseTime(30f);
                StartCoroutine(Cambio(4f, 2));
            }
        }
        
    }

    public void OnClickButton()
    {
        currentScore += _clickValue;
        currentScore = Mathf.Clamp(currentScore, 0f, _maxScore);
        if (currentScore >= _maxScore)
        {
            Debug.Log("¡Ganaste!");
            PanelGanador.SetActive(true);
            StartCoroutine(Cambio(4f, 2));
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
        PanelPerdedor.SetActive(false);
    }
}