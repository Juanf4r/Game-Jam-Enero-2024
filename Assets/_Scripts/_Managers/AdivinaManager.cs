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

        if (guess.Length != contraseñaActual.Length)
        {
            feedbackText.text = "La palabra debe tener " + contraseñaActual.Length + " letras";
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
            feedbackText.text = "¡Felicidades! Has adivinado la contraseña, eres más listo de lo que pareces";
            StartCoroutine(DesactivarPanelDespuesDeEspera(5f, 1));
        }
        else
        {
            int letrasIncorrectas = contraseñaActual.Length - letrasCorrectas;
            feedbackText.text = "Tienes " + letrasCorrectas + " letras correctas y " + letrasIncorrectas + " letras incorrectas";

            maximoIntentos--;

            UpdateIntentosText();

            if (maximoIntentos <= 0)
            {
                feedbackText.text = "¡Jajajaja!, la contraseña era " + contraseñaActual;
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
