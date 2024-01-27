using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PersecusionManager : MonoBehaviour
{
    [Header("MiniJuego")]
    public float velocidadEnemigo = 5f;
    private Vector2 posicionInicialEnemigo;
    [SerializeField] GameObject panelFelicidades;
    [SerializeField] GameObject PanelInicio;

    [Header("Cinematica")]
    public Transform[] ruta;
    [SerializeField] private int indice = 0;
    public Vector3 direccion;
    [SerializeField] private float velocidad = 2;

    void Start()
    {
        //Cinematica
        transform.position = ruta[0].position;
        //Cuando inicia el minijuego
        StartCoroutine(Iniciador(10f));
    }

    void Update()
    {
        //Cinematica
        direccion = (ruta[indice].position - transform.position).normalized;
        transform.Translate(direccion * velocidad * Time.deltaTime);
        if (Vector3.Distance(transform.position, ruta[indice].position) < .2f && indice < ruta.Length - 1)
        {
            indice++;
        }
        StartCoroutine(Huir(10f));

    }

    private void OnMouseDown()
    {
        Debug.Log("Has tocado al enemigo");
        Time.timeScale = 0f;
        panelFelicidades.SetActive(true);
    }

    private void MoverEnemigo(Vector2 objetivo)
    {
        transform.position = Vector2.MoveTowards(transform.position, objetivo, -velocidadEnemigo * Time.deltaTime);
    }

    private void MoverEnemigoAleatorio()
    {
        Vector2 posicionAleatoria = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f));
        transform.position = posicionAleatoria;
    }

    private void MiniGame()
    {
        posicionInicialEnemigo = transform.position;
        MoverEnemigoAleatorio();
        PanelInicio.SetActive(true);
    }
    IEnumerator Huir(float espera)
    {
        yield return new WaitForSeconds(espera);
        //Minijuego
        Vector2 posicionRaton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MoverEnemigo(posicionRaton);
        PanelInicio.SetActive(false);
    }
    IEnumerator Iniciador(float tiempoEspera)
    {
        yield return new WaitForSeconds(tiempoEspera);
        MiniGame();
    }
}
