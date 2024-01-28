using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersecusionManager : MonoBehaviour
{
    private float _velocidadEnemigo = 5f;
    private Vector2 posicionInicialEnemigo;
    [SerializeField] GameObject panelFelicidades;
    [SerializeField] GameObject panelPerdedor; 
    //[SerializeField] GameObject PanelDialogo;

    private float _tiempoRestante;
    [SerializeField] private float _tiempoLimite;
    private bool _timeTrial;


    void Start()
    {
        posicionInicialEnemigo = transform.position;
        MoverEnemigoAleatorio();
        _tiempoRestante = _tiempoLimite;
        //PanelDialogo.SetActive(true);
        StartCoroutine(Dialogo(2f));
        ResetGame();
    }

    void Update()
    {
        Vector2 posicionRaton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MoverEnemigo(posicionRaton);
        if (_timeTrial == false)
        {
            _tiempoRestante -= Time.deltaTime;

            if (_tiempoRestante <= 0.0f)
            {
                _tiempoRestante = 0.0f;
                _timeTrial = true;
                panelPerdedor.SetActive(true);
            }
        }
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
        //PanelDialogo.SetActive(false);
    }

    public void ResetGame()
    {
        panelFelicidades.SetActive(false);
    }
}