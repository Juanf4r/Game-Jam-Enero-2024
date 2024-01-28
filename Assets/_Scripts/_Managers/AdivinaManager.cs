using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdivinaManager : MonoBehaviour
{
    public TMP_Text feedbackText;
    public TMP_InputField inputField;
    public TMP_Text MostrarIntentosT;
    private string[] _contraseñas = { "unity", "game", "script", "player", "object" };
    private string _contraseñaActual;
    private int _maximoIntentos = 6;
    [SerializeField] GameObject panelJuego;
    [SerializeField] GameObject PanelPerdedor;
    [SerializeField] GameObject PanelGanador;
    

    private void Start()
    {
        SeleccionarContraseñaAleatoria();
        ResetGame();
    }

    private void SeleccionarContraseñaAleatoria()
    {
        _contraseñaActual = _contraseñas[Random.Range(0, _contraseñas.Length)].ToLower();
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

        if (guess.Length != _contraseñaActual.Length)
        {
            _maximoIntentos--;
            UpdateIntentosText();
            feedbackText.text = "The word must have " + _contraseñaActual.Length + " letter";
            if (_maximoIntentos <= 0)
            {
                feedbackText.text = "Hahahaha, the password was " + _contraseñaActual;
                StartCoroutine(Perder(5f));
                StartCoroutine(DesactivarPanelDespuesDeEspera(10f, 2));
                TimeManager.Instance.LoseTime(30f);
            }
            return;
        }

        int letrasCorrectas = 0;

        for (int i = 0; i < _contraseñaActual.Length; i++)
        {
            if (guess[i] == _contraseñaActual[i])
            {
                letrasCorrectas++;
            }
        }

        if (letrasCorrectas == _contraseñaActual.Length)
        {
            feedbackText.text = "Congratulations! You've guessed the password, you're smarter than you look";
            StartCoroutine(ganar(5f));
            StartCoroutine(DesactivarPanelDespuesDeEspera(10f, 2));
            TimeManager.Instance.WinTime(20f);
        }
        else
        {
            int letrasIncorrectas = _contraseñaActual.Length - letrasCorrectas;
            feedbackText.text = "You have " + letrasCorrectas + " correct lettering and " + letrasIncorrectas + " Incorrect Letters";

            _maximoIntentos--;

            UpdateIntentosText();

            if (_maximoIntentos <= 0)
            {
                feedbackText.text = "Hahahaha, the password was " + _contraseñaActual;
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