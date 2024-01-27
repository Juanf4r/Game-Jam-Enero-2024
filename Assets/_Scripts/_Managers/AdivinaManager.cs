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
    private string[] contrase�as = { "unity", "game", "script", "player", "object" };
    private string contrase�aActual;
    private int maximoIntentos = 6;
    [SerializeField] GameObject panelJuego;

    private void Start()
    {
        SeleccionarContrase�aAleatoria();
        ResetGame();
    }

    private void SeleccionarContrase�aAleatoria()
    {
        contrase�aActual = contrase�as[Random.Range(0, contrase�as.Length)].ToLower();
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

        if (guess.Length != contrase�aActual.Length)
        {
            maximoIntentos--;
            UpdateIntentosText();
            feedbackText.text = "The word must have " + contrase�aActual.Length + " letter";
            if (maximoIntentos <= 0)
            {
                feedbackText.text = "Hahahaha, the password was " + contrase�aActual;
                StartCoroutine(DesactivarPanelDespuesDeEspera(5f, 2));
            }
            return;
        }

        int letrasCorrectas = 0;

        for (int i = 0; i < contrase�aActual.Length; i++)
        {
            if (guess[i] == contrase�aActual[i])
            {
                letrasCorrectas++;
            }
        }

        if (letrasCorrectas == contrase�aActual.Length)
        {
            feedbackText.text = "Congratulations! You've guessed the password, you're smarter than you look";
            StartCoroutine(DesactivarPanelDespuesDeEspera(5f, 2));
        }
        else
        {
            int letrasIncorrectas = contrase�aActual.Length - letrasCorrectas;
            feedbackText.text = "You have " + letrasCorrectas + " correct lettering and " + letrasIncorrectas + " Incorrect Letters";

            maximoIntentos--;

            UpdateIntentosText();

            if (maximoIntentos <= 0)
            {
                feedbackText.text = "Hahahaha, the password was " + contrase�aActual;
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
