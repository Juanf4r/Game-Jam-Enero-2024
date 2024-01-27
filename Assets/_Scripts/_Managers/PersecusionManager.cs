using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersecusionManager : MonoBehaviour
{
    private float _velocidadEnemigo = 5f;
    private Vector2 posicionInicialEnemigo;
    [SerializeField] GameObject panelFelicidades;
    [SerializeField] GameObject PanelDialogo;

    void Start()
    {
        posicionInicialEnemigo = transform.position;
        MoverEnemigoAleatorio();
        PanelDialogo.SetActive(true);
        StartCoroutine(Dialogo(2f));
        ResetGame();
    }

    void Update()
    {
        Vector2 posicionRaton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MoverEnemigo(posicionRaton);
    }

    private void OnMouseDown()
    {
        panelFelicidades.SetActive(true);
        StartCoroutine(CambioEscena(2f, 2));
        TimeManager.Instance.WinTime(45f);
    }

    private void MoverEnemigo(Vector2 objetivo)
    {
        transform.position = Vector2.MoveTowards(transform.position, objetivo, -_velocidadEnemigo * Time.deltaTime);
    }

    private void MoverEnemigoAleatorio()
    {
        Vector2 posicionAleatoria = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f));
        transform.position = posicionAleatoria;
    }

    IEnumerator CambioEscena(float Espera, int Escena)
    {
        yield return new WaitForSeconds(Espera);
        panelFelicidades.SetActive(false);
        SceneManager.LoadScene(Escena);
    }

    IEnumerator Dialogo(float tiempoEspera)
    {
        yield return new WaitForSeconds(tiempoEspera);
        PanelDialogo.SetActive(false);
    }

    public void ResetGame()
    {
        panelFelicidades.SetActive(false);
    }
}