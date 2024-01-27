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
        feedbackText.text = "Intenta adivinar la palabra";
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
            feedbackText.text = "La palabra debe tener " + contrase�aActual.Length + " letras";
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
            feedbackText.text = "�Felicidades! Has adivinado la contrase�a, eres m�s listo de lo que pareces";
            StartCoroutine(DesactivarPanelDespuesDeEspera(5f, 1));
        }
        else
        {
            int letrasIncorrectas = contrase�aActual.Length - letrasCorrectas;
            feedbackText.text = "Tienes " + letrasCorrectas + " letras correctas y " + letrasIncorrectas + " letras incorrectas";

            maximoIntentos--;

            UpdateIntentosText();

            if (maximoIntentos <= 0)
            {
                feedbackText.text = "�Jajajaja!, la contrase�a era " + contrase�aActual;
                StartCoroutine(DesactivarPanelDespuesDeEspera(5f, 1));
            }
        }
    }

    IEnumerator DesactivarPanelDespuesDeEspera(float tiempoEspera, int Escena)
    {
        yield return new WaitForSeconds(tiempoEspera);
        SceneManager.LoadScene(Escena);
    }
}
