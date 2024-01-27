using UnityEngine;

public class PersecusionManager : MonoBehaviour
{
    public float velocidadEnemigo = 5f;
    private Vector2 posicionInicialEnemigo;
    [SerializeField] GameObject panelFelicidades;

    void Start()
    {
        posicionInicialEnemigo = transform.position;
        MoverEnemigoAleatorio();
    }

    void Update()
    {
        Vector2 posicionRaton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MoverEnemigo(posicionRaton);
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
}
