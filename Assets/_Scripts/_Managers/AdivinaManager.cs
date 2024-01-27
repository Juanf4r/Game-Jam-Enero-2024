using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdivinaManager : MonoBehaviour
{
    public TMP_Text feedbackText;
    public TMP_InputField inputField;
    public TMP_Text MostrarIntentosT;
    private string[] contraseñas = { "unity", "game", "script", "player", "object" };
    private string contraseñaActual;
    private int maximoIntentos = 6;
    [SerializeField] GameObject panelJuego;

    private void Start()
    {
        SeleccionarContraseñaAleatoria();
        ResetGame();
    }

    private void SeleccionarContraseñaAleatoria()
    {
        contraseñaActual = contraseñas[Random.Range(0, contraseñas.Length)].ToLower();
    }

    private void ResetGame()
    {
        feedbackText.text = "Try to guess the password";
        inputField.text = "";
        maximoIntentos = 6;
        UpdateIntentosText();
    }

    private void UpdateIntentosText()
    {
        MostrarIntentosT.text = "Remaining Attempts: " + maximoIntentos;
    }

    public void CheckGuess()
    {
        string guess = inputField.text.ToLower();

        if (guess.Length != contraseñaActual.Length)
        {
            maximoIntentos--;
            UpdateIntentosText();
            feedbackText.text = "The word must have " + contraseñaActual.Length + " letter";
            if (maximoIntentos <= 0)
            {
                feedbackText.text = "Hahahaha, the password was " + contraseñaActual;
                StartCoroutine(DesactivarPanelDespuesDeEspera(5f, 2));
            }
            return;
        }

        int letrasCorrectas = 0;

        for (int i = 0; i < contraseñaActual.Length; i++)
        {
            if (guess[i] == contraseñaActual[i])
            {
                letrasCorrectas++;
            }
        }

        if (letrasCorrectas == contraseñaActual.Length)
        {
            feedbackText.text = "Congratulations! You've guessed the password, you're smarter than you look";
            StartCoroutine(DesactivarPanelDespuesDeEspera(5f, 2));
        }
        else
        {
            int letrasIncorrectas = contraseñaActual.Length - letrasCorrectas;
            feedbackText.text = "You have " + letrasCorrectas + " correct lettering and " + letrasIncorrectas + " Incorrect Letters";

            maximoIntentos--;

            UpdateIntentosText();

            if (maximoIntentos <= 0)
            {
                feedbackText.text = "Hahahaha, the password was " + contraseñaActual;
                StartCoroutine(DesactivarPanelDespuesDeEspera(5f, 2));
            }
        }
    }

    IEnumerator DesactivarPanelDespuesDeEspera(float tiempoEspera, int Escena)
    {
        yield return new WaitForSeconds(tiempoEspera);
        SceneManager.LoadScene(Escena);
    }
}
