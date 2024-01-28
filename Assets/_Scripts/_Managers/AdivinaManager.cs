using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdivinaManager : MonoBehaviour
{
    public TMP_Text feedbackText;
    public TMP_InputField inputField;
    public TMP_Text MostrarIntentosT;
    private string[] _contrase�as = { "unity", "game", "script", "player", "object" };
    private string _contrase�aActual;
    private int _maximoIntentos = 6;
    [SerializeField] GameObject panelJuego;
    [SerializeField] GameObject PanelPerdedor;
    [SerializeField] GameObject PanelGanador;
    

    private void Start()
    {
        SeleccionarContrase�aAleatoria();
        ResetGame();
    }

    private void SeleccionarContrase�aAleatoria()
    {
        _contrase�aActual = _contrase�as[Random.Range(0, _contrase�as.Length)].ToLower();
    }

    private void ResetGame()
    {
        feedbackText.text = "Try to guess the password";
        inputField.text = "";
        _maximoIntentos = 6;
        UpdateIntentosText();
    }

    private void UpdateIntentosText()
    {
        MostrarIntentosT.text = "Remaining Attempts: " + _maximoIntentos;
    }

    public void CheckGuess()
    {
        string guess = inputField.text.ToLower();

        if (guess.Length != _contrase�aActual.Length)
        {
            _maximoIntentos--;
            UpdateIntentosText();
            feedbackText.text = "The word must have " + _contrase�aActual.Length + " letter";
            if (_maximoIntentos <= 0)
            {
                feedbackText.text = "Hahahaha, the password was " + _contrase�aActual;
                StartCoroutine(Perder(5f));
                StartCoroutine(DesactivarPanelDespuesDeEspera(10f, 2));
                TimeManager.Instance.LoseTime(30f);
            }
            return;
        }

        int letrasCorrectas = 0;

        for (int i = 0; i < _contrase�aActual.Length; i++)
        {
            if (guess[i] == _contrase�aActual[i])
            {
                letrasCorrectas++;
            }
        }

        if (letrasCorrectas == _contrase�aActual.Length)
        {
            feedbackText.text = "Congratulations! You've guessed the password, you're smarter than you look";
            StartCoroutine(ganar(5f));
            StartCoroutine(DesactivarPanelDespuesDeEspera(10f, 2));
            TimeManager.Instance.WinTime(20f);
        }
        else
        {
            int letrasIncorrectas = _contrase�aActual.Length - letrasCorrectas;
            feedbackText.text = "You have " + letrasCorrectas + " correct lettering and " + letrasIncorrectas + " Incorrect Letters";

            _maximoIntentos--;

            UpdateIntentosText();

            if (_maximoIntentos <= 0)
            {
                feedbackText.text = "Hahahaha, the password was " + _contrase�aActual;
                StartCoroutine(Perder(4f));
                TimeManager.Instance.WinTime(-25f);
                StartCoroutine(DesactivarPanelDespuesDeEspera(6f, 2));
            }
        }
    }

    IEnumerator DesactivarPanelDespuesDeEspera(float tiempoEspera, int Escena)
    {
        yield return new WaitForSeconds(tiempoEspera);
        SceneManager.LoadScene(Escena);
    }

    IEnumerator Perder(float tiempoEspera)
    {
        yield return new WaitForSeconds(tiempoEspera);
        PanelPerdedor.SetActive(true);
        panelJuego.SetActive(false);
    }

    IEnumerator ganar(float tiempoEspera)
    {
        yield return new WaitForSeconds(tiempoEspera);
        PanelGanador.SetActive(true);
        panelJuego.SetActive(false);
    }
}