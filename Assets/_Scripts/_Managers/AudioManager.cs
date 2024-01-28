using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public float musicVolume;
    [SerializeField] private AudioSource _musica;
    public void SaveMusica()
    {
        musicVolume = InicioManager.Instance._musicaSlider.value;
        _musica.volume = musicVolume;
    }
    private void Update()
    {
        SaveMusica();
    }
}
