using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InicioManager : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }

    public void cambioDeEscena(int Escena)
    {
        SceneManager.LoadScene(Escena);
    }

}
