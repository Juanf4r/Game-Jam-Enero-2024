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
        feedbackText.text = "Well well, look at us here, out of your own computer, HAHAHAHA, good luck typing your NEW PASSWORD HAHAHAHAHAHA";
        inputField.text = "";
        maximoIntentos = 6;
        UpdateIntentosText();
    }

    private void UpdateIntentosText()
    {
        MostrarIntentosT.text = "Intentos restantes: " + maximoIntentos;
    }

    public void CheckGuess()
    {
        string guess = inputField.text.ToLower();

        if (guess.Length != contrase�aActual.Length)
        {
            maximoIntentos--;
            UpdateIntentosText();
            feedbackText.text = "The word you are looking for must have " + contrase�aActual.Length + " letters, you fool!";
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
            feedbackText.text = "�Congrats! You've guess the word..., with pure luck!";
            StartCoroutine(DesactivarPanelDespuesDeEspera(3f, 2));  
        }
        else
        {
            int letrasIncorrectas = contrase�aActual.Length - letrasCorrectas;
            feedbackText.text = "You've got " + letrasCorrectas + " correct letters & " + letrasIncorrectas + " incorrect letters";

            maximoIntentos--;

            UpdateIntentosText();

            if (maximoIntentos <= 0)
            {
                feedbackText.text = "�HAHAHAHAHA!, The PASSWORD Was " + contrase�aActual;
                StartCoroutine(DesactivarPanelDespuesDeEspera(3f, 2));
            }
        }
    }

    IEnumerator DesactivarPanelDespuesDeEspera(float tiempoEspera, int Escena)
    {
        yield return new WaitForSeconds(tiempoEspera);
        SceneManager.LoadScene(Escena);
    }
}
