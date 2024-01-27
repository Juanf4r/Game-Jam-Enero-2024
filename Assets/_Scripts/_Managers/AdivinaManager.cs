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

        if (guess.Length != contraseñaActual.Length)
        {
            maximoIntentos--;
            UpdateIntentosText();
            feedbackText.text = "The word you are looking for must have " + contraseñaActual.Length + " letters, you fool!";
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
            feedbackText.text = "¡Congrats! You've guess the word..., with pure luck!";
            StartCoroutine(DesactivarPanelDespuesDeEspera(3f, 2));  
        }
        else
        {
            int letrasIncorrectas = contraseñaActual.Length - letrasCorrectas;
            feedbackText.text = "You've got " + letrasCorrectas + " correct letters & " + letrasIncorrectas + " incorrect letters";

            maximoIntentos--;

            UpdateIntentosText();

            if (maximoIntentos <= 0)
            {
                feedbackText.text = "¡HAHAHAHAHA!, The PASSWORD Was " + contraseñaActual;
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
